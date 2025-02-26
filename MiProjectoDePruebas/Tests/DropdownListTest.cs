using NUnit.Framework;
using MiProyectoPruebas.Pages;
using MiProyectoPruebas.Utils;

namespace MiProyectoPruebas.Tests
{
    public class DrpdownListTest : TestBase
    {
        private HomePage homePage;
        private DropdownListPage dropdownPage;

        [SetUp]
        public void TestSetup()
        {
            driver.Navigate().GoToUrl(baseUrl);
            homePage = new HomePage(driver);
            dropdownPage = new DropdownListPage(driver);
        }

        [Test]
        public void VerifyDropdownOptionsAndSelection()
        {
            homePage.ClickDropdown();

            List<string> expectedOptions = new List<string> {
                "Please select an option", 
                "Option 1", 
                "Option 2"
            };

            List<string> actualOption = dropdownPage.GetDropdownOptions();
            Assert.That(actualOption, Is.EqualTo(expectedOptions), "Las opciones en el dropdown no son las esperadas");

            Assert.That(dropdownPage.IsDefaultOptionDisabled(), Is.True, "La opcion por defecto no esta deshabilitada.");

            dropdownPage.SelectOptionByValue("1");
            Assert.That(dropdownPage.IsOptionSelected("1"), Is.True, "La opcion 1 no se selecciono correctamente.");

            dropdownPage.SelectOptionByValue("2");
            Assert.That(dropdownPage.IsOptionSelected("2"), Is.True, "La opcion 1 no se selecciono correctamente.");
        }
    }
}