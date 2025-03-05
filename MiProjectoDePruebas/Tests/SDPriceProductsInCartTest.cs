using NUnit.Framework;
using OpenQA.Selenium;
using MiProyectoPruebas.Utils;
using MiProyectoPruebas.Elements;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using SwagLabsHomepageEnum.Utils;

namespace MiProyectoPruebas.Tests
{
    public class SDPriceProductsCartTest : TestBase
    {
        private SDLoginPage sDLoginPage;
        private SDHomePage sDHomePage;
        private SDCartPage sDCartPage;
        private WebDriverWait wait;

        [SetUp]
        public void TestSetup()
        {
            driver.Navigate().GoToUrl(baseUrl);
            sDLoginPage = new SDLoginPage(driver, config);
            sDCartPage = new SDCartPage(driver, config);
            sDHomePage = new SDHomePage(driver, config);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void VerifyProductPricesInCart()
        {
            string username = config["TestSettings:standardUser"] ?? throw new Exception("standardUser not defined");
            string password = config["TestSettings:password"] ?? throw new Exception("password not defined");

            sDLoginPage.LoginTo(username, password);

            string[] products = new string[]
            {
                ProductsInHomepage.Products["backPAck"], 
                ProductsInHomepage.Products["onesie"]
            };
            List<string> productPrices = sDHomePage.GetPricesFromHomepage(products);

            sDHomePage.selectProducts(products);
            sDHomePage.clickCartIcon();

            Assert.That(sDCartPage.isPriceDisplayed(products, productPrices), $"Not all the prices match. Products: {string.Join(", ", products)}, Expected Prices: {string.Join(", ", productPrices)}");
        }
    }
}