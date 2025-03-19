using NUnit.Framework;
using MiProyectoPruebas.Utils;
using SwagLabsHomepageEnum.Utils;

namespace MiProyectoPruebas.Tests
{
    public class SDPriceProductsCartTest : TestBase
    {
        private string username;
        private string password;

        [SetUp]
        public void TestSetup()
        {
            driver.Navigate().GoToUrl(baseUrl);

            // load configuration data
            Logger.LogAction("loading configuration data...");
            username = config["TestSettings:standardUser"] ?? throw new Exception("standardUser not defined");
            password = config["TestSettings:password"] ?? throw new Exception("password not defined");
        }

        [Test]
        public void VerifyProductPricesInCart()
        {
            Logger.LogAction("starting test to validate product prices in the cart...");
            SDLoginPage.Login(username, password);

            string[] products = new string[]
            {
                ProductsInHomepage.Backpack.GetProduct(), 
                ProductsInHomepage.BikeLight.GetProduct()
            };
            Logger.LogAction($"Selecting products: {string.Join(", ", products)}");
            List<string> productPrices = SDHomePage.GetPricesFromHomepage(products);

            SDHomePage.SelectProducts(products);
            SDHomePage.ClickCartIcon();

            Logger.LogAction("Verifying prices in cart...");
            Assert.That(SDCartPage.isPriceDisplayed(products, productPrices), $"Some product prices in the cart do not match. Products: {string.Join(", ", products)}, Expected Prices: {string.Join(", ", productPrices)}");
        }
    }
}