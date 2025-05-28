using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using MiProyectoPruebas.Utils;

namespace MiProyectoPruebas.Framework
{
    /** Single Responsibility Principle (SRP)
    Each class should have only one reason to change
    Handles common web page actions and utilities that are shared across all page objects (e.g. methods for entering text, clicking elements, waiting for elements).
    Provides a foudation for all other page classes to inherit common functionality, reducing code duplication
    keeps code maintainable and easy to understand. Each class has a clear purpose.

    Open/Close Principle (OCP)
    Classes should be open for extension, but close for modification
    Can be extended by other page classes without modifying the base.
    New features can be added (like new pages or browsers) without changing existing code.

    Liskov Substitution Principle (LSP)
    Derived classes must be substitutable for their base classes
    Any class inheriting from BasePage can be used wherever a BasePage is expected, ensuring correct behavior.
    Ensures that all page objects can be used interchangeably if they inherit from BasePage.

    Interface Segregation Principle (ISP)
    Clients should not be forced to depend on interfaces they do not use
    Implements the interfaces (IClickable, ITextEntry) and provides public methods for those actions.
    Any page class that inherits from BasePage and needs those actions can use them
    It works by defining small interfaces and having each class implement only what it needs, avoiding "fat" interfaces and unnecessary dependencies.
*/
    public abstract class BasePage : IClickable, ITextEntry
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;

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

        public void Click(By locator)
        {
            Logger.LogAction($"Clicking the element with locator: {locator}");
            FindElement(locator).Click();
        }

        public void EnterText(By locator, string text)
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