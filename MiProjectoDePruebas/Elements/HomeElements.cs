using OpenQA.Selenium;

namespace MiProyectoPruebas.Elements
{
    public class HomeElements
    {
        public static By DynamicContentLink = By.XPath("//a[normalize-space(text())= 'Dynamic Content']");
    }
}