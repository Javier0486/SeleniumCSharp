using OpenQA.Selenium;
using MiProyectoPruebas.Elements;
using MiProyectoPruebas.Framework;

namespace MiProyectoPruebas
{
    public class SDLoginPage : BasePage // Inherit from BasePage
    {
        public SDLoginPage(IWebDriver driver) : base(driver) {}

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