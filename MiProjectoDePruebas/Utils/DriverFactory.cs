using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace MiProyectoPruebas.Utils
{
    /** Single Responsibility Principle (SRP)
        each class should have only one reason to change
        DriverFactory manages WebDriver creation and disposal
        keeps code maintainable and easy to understand. Each class has a clear purpose.

        Open/Close Principle (OCP)
        Classes should be open for extension, but close for modification
        Can be extended to support more browsers by adding new methods/options.
        New features can be added (like new pages or browsers) without changing existing code.
    */
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
                // optioins added to avoid emergent browser windows
                options.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2);
                options.AddUserProfilePreference("profile.default_content_setting_values.popups", 2);
                options.AddArgument("--password-store=basic");
                options.AddArgument("--disable-features=PasswordLeakDetection");
                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);
                options.AddArgument("--incognito");
                options.AddArgument("--disable-save-password-bubble");
                options.AddArgument("--user-data-dir=/tmp/temporary-chrome-profile");

                driver = new ChromeDriver(options);
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