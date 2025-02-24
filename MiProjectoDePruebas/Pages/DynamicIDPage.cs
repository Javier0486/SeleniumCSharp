using OpenQA.Selenium;
using MiProyectoPruebas.Elements;

namespace MiProyectoPruebas.Pages
{
    public class DynamicIDPage
    {
        private readonly IWebDriver driver;

        public DynamicIDPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool IsDynamicHeaderDisplayed()
        {
            return driver.FindElement(DynamicContentElements.DynamicContentHeader).Displayed;
        }
    }
}