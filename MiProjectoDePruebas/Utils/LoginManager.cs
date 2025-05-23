using OpenQA.Selenium;
using MiProyectoPruebas.Pages;
using MiProyectoPruebas.config;

/// Implements Facade Pattern to provide a simplified interface for login operations
/// across multiple application
public class LoginManager
{
    private readonly IWebDriver _driver;

    public LoginManager(IWebDriver driver)
    {
        _driver = driver;
    }

    /// Executes complete SauceDemo login flow using configured credentials
    public void LoginToApp(string siteKey)
    {
        var loginPage = new LoginPage(_driver, siteKey);
        loginPage.Navigate();
        loginPage.Login();
    }
}