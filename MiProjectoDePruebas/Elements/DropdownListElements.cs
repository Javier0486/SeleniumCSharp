using OpenQA.Selenium;

namespace MiProyectoPruebas.Elements
{
    public class DropdownListElements
    {
        public static By DropdownListHeader = By.XPath("//h3[normalize-space(text())= 'Dropdown List']");
        public static By dropdown = By.Id("dropdown");
    }
}