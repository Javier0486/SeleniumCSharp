using OpenQA.Selenium;

namespace MiProyectoPruebas.Elements
{
    public class CheckboxPageElements
    {
        public static By CheckboxesHeader = By.XPath("//h3[normalize-space(text())= 'Checkboxes']");
        public static By checkboxes = By.XPath("//input[@type='checkbox']");
    }
}