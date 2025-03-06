using NUnit.Framework;
using MiProyectoPruebas.Utils;
using MiProyectoPruebas.Elements;

namespace MiProyectoPruebas.Tests
{
    public class SDLogoutTest : TestBase
    {
        private string username;
        private string password;

        [SetUp]
        public void TestSetup()
        {
            driver.Navigate().GoToUrl(baseUrl);

            //load configuration data
            Logger.LogAction("loading configuration data");
            username = config["TestSettings:standardUser"] ?? throw new Exception("standardUser not defined");
            password = config["TestSettings:password"] ?? throw new Exception("password not defined");
        }

        [Test]
        public void VerifyLogout()
        {
            Logger.LogAction("starting test logout functionality...");
            SDLoginPage.Login(username, password);
            var header = SDLoginPage.WaitForHeader(SDHomePageElements.SwagLabsHeader);
            Assert.That(header.Displayed, "the SwagLabs header is not displayed after successful login");

            Logger.LogAction("Verifying Login Page is displayed after logout...");
            Assert.That(SDHomePage.LogoutFromSite(), "Login Page is not displayed after logout");
        }
    }
}