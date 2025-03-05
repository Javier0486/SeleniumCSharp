using NUnit.Framework;
using MiProyectoPruebas.Utils;
using SwagLabsHomepageEnum.Utils;

namespace MiProyectoPruebas.Tests
{
    public class SDPriceProductsCartTest : TestBase
    {
        private SDLoginPage sDLoginPage;
        private SDHomePage sDHomePage;
        private SDCartPage sDCartPage;

        [SetUp]
        public void TestSetup()
        {
            driver.Navigate().GoToUrl(baseUrl);
            sDLoginPage = new SDLoginPage(driver, config);
            sDCartPage = new SDCartPage(driver, config);
            sDHomePage = new SDHomePage(driver, config);
        }

        [Test]
        public void VerifyProductPricesInCart()
        {
            string username = config["TestSettings:standardUser"] ?? throw new Exception("standardUser not defined");
            string password = config["TestSettings:password"] ?? throw new Exception("password not defined");

            sDLoginPage.Login(username, password);

            string[] products = new string[]
            {
                ProductsInHomepage.Products["backPAck"], 
                ProductsInHomepage.Products["onesie"]
            };
            List<string> productPrices = sDHomePage.GetPricesFromHomepage(products);

            sDHomePage.SelectProducts(products);
            sDHomePage.ClickCartIcon();

            Assert.That(sDCartPage.isPriceDisplayed(products, productPrices), $"Some product prices in the cart do not match. Products: {string.Join(", ", products)}, Expected Prices: {string.Join(", ", productPrices)}");
        }
    }
}