using NUnit.Framework;
using MiProyectoPruebas.Pages;
using MiProyectoPruebas.Utils;

namespace MiProyectoPruebas.Tests
{
    public class CheckboxTest : TestBase
    {
        [Test]
        public void VerifyCheckboxPageFunctionality()
        {
            driver.Navigate().GoToUrl(baseUrl);

            HomePage homePage = new HomePage(driver);
            homePage.ClickCheckbox();

            CheckboxPage checkboxPage = new CheckboxPage(driver);

            Assert.That(checkboxPage.IsChecboxesHeaderDisplayed(), Is.True, "El Header no esta visible");

            Assert.That(checkboxPage.IsCheckboxChecked(1), Is.Not.True, "Checbox 1 esta seleccionado al cargar la pagina");

            Assert.That(checkboxPage.IsCheckboxChecked(2), Is.True, "Checkbox 2 no esta seleccionado al cargar la pagina");

            checkboxPage.selectCheckboxByClicking(1);
            checkboxPage.selectCheckboxByClicking(2);

            Assert.That(checkboxPage.IsCheckboxChecked(1), Is.True, "Checkbox 1 no se selecciono correctamente");

            Assert.That(checkboxPage.IsCheckboxChecked(2), Is.Not.True, "Checkbox 2 esta seleccionado");
        }
    }
}