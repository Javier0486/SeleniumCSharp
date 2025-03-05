using OpenQA.Selenium;

namespace MiProyectoPruebas.Elements
{
    public class SDLoginPageElements
    {
        public static By UsernameIput = By.Id("user-name");
        public static By PasswordInput = By.Id("password");
        public static By LoginButton = By.Id("login-button");
        public static By LockedUserMessage = By.XPath("//h3[normalize-space(text())='Epic sadface: Sorry, this user has been locked out.']");
    }
}