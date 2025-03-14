using NUnit.Framework;
using MiProyectoPruebas.Utils;
using SwagLabsHomepageEnum.Utils;

namespace MiProyectoPruebas.Tests
{
    public class SDEndToEndTest : TestBase
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
        public void EndToEndTest()
        {
            Logger.LogAction("starting end to end test...");
            SDLoginPage.Login(username, password);

            string[] products = new string[]
            {
                ProductsInHomepage.Products["backPAck"], 
                ProductsInHomepage.Products["onesie"]
            };
            Logger.LogAction($"Selecting products: {string.Join(", ", products)}");
            SDHomePage.SelectProducts(products);
            SDHomePage.ClickCartIcon();

            Logger.LogAction("Verifying products in cart...");
            Assert.That(SDCartPage.isProductDisplayedInCart(products), $"Some products in the cart are not displayed. Products: {string.Join(", ", products)}");

            Logger.LogAction("Proceeding to checkout...");
            SDCartPage.Checkout();

            Logger.LogAction("Filling user information...");
            SDCheckoutYourInfoPage.FillInfo("John", "Doe", "12345");

            Logger.LogAction("Continuing to overview...");
            SDCheckoutYourInfoPage.Continue();

            Logger.LogAction("Verifying products in overview...");
            Assert.That(SDCheckoutOverviewPage.isProductInOverview(products), $"Some products are not in the overview. Products: {string.Join(", ", products)}");

            Logger.LogAction("Finishing checkout...");
            SDCheckoutOverviewPage.Finish();

            var expectedText = "Thank you for your order!";
            var expectedHeader = "Your order has been dispatched, and will arrive just as fast as the pony can get there!";
            Logger.LogAction("Verifying order completion...");
            Assert.That(SDCheckoutCompletePage.AreHeaderAndTextDisplayed(expectedText, expectedHeader));

            Logger.LogAction("Going back to homepage...");
            SDCheckoutCompletePage.BackHome();

            Logger.LogAction("Loging out...");
            Assert.That(SDHomePage.LogoutFromSite(), "Login Page is not displayed after logout");

        }
    }
}