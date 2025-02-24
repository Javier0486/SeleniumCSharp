using NUnit.Framework;
using MiProyectoPruebas.Utils;
using MiProyectoPruebas.Pages;
using OpenQA.Selenium;

namespace MiProyectoPruebas.Tests
{
    public class LoginTests : TestBase
    {
        private LoginPage loginPage;

        [SetUp]
        public void BeforeEachTest()
        {
            loginPage = new LoginPage(driver);
        }

        [Test]
        [Ignore("skip test")]
        public void TestLogin()
        {
            string baseUrl = ConfigReader.GetBaseUrl();
            driver.Navigate().GoToUrl(baseUrl);
            Assert.That(driver.Url, Is.EqualTo(baseUrl));
        }

    }
}