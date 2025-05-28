using OpenQA.Selenium;
using MiProyectoPruebas.Framework;
using MiProyectoPruebas.config;
using MiProyectoPruebas.Elements;
using MiProyectoPruebas.Utils;

/** Single Responsibility Principle (SRP)
    each class should have only one reason to change
    Handles all actions and interactions specific to the login page. Uses locators and configuration to perform login-related tasks.
    Implements the Single Responsibility principle by focusing only on login page logic.
    keeps code maintainable and easy to understand. Each class has a clear purpose.

    Interface Segregation Principle (ISP)
    Clients should not be forced to depend on interfaces they do not use
    Implements only the interfaces it needs (e.g. IClickable, ITextEntry), so it is not forced to depend on methods it doesn't use.
    It works by defining small interfaces and having each class implement only what it needs, avoiding "fat" interfaces and unnecessary dependencies.
*/
public class LoginPage : BasePage, ITextEntry, IClickable
{
    private readonly By _usernameLocator;
    private readonly By _passwordLocator;
    private readonly By _loginButtonLocator;
    private readonly string _siteKey;

    public LoginPage(IWebDriver driver, string siteKey) : base(driver)
    {
        _siteKey = siteKey;
        var (username, password, loginBtn) = LOCATORS.Map[siteKey];

        // Determine whether to use CssSelector or XPath dynamically
        _usernameLocator = username.StartsWith("//") ? By.XPath(username) : By.CssSelector(username);
        _passwordLocator = password.StartsWith("//") ? By.XPath(password) : By.CssSelector(password);
        _loginButtonLocator = loginBtn.StartsWith("//") ? By.XPath(loginBtn) : By.CssSelector(loginBtn);
    }

    public void Navigate()
    {
        string url = ConfigReader.GetBaseUrl(_siteKey);
        Driver.Navigate().GoToUrl(url);
    }

    public void Login()
    {
        var (username, password) = ConfigReader.GetCredentials(_siteKey);
        EnterText(_usernameLocator, username);
        EnterText(_passwordLocator, password);
        Click(_loginButtonLocator);
    }
}