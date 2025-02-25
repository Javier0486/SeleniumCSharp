using MiProyectoPruebas.Elements;
using OpenQA.Selenium;

namespace MiProyectoPruebas.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickDynamicID()
        {
            driver.FindElement(HomeElements.DynamicContentLink).Click();
        }

        public void ClickDropdown()
        {
            driver.FindElement(HomeElements.DropdownLink).Click();
        }

        public void ClickCheckbox()
        {
            driver.FindElement(HomeElements.CheckboxLink).Click();
        }
    }
}