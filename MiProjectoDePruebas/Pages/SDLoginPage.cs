using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using MiProyectoPruebas.Elements;
using SeleniumExtras.WaitHelpers;
using Microsoft.Extensions.Configuration;

namespace MiProyectoPruebas
{
    public class SDLoginPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly IConfiguration config;

        public SDLoginPage(IWebDriver driver, IConfiguration config)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            this.config = config;
        }

        public void LoginTo(string user, string password)
        {
            driver.FindElement(SDLoginPageElements.usernameIput).SendKeys(user);
            driver.FindElement(SDLoginPageElements.passwordInput).Clear();//se agrego ya que aunque se ejecute la funcion ClearInputs, cuando vuelve a escribir el password aparece dos veces, con esta linea nos aseguramos que cuando vuelva a escribir el password el input este limpio
            driver.FindElement(SDLoginPageElements.passwordInput).SendKeys(password);
            driver.FindElement(SDLoginPageElements.loginButton).Click(); 
        }

        public bool VerifyLoginWithLockedOutUser()
        {
            return driver.FindElement(SDLoginPageElements.lockedUserMessage).Displayed;
        }

        public void ClearInputs()
        {
            var usernameElement = driver.FindElement(SDLoginPageElements.usernameIput);
            var passwordElement = driver.FindElement(SDLoginPageElements.passwordInput);

            usernameElement.Clear();
            passwordElement.Clear();
            }
    }
}