using NUnit.Framework;
using MiProyectoPruebas.Elements;
using MiProyectoPruebas.Utils;
using MiProyectoPruebas.Pages;

namespace MiProyectoPruebas.Tests
{
    public class SDLoginBlockedUserTest : TestBase
    {   
        private SDLoginPage sDLoginPage;

        [SetUp]
        public void TestSetup()
        {
            driver.Navigate().GoToUrl(baseUrl);
            sDLoginPage = new SDLoginPage(driver, config);
        }

        [Test]
        public void VerifyLoginFailAnPass()
        {
            //data from config
            string usernamelocked = config["TestSettings:lockOutUser"] ?? throw new Exception("lockOutUser no definido en appsettings.json");
            string password = config["TestSettings:password"] ?? throw new Exception("password not defined");
            string username = config["TestSettings:standardUser"] ?? throw new Exception("standarUser no definido");

            // login locked user
            sDLoginPage.Login(usernamelocked, password);
            Assert.That(sDLoginPage.VerifyLoginWithLockedOutUser(), Is.True, "locked user message is not displayed");

            // clean fields
            sDLoginPage.ClearInputs();
            
            // login valid user
            sDLoginPage.Login(username, password);
            var header = sDLoginPage.WaitForHeader(SDHomePageElements.SwagLabsHeader);
            Assert.That(header.Displayed, "the SwagLabs header is not displayed after successful login");
        }
    }
}