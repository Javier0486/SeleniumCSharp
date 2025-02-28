using NUnit.Framework;
using OpenQA.Selenium;
using Microsoft.Extensions.Configuration;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

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

            config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(projectRoot, "Config"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            baseUrl = config["TestSettings:baseUrl"] ?? throw new System.Exception("baseUrl no definido en appsettings.json");

            //Asegura que la carpeta Report Exista antes de generar el reporte
            string reportFolder = Path.Combine(projectRoot, "Report");
            if (!Directory.Exists(reportFolder))
            {
                Directory.CreateDirectory(reportFolder);
            }

            string reportPath = Path.Combine(projectRoot, "Report", "TestReport.htm");

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
            catch (System.Exception ex)
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
                TestContext.Progress.WriteLine("Generando reporte...");
                extent.Flush();
                TestContext.Progress.WriteLine("Reporte generado con exito...");
            }
            else
            {
                TestContext.Progress.WriteLine("Error: ExtentReports no fue inicializado");
            }
        }

        public string TakeScreenshot(string testName)
        {
            try
            {
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots", $"{testName}_{timestamp}.png");

                //Asegurar que la carpeta de screenshots exista
                Directory.CreateDirectory(screenshotPath);

                screenshot.SaveAsFile(screenshotPath);
                
                return screenshotPath;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error tomando la captura de pantalla: " + e.Message);
                return null;
            }
        }
    }
}