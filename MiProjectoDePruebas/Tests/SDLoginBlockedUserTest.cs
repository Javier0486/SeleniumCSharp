using NUnit.Framework;
using MiProyectoPruebas.Elements;
using MiProyectoPruebas.Utils;

namespace MiProyectoPruebas.Tests
{
    public class SDLoginBlockedUserTest : TestBase
    {   
        private string usernamelocked;
        private string password;
        private string username;

        [SetUp]
        public void TestSetup()
        {
            driver.Navigate().GoToUrl(baseUrl);

            //values from configuration
            Logger.LogAction("loading configuration data...");
            usernamelocked = config["TestSettings:lockOutUser"] ?? throw new Exception("lockOutUser no definido en appsettings.json");
            password = config["TestSettings:password"] ?? throw new Exception("password not defined");
            username = config["TestSettings:standardUser"] ?? throw new Exception("standardUser not defined");
        }

        [Test]
        public void VerifyLoginFailAnPass()
        {

            // login locked user
            Logger.LogAction("Starting user locked test...");
            SDLoginPage.Login(usernamelocked, password);
            Assert.That(SDLoginPage.VerifyLoginWithLockedOutUser(), Is.True, "locked user message is not displayed");

            // clean fields
            Logger.LogAction("clear input fields");
            SDLoginPage.ClearInputs();
            
            // login valid user
            Logger.LogAction("starting valid user test...");
            SDLoginPage.Login(username, password);
            var header = SDLoginPage.WaitForHeader(SDHomePageElements.SwagLabsHeader);
            Assert.That(header.Displayed, "the SwagLabs header is not displayed after successful login");
        }
    }
}