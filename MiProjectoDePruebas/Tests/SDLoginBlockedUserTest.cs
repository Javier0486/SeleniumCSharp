using NUnit.Framework;
using MiProyectoPruebas.Elements;
using MiProyectoPruebas.Utils;

namespace MiProyectoPruebas.Tests
{
    public class SDLoginBlockedUserTest : TestBase
    {
        private string siteKey;
        private string lockedUsername;

        [SetUp]
        public void TestSetup()
        {
            siteKey = targetApp;
            lockedUsername = ConfigReader.GetConfigValue("Credentials:SauceDemo:LockOutUser");
        }

        [Test]
        public void VerifyLoginFailAnPass()
        {
            var LoginManager = new LoginManager(driver);
            // login locked user
            Logger.LogAction("Starting user locked test...");
            LoginManager.LoginToAppWithCredentials(siteKey, lockedUsername);
            Assert.That(SDLoginPage.VerifyLoginWithLockedOutUser(), Is.True, "locked user message is not displayed");

            // clean fields
            Logger.LogAction("clear input fields");
            SDLoginPage.ClearInputs();
            
            // login valid user
            Logger.LogAction("starting valid user test...");
            LoginManager.LoginToApp(siteKey);
            var header = SDLoginPage.WaitForHeader(SDHomePageElements.SwagLabsHeader);
            Assert.That(header.Displayed, "the SwagLabs header is not displayed after successful login");
        }
    }
}