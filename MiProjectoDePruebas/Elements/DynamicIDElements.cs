using OpenQA.Selenium;

namespace MiProyectoPruebas.Elements
{
    public class DynamicContentElements
    {
        public static By DynamicContentHeader = By.XPath("//h3[normalize-space(text())= 'Dynamic Content']");
    }
}