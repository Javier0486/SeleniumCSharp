using NUnit.Framework;
using OpenQA.Selenium;
using Microsoft.Extensions.Configuration;
using AventStack.ExtentReports;
using MiProyectoPruebas.Utils;

namespace MiProyectoPruebas.Utils
{
    public class TestBase
    {
        protected IWebDriver driver;
        protected string baseUrl;
        protected string targetApp;
        protected static ExtentReports extent;
        protected ExtentTest test;
        protected IConfiguration config;

        // classes page properties
        protected SDLoginPage SDLoginPage => new SDLoginPage(driver);
        protected SDHomePage SDHomePage => new SDHomePage(driver);
        protected SDCartPage SDCartPage => new SDCartPage(driver);
        protected SDCheckoutYourInfoPage SDCheckoutYourInfoPage => new SDCheckoutYourInfoPage(driver);
        protected SDCheckoutOverviewPage SDCheckoutOverviewPage => new SDCheckoutOverviewPage(driver);
        protected SDCheckoutCompletePage SDCheckoutCompletePage => new SDCheckoutCompletePage(driver);

        [OneTimeSetUp] //Se ejecuta una sola vez antes de todas la pruebas de la clase
        public void OneTimeSetUp()
        {
            var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "MiProjectoDePruebas"));

            // Configuration
            config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(projectRoot, "Config"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            targetApp = TestContext.Parameters.Get("TargetSite", "SauceDemo");
            baseUrl = config[$"BaseUrls:{targetApp}"] ?? throw new System.Exception("SauceDemo baseUrl not found in appsettings.json");

            //Asegura que la carpeta Report Exista antes de generar el reporte
            var reportPath = Path.Combine(projectRoot, "Report", "TestReport.html");
            extent = ReportManager.CreateReport(reportPath);
        }

        [SetUp] //Se ejecuta antes de cada prueba
        public void SetUp()
        {
            Logger.LogAction("Initializing driver...");
            driver = DriverFactory.GetDriver(); // Obtiene una nueva instancia del WebDriver
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown] //Se ejecuta despues de cada prueba
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                test.Fail("Test Failed");

                // taking screenshot if failed
                string screenshotPath = ScreenshotManager.TakeScreenshot(driver, TestContext.CurrentContext.Test.Name);
            if (!string.IsNullOrEmpty(screenshotPath))
            {
                test.AddScreenCaptureFromPath(screenshotPath);
            }
            else
            {
                test.Pass("Test Passed");
            }

            try
            {
                Logger.LogAction("Ending script and closing driver");
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
    }
}