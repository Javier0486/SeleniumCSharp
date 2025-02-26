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
        private readonly By checkboxLocator = CheckboxPageElements.checkboxes;

        public CheckboxPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public bool IsChecboxesHeaderDisplayed()
        {
            return driver.FindElement(CheckboxPageElements.CheckboxesHeader).Displayed;
        }

        public bool IsCheckboxChecked(int index)
        {
            wait.Until(ExpectedConditions.ElementExists(checkboxLocator));
            var checkboxList = driver.FindElements(CheckboxPageElements.checkboxes);

            if(checkboxList.Count == 0)
            {
                throw new InvalidOperationException("No se encontraron checkboxes en la pagina");
            }

            if(index < 1 || index > checkboxList.Count)
                throw new ArgumentOutOfRangeException(nameof(index), $"El indice {index} esta fuera del rango valido (1-{checkboxList.Count})");
            
            return checkboxList[index - 1].Selected;
        }

        public void selectCheckboxByClicking(int index)
        {
            wait.Until(ExpectedConditions.ElementExists(checkboxLocator));
            var checkboxSelect = driver.FindElements(checkboxLocator);

            checkboxSelect[index - 1].Click();
        }
    }
}