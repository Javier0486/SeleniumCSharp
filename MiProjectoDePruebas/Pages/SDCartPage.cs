using OpenQA.Selenium;
using MiProyectoPruebas.Framework;
using MiProyectoPruebas.Utils;
using MiProyectoPruebas.Elements;

namespace MiProyectoPruebas
{
    public class SDCartPage : BasePage
    {
        public SDCartPage(IWebDriver driver) : base(driver) {}

        public IWebElement ProductPriceInCartLocator(string productName)
        {
            string dynamicXPath = $"//div[normalize-space(text())='{productName}']/ancestor::div[@class='cart_item_label']//div[@class='inventory_item_price']";
            return FindElement(By.XPath(dynamicXPath)); // Modification: Use of BasePage method
        }

        public By ProductInCartLocator(string productName)
        {
            string dynamicXPath = $"//div[normalize-space(text())='{productName}']";
            return By.XPath(dynamicXPath); // Use of BasePage method
        }

        public bool isPriceDisplayed(string[] products, List<string> pricesFromHomepage)
        {
            for (int i=0; i<products.Length; i++)
            {
                // localize the price in the cart based in the product name
                var priceElementInCart = ProductPriceInCartLocator(products[i]);
                //verify if the price text in the cart is equal to the price in the homepage
                if(priceElementInCart.Text.Trim() != pricesFromHomepage[i].Trim())
                {
                    Logger.LogAction($"the price of the product '{products[i]}' doesn't match: " + $"Homepage: {pricesFromHomepage}, CartPage: {priceElementInCart.Text}");
                    return false; //if any price doesn't match, return false
                }
            }

            //if all elements where found, return true
            return true;
        }

        public bool isProductDisplayedInCart(string[] products)
        {
            for (int i=0; i<products.Length; i++)
            {
                // localize the product in the cart based in the product name
                var productElementInCart = ProductInCartLocator(products[i]);
                //verify if the product is displayed in the cart
                if(!IsElementDisplayed(productElementInCart))
                {
                    Logger.LogAction($"the product '{products[i]}' is not displayed in the cart");
                    return false; //if any product is not displayed, return false
                }
            }

            return true;
        }

        public void Checkout()
        {
            Click(SDCartElements.CheckoutButton);
        }

    }
}