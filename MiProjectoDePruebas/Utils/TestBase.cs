using NUnit.Framework;
using OpenQA.Selenium;
using MiProyectoPruebas.Utils;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace MiProyectoPruebas.Utils
{
    public class TestBase
    {
        protected IWebDriver driver;
        protected string baseUrl;

        [SetUp]
        public void SetUp()
        {
            driver = DriverFactory.GetDriver();
            var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "MiProjectoDePruebas"));

            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(projectRoot, "Config"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            baseUrl = config["TestSettings:baseUrl"] ?? throw new System.Exception("baseUrl no definido en appsettings.json");
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}