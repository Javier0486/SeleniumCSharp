using NUnit.Framework;
using MiProyectoPruebas.Utils;
using SortByEnum.Utils;

namespace MiProyectoPruebas.Tests
{
    public class SDSortByAtoZTest : TestBase
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
        public void VerifySortByAtoZ()
        {
            Logger.LogAction("starting test to validate sort by A to Z...");
            SDLoginPage.Login(username, password);

            string sortByAtoZ = SortBy.AtoZ.GetSort();

            Logger.LogAction($"Selecting Sort By: {sortByAtoZ}");
            SDHomePage.selectSort(sortByAtoZ);

            Logger.LogAction("Storing the products after selecting the type of sort");
            List<string> products = SDHomePage.GetAllProductNamesInHomePage();
            List<string> sortedItems = products.OrderBy(x => x).ToList();

            Logger.LogAction($"List of products: {string.Join(", ",products)}");
            Logger.LogAction($"List ordered: {string.Join(", ", sortedItems)}");

            bool areEqual = SDHomePage.verifySortedAlphabetical(products, sortedItems);
            Assert.That(areEqual, Is.True);

            SDHomePage.LogoutFromSite();
        }
    }
}