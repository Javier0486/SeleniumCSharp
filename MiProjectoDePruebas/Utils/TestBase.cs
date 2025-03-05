using NUnit.Framework;
using OpenQA.Selenium;
using Microsoft.Extensions.Configuration;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.IO;
using MongoDB.Driver.Core.Misc;

namespace MiProyectoPruebas.Utils
{
    public class TestBase
    {
        protected IWebDriver driver;
        protected string baseUrl;
        protected static ExtentReports extent;
        protected ExtentTest test;
        protected IConfiguration config;

        [OneTimeSetUp] //Se ejecuta una sola vez antes de todas la pruebas de la clase
        public void OneTimeSetUp()
        {
            var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "MiProjectoDePruebas"));

            // Configuration
            config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(projectRoot, "Config"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            baseUrl = config["TestSettings:baseUrl"] ?? throw new System.Exception("baseUrl no definido en appsettings.json");

            //Asegura que la carpeta Report Exista antes de generar el reporte
            string reportFolder = EnsureDirectoryExists(Path.Combine(projectRoot, "Report"));
            
            string reportPath = Path.Combine(reportFolder, "TestReport.htm");

            var htmlReporter = new ExtentSparkReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [SetUp] //Se ejecuta antes de cada prueba
        public void SetUp()
        {
            driver = DriverFactory.GetDriver(); // Obtiene una nueva instancia del WebDriver
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown] //Se ejecuta despues de cada prueba
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                test.Fail("Test Failed");
            else
                test.Pass("Test Passed");

            try
            {
                driver?.Quit();
                driver?.Dispose();
            }
            catch (Exception ex)
            {
                TestContext.Progress.WriteLine("Error al cerrar el driver: " + ex.Message);
            }
        }

        [OneTimeTearDown] //Se ejecuta uan sola vez despues de todas las pruebas de la clase
        public void OneTimeTearDown()
        {
            TestContext.Progress.WriteLine("Finalizando pruebas...");
            
            if (extent != null)
                {
                    extent.Flush();
                }   
                else
                {
                    TestContext.Progress.WriteLine("Error: ExtentReports was not initialized");
                }
        }

        public string TakeScreenshot(string testName)
        {
            try
            {
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string screenshotFolder = EnsureDirectoryExists(Path.Combine(Directory.GetCurrentDirectory(), "Screenshots"));
                string screenshotPath = Path.Combine(screenshotFolder, $"{testName}_{timestamp}.png");

                screenshot.SaveAsFile(screenshotPath);
                
                return screenshotPath;
            }
            catch (Exception e)
            {
                TestContext.Progress.WriteLine("Error from screenshot: " + e.Message);
                return null;
            }
        }

        private string EnsureDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            return directoryPath;
        }
    }
}