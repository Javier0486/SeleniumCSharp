using NUnit.Framework;
using MiProyectoPruebas.Pages;
using MiProyectoPruebas.Utils;

namespace MiProyectoPruebas.Tests
{
    public class DynamicIDTest : TestBase
    {
        [Test]
        public void VerifyDynamicHeaderIsDisplayed()
        {
            driver.Navigate().GoToUrl(baseUrl);

            HomePage homePage = new HomePage(driver);
            homePage.ClickDynamicID();

            DynamicIDPage dynamicIDPage = new DynamicIDPage(driver);
            Assert.That(dynamicIDPage.IsDynamicHeaderDisplayed(), Is.True, "El header Dynamic Content no se muestra.");
        }
    }
}