using OpenQA.Selenium;

namespace MiProyectoPruebas.Elements
{
    public class SDHomePageElements
    {
        public static By SwagLabsHeader = By.XPath("//div[normalize-space(text())='Swag Labs']");
        public static By cart = By.Id("shopping_cart_container");
        public static By BurgerMenu = By.Id("react-burger-menu-btn");
        public static By LogoutOption = By.Id("logout_sidebar_link");
    }
}