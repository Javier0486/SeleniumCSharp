using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using MiProyectoPruebas.Elements;
using SeleniumExtras.WaitHelpers;


namespace MiProyectoPruebas
{
    public class DropdownListPage
    {
        private readonly IWebDriver driver;
        private readonly By dropdownLocator = By.Id("dropdown");
        private readonly WebDriverWait wait;

        public DropdownListPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public bool IsDropdownListHeaderDisplayed()
        {
            return driver.FindElement(DropdownListElements.DropdownListHeader).Displayed;
        }

        public bool IsDefaultOptionDisabled()
        {
            wait.Until(ExpectedConditions.ElementExists(dropdownLocator));
            var selectElement = new SelectElement(driver.FindElement(dropdownLocator));
            var defaultOption = selectElement.Options.FirstOrDefault();
            return defaultOption != null && defaultOption.Enabled == false;
        }

        public List<string> GetDropdownOptions()
        {
            wait.Until(ExpectedConditions.ElementExists(dropdownLocator));
            var selectElement = new SelectElement(driver.FindElement(dropdownLocator));
            return selectElement.Options.Select(o => o.Text).ToList();
        }

        public string GetSelectedOption()
        {
            var dropdown = driver.FindElement(dropdownLocator);
            var selectedOption = dropdown.FindElements(By.TagName("option"))
                                            .FirstOrDefault(option => option.Selected);
            return selectedOption != null ? selectedOption.Text : string.Empty;
        }

        public void SelectOptionByValue(string value)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(dropdownLocator));
            var selectElement = new SelectElement(driver.FindElement(dropdownLocator));
            selectElement.SelectByValue(value);
        }

        public bool IsOptionSelected(string value)
        {
            var selectElement = new SelectElement(driver.FindElement(dropdownLocator));
            return selectElement.SelectedOption.GetAttribute("value") == value;
        }
    }
}