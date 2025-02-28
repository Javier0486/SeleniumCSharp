using NUnit.Framework;
using OpenQA.Selenium;
using MiProyectoPruebas.Utils;
using MiProyectoPruebas.Elements;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;

namespace MiProyectoPruebas.Tests
{
    public class SDLoginBlockedUserTest : TestBase
    {
        private SDLoginPage sdLoginPage;
        private readonly WebDriverWait wait;
        
        [SetUp]
        public void TestSetup()
        {
            driver.Navigate().GoToUrl(baseUrl);
            sdLoginPage = new SDLoginPage(driver, config);
        }

        [Test]
        public void VerifyLoginFailAnPass()
        {
            string usernamelocked = config["TestSettings:lockOutUser"] ?? throw new Exception("lockOutUser no definido en appsettings.json");
            string password = config["TestSettings:password"];
            string username = config["TestSettings:standardUser"] ?? throw new Exception("standarUser no definido");

            sdLoginPage.LoginTo(usernamelocked, password);
            Assert.That(sdLoginPage.VerifyLoginWithLockedOutUser(), Is.True, "El mensaje de usuario bloqueado no es visible");

            sdLoginPage.ClearInputs();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

            sdLoginPage.LoginTo(username, password);
            var header = wait.Until(ExpectedConditions.ElementExists(SDHomePageElements.SwagLabsHeader));
            Assert.That(header.Displayed);
        }
    }
}