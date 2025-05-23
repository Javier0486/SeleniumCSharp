using OpenQA.Selenium;
using MiProyectoPruebas.Framework;
using MiProyectoPruebas.config;
using MiProyectoPruebas.Elements;
using MiProyectoPruebas.Utils;

public class LoginPage : BasePage
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