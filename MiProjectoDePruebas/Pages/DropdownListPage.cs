using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
            var dropdown = wait.Until(ExpectedConditions.ElementExists(dropdownLocator));
            var selectElement = new SelectElement(dropdown);
            var defaultOption = selectElement.Options.FirstOrDefault();
            return defaultOption != null && !defaultOption.Enabled;
        }

        public List<string> GetDropdownOptions()
        {
            var dropdown = wait.Until(ExpectedConditions.ElementExists(dropdownLocator));
            var selectElement = new SelectElement(dropdown);
            return selectElement.Options.Select(o => o.Text).ToList();
        }

        public string GetSelectedOption()
        {
            var dropdown = wait.Until(ExpectedConditions.ElementExists(dropdownLocator));
            var selectedOption = dropdown.FindElements(By.TagName("option"))
                                            .FirstOrDefault(option => option.Selected);
            return selectedOption?.Text ?? string.Empty;
        }

        public void SelectOptionByValue(string value)
        {
            var dropdown = wait.Until(ExpectedConditions.ElementToBeClickable(dropdownLocator));
            new SelectElement(dropdown).SelectByValue(value);
        }

        public bool IsOptionSelected(string value)
        {
            var dropdown = wait.Until(ExpectedConditions.ElementExists(dropdownLocator));
            var selectElement = new SelectElement(dropdown);
            return selectElement.SelectedOption.GetAttribute("value") == value;
        }
    }
}