using NUnit.Framework;
using MiProyectoPruebas.Utils;
using SortByEnum;
using SortByEnum.Utils;

namespace MiProyectoPruebas.Tests
{
    public class SDSortByLowToGighPriceTest : TestBase
    {
        private string username;
        private string password;

        [SetUp]
        public void TestSetup()
        {
            driver.Navigate().GoToUrl(baseUrl);

            Logger.LogAction("loading configuration data...");
            username = config["TestSettings:standardUser"] ?? throw new Exception("standardUser not defined");
            password = config["TestSettings:password"] ?? throw new Exception("password not defined");
        }

        [Test]
        public void VerifySortByLowToHigh()
        {
            Logger.LogAction("starting test to validate sort by Low to High price...");
            SDLoginPage.Login(username, password);

            string sortByLowToHigh = SortBy.LowToHigh.GetSort();

            Logger.LogAction($"Selecting Sort By {sortByLowToHigh}...");
            SDHomePage.selectSort(sortByLowToHigh);

            Logger.LogAction("Storing the products after selecting the type of sort...");
            List<string> productPrices = SDHomePage.GetPriceFromAllProductsInHomePage();

            Logger.LogAction("Converting the prices from string to double...");
            List<double> pricesInDouble = SDHomePage.convertToNumber(productPrices);

            Logger.LogAction("Ordering the list from low to high price...");
            List<double> orderedList = SDHomePage.orderLowToHighPrice(pricesInDouble);

            Logger.LogAction($"Logging both lists \n{string.Join(", ", pricesInDouble)} \n{string.Join(", ",orderedList)}");

            Logger.LogAction("Validating both lists are sorted in the same way...");
            bool areEqual = SDHomePage.verifySorted(pricesInDouble, orderedList);
            Assert.That(areEqual, Is.True);

            Logger.LogAction("Loging out from the site after finishing validations...");
            SDHomePage.LogoutFromSite();
        }
    }
}