using NUnit.Framework;
using MiProyectoPruebas.Utils;
using SortByEnum.Utils;

namespace MiProyectoPruebas.Tests
{
    public class SDSortByZtoATest: TestBase
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
        public void VerifySortByZtoA()
        {
            Logger.LogAction("Starting test to validate sort by Z to A...");
            SDLoginPage.Login(username, password);

            string sortByZtoA = SortBy.ZtoA.GetSort();

            Logger.LogAction($"Selecting sort By {sortByZtoA}...");
            SDHomePage.selectSort(sortByZtoA);

            Logger.LogAction("Storing all product in the page...");
            List<string> products = SDHomePage.GetAllProductNamesInHomePage();

            Logger.LogAction("Sorting the list in Descending format...");
            List<string> productsSorted = products.OrderByDescending(x => x).ToList();

            Logger.LogAction($"Validating {products} is sorted the same way as {productsSorted}");
            bool areEqual = SDHomePage.verifySortedAlphabetical(products, productsSorted);
            Assert.That(areEqual, Is.True);

            Logger.LogAction("Loging out from the site...");
            SDHomePage.LogoutFromSite();
        }
    }
}
