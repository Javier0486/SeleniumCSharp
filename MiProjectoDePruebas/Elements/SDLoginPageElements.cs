using OpenQA.Selenium;

namespace MiProyectoPruebas.Elements
{
    public class SDLoginPageElements
    {
        public static By usernameIput = By.Id("user-name");
        public static By passwordInput = By.Id("password");
        public static By loginButton = By.Id("login-button");
        public static By lockedUserMessage = By.XPath("//h3[normalize-space(text())='Epic sadface: Sorry, this user has been locked out.']");
    }
}