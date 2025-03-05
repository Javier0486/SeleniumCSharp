using OpenQA.Selenium;

namespace MiProyectoPruebas.Elements
{
    public class SDHomePageElements
    {
        public static By SwagLabsHeader = By.XPath("//div[normalize-space(text())='Swag Labs']");
        public static By cart = By.Id("shopping_cart_container");
    }
}