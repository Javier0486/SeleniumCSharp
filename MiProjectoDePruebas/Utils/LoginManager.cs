using OpenQA.Selenium;
using MiProyectoPruebas.Pages;
using MiProyectoPruebas.config;

/// Implements Facade Pattern to provide a simplified interface for login operations
/// across multiple application
/** Single Responsibility Principle (SRP)

    each class should have only one reason to change
    LoginManager  coordinates login operations
    keeps code maintainable and easy to understand. Each class has a clear purpose.

    Open/Close Principle (OCP)
    Classes should be open for extension, but close for modification
    Can be extended for new login flows
    New features can be added (like new pages or browsers) without changing existing code.

    Dependency Inversion Principle (DIP)
    Depends on abstractions, not on concretions.
    Depend on IWebDriver abstraction, not a specific driver.
    The classes use the IWebDriver interface, allowing to swap out the actual driver implementation if needed.
    Makes the code flexible and stable by relying on interfaces (IWebDriver).
*/
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