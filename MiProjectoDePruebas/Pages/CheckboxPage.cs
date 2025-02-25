using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using MiProyectoPruebas.Elements;
using SeleniumExtras.WaitHelpers;

namespace MiProyectoPruebas
{
    public class CheckboxPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly By checkboxLocator = CheckboxElements.checkboxes;

        public CheckboxPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public bool IsChecboxesHeaderDisplayed()
        {
            return driver.FindElement(CheckboxElements.CheckboxesHeader).Displayed;
        }

        public bool IsCheckboxChecked(int index)
        {
            wait.Until(ExpectedConditions.ElementExists(checkboxLocator));
            var checkboxes = driver.FindElements(CheckboxElements.checkboxes);

            if(index > 0 && index <= checkboxes.Count)
            {
                return checkboxes[index - 1].Selected;
            }
            throw new IndexOutOfRangeException($"No hay un checkbox en la posicion {index} de checkboxes: {checkboxes.Count}");
        }

        public void selectCheckboxByClicking(int index)
        {
            wait.Until(ExpectedConditions.ElementExists(checkboxLocator));
            var checkboxSelect = driver.FindElements(CheckboxElements.checkboxes);

            checkboxSelect[index - 1].Click();
        }
    }
}