using OpenQA.Selenium;

namespace MiProyectoPruebas.Pages
{
    public class LoginPage
    {
        private IWebDriver driver;

        //localizadores
        private By userField = By.Id("username");
        private By passwordField = By.Id("password");
        private By loginButton = By.Id("login");

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //metodos de accion
        public void EnterUsername(string username) => driver.FindElement(userField).SendKeys(username);
        public void EnterPassword(string password) => driver.FindElement(passwordField).SendKeys(password);
        public void ClickLogin() => driver.FindElement(loginButton).Click();
    }
}