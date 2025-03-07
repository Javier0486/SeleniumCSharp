using MiProyectoPruebas.Tests;
using NUnit.Framework;

namespace MiProyectoPruebas.Suites
{
    [TestFixture]
    [Category("DynamicSuite")]
    public class DynamicIDTestSuite
    {
        private SDLogoutTest sdLogoutTest;
        private SDLoginBlockedUserTest sdLoginBlockedUserTest;
        private SDPriceProductsCartTest sdPriceProductsCartTest;

        [SetUp]
        public void SetUp()
        {
            sdLogoutTest = new SDLogoutTest();
            sdLogoutTest.SetUp();

            sdLoginBlockedUserTest = new SDLoginBlockedUserTest();
            sdLoginBlockedUserTest.SetUp();

            sdPriceProductsCartTest = new SDPriceProductsCartTest();
            sdPriceProductsCartTest.SetUp();
        }

        [Test]
        public void RunVerifyLogoutTest()
        {
            sdLogoutTest.VerifyLogout();
        }

        [Test]
        public void RunVerifyLoginFailAnPassTest()
        {
            sdLoginBlockedUserTest.VerifyLoginFailAnPass();
        }

        [Test]
        public void RunVerifyProductPricesInCartTest()
        {
            sdPriceProductsCartTest.VerifyProductPricesInCart();
        }

        [TearDown]
        public void TearDown()
        {
            sdLogoutTest.TearDown();
            sdLoginBlockedUserTest.TearDown();
            sdPriceProductsCartTest.TearDown();
        }
    }
}