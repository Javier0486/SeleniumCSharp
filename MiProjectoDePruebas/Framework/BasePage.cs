using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using MiProyectoPruebas.Utils;

namespace MiProyectoPruebas.Framework
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        protected BasePage(IWebDriver driver)
        {
            this.Driver = driver;
            this.Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        protected IWebElement FindElement(By locator)
        {
            Logger.LogAction($"Searching element using locator: {locator}");
            return Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        protected void Click(By locator)
        {
            Logger.LogAction($"Clicking the element with locator: {locator}");
            FindElement(locator).Click();
        }

        protected void EnterText(By locator, string text)
        {
            Logger.LogAction($"Sending text '{text} to the element with locator: {locator}");
            var element = FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        protected string GetText(By locator)
        {
            Logger.LogAction($"Getting text from the element with locator: {locator}");
            return FindElement(locator).Text;
        }

        protected bool IsElementDisplayed(By locator)
        {
            try
            {
                Logger.LogAction($"Verifying if the element with locator: {locator} is visible");
                return FindElement(locator).Displayed;
            }
            catch (NoSuchElementException)
            {
                Logger.LogAction($"the element with locator: {locator} is not present");
                return false;
            }
        }

        protected void ClearField(By locator)
        {
            Logger.LogAction($"Clearing fields with locator {locator}");
            var element = FindElement(locator); // use the FindElement from BasePage
            element.Clear();
        }

        protected IWebElement WaitUntilElementIsVisible(By locator, int timeoutInSeconds = 10)
        {
            Logger.LogAction($"waiting for the locator: {locator} to be visible");
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        protected IWebElement WaitUntilElementExist(By locator, int timeoutInSeconds = 10)
        {
            Logger.LogAction($"waiting for the locator: {locator} to exist");
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(ExpectedConditions.ElementExists(locator));
        }
    }
}