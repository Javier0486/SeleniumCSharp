using NUnit.Framework;
using MiProyectoPruebas.Utils;
using MiProyectoPruebas.Pages;
using MiProyectoPruebas.config;
using OpenQA.Selenium;

namespace MiProyectoPruebas.Tests
{
    [Category("LoginTests")]
    public class LoginTests : TestBase
    {
        private string siteKey;

        [SetUp]
        public void BeforeEachTest()
        {
            siteKey = targetApp; // targetApp comes from TestBase, and it's "saucedemo"
         }

        [Test]
        public void TestLogin()
        {
            var LoginManager = new LoginManager(driver);
            TestContext.WriteLine($"Logging into site {siteKey}");
            LoginManager.LoginToApp(siteKey);

            Assert.That(driver.Url, Does.Contain("inventory.html"));
        }

    }
}