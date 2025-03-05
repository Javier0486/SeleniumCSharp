using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using MiProyectoPruebas.Elements;
using SeleniumExtras.WaitHelpers;
using Microsoft.Extensions.Configuration;
using MiProyectoPruebas.Framework;

namespace MiProyectoPruebas
{
    public class SDLoginPage : BasePage // Modification: Inherit from BasePage
    {
        private readonly IConfiguration config; 

        public SDLoginPage(IWebDriver driver, IConfiguration config) : base(driver) // Modification: constructor updated
        {
            this.config = config;
        }

        public void Login(string user, string password)
        {
            // Modification: Use of BasePage method
            EnterText(SDLoginPageElements.UsernameIput, user);
            ClearField(SDLoginPageElements.PasswordInput);
            EnterText(SDLoginPageElements.PasswordInput, password);
            Click(SDLoginPageElements.LoginButton);

        }

        public bool VerifyLoginWithLockedOutUser()
        {
            return IsElementDisplayed(SDLoginPageElements.LockedUserMessage);
        }

        public void ClearInputs() // New method to clear input fields
        {
            ClearField(SDLoginPageElements.UsernameIput);
            ClearField(SDLoginPageElements.PasswordInput);
        }

        public IWebElement WaitForHeader(By locator)
        {
            return WaitUntilElementExist(locator);
        }
    }
}