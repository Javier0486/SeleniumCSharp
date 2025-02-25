using OpenQA.Selenium;

namespace MiProyectoPruebas.Elements
{
    public class HomeElements
    {
        public static By DynamicContentLink = By.XPath("//a[normalize-space(text())= 'Dynamic Content']");
        public static By DropdownLink = By.XPath("//a[normalize-space(text())= 'Dropdown']");
        public static By CheckboxLink = By.XPath("//a[normalize-space(text())= 'Checkboxes']");
    }
}