using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace MiProyectoPruebas.Utils
{
    public class DriverFactory
    {
        private static IWebDriver? driver;

        public static IWebDriver GetDriver()
        {
            if (driver == null)
            {
                // this line downloads the chromeDriver version compatible with the browser
                new DriverManager().SetUpDriver(new ChromeConfig());

                var options = new ChromeOptions();
                // we can add here more options if needed: like options.AddArguments("--headless");
                
                driver = new ChromeDriver();
            }
            return driver;
        }

        public static void CloseDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }
}