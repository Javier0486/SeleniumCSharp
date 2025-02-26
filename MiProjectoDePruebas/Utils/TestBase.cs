using NUnit.Framework;
using OpenQA.Selenium;
using Microsoft.Extensions.Configuration;

namespace MiProyectoPruebas.Utils
{
    public class TestBase
    {
        protected IWebDriver driver;
        protected string baseUrl;

        [OneTimeSetUp] //Se ejecuta una sola vez antes de todas la pruebas de la clase
        public void OneTimeSetUp()
        {
            var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "MiProjectoDePruebas"));

            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(projectRoot, "Config"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            baseUrl = config["TestSettings:baseUrl"] ?? throw new System.Exception("baseUrl no definido en appsettings.json");
        }

        [SetUp] //Se ejecuta antes de cada prueba
        public void SetUp()
        {
            driver = DriverFactory.GetDriver(); // Obtiene una nueva instancia del WebDriver
        }

        [TearDown] //Se ejecuta despues de cada prueba
        public void TearDown()
        {
            try
            {
                driver?.Quit();
                driver?.Dispose();
            }
            catch (System.Exception ex)
            {
                TestContext.WriteLine("Error al cerrar el driver: " + ex.Message);
            }
        }

        [OneTimeTearDown] //Se ejecuta uan sola vez despues de todas las pruebas de la clase
        public void OneTimeTearDown()
        {
            TestContext.WriteLine("Finalizando pruebas...");
        }
    }
}