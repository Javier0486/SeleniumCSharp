using MiProyectoPruebas.Elements;
using MiProyectoPruebas.Framework;
using OpenQA.Selenium;

namespace MiProyectoPruebas.Pages
{
    /** Interface Segregation Principle (ISP)
        Clients should not be forced to depend on interfaces they do not use
        Implements only the interfaces it needs (e.g. IClickable), so it is not forced to depend on methods it doesn't use.
        It works by defining small interfaces and having each class implement only what it needs, avoiding "fat" interfaces and unnecessary dependencies.
    */
    public class HomePage : BasePage, IClickable
    {
        private readonly IWebDriver driver;

        public HomePage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public void ClickDynamicID()
        {
            Click(HomeElements.DynamicContentLink);
        }

        public void ClickDropdown()
        {
            Click(HomeElements.DropdownLink);
        }

        public void ClickCheckbox()
        {
            Click(HomeElements.CheckboxLink);
        }
    }
}