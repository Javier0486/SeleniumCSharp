using NUnit.Framework;
using MiProyectoPruebas.Pages;
using MiProyectoPruebas.Utils;
using MiProyectoPruebas.Elements;

namespace MiProyectoPruebas.Tests
{
    public class CheckboxTest : TestBase
    {

        private HomePage homePage;
        private CheckboxPage checkboxPage;

        [SetUp]
        public void TestSetup()
        {
            driver.Navigate().GoToUrl(baseUrl);
            homePage = new HomePage(driver);
            checkboxPage = new CheckboxPage(driver);
        }

        [Test]
        public void VerifyCheckboxPageFunctionality()
        {
            homePage.ClickCheckbox();

            Assert.That(checkboxPage.IsChecboxesHeaderDisplayed(), Is.True, "El Header no esta visible");

            var cheboxCount = driver.FindElements(CheckboxPageElements.checkboxes).Count;
            Assert.That(cheboxCount, Is.GreaterThanOrEqualTo(2), "No hay suficientes checkboxes en la pagina");

            Assert.That(checkboxPage.IsCheckboxChecked(1), Is.Not.True, "Checbox 1 esta seleccionado al cargar la pagina");

            Assert.That(checkboxPage.IsCheckboxChecked(2), Is.True, "Checkbox 2 no esta seleccionado al cargar la pagina");

            checkboxPage.selectCheckboxByClicking(1);
            checkboxPage.selectCheckboxByClicking(2);

            Assert.That(checkboxPage.IsCheckboxChecked(1), Is.True, "Checkbox 1 no se selecciono correctamente");

            Assert.That(checkboxPage.IsCheckboxChecked(2), Is.Not.True, "Checkbox 2 esta seleccionado");
        }
    }
}