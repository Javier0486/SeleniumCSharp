using NUnit.Framework;
using MiProyectoPruebas.Utils;
using SwagLabsHomepageEnum.Utils;
using OpenQA.Selenium.Support.UI;

namespace MiProyectoPruebas.Tests
{
    public class SDEndToEndTest : TestBase
    {
        private string siteKey;

        [SetUp]
        public void TestSetup()
        {
            siteKey = targetApp;
        }

        [Test] 
        public void EndToEndTest()
        {
            var LoginManager = new LoginManager(driver);
            TestContext.WriteLine($"Login into site {siteKey}");
            LoginManager.LoginToApp(siteKey);

            string[] products = new string[]
            {
                ProductsInHomepage.Backpack.GetProduct(), 
                ProductsInHomepage.Onesie.GetProduct()
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