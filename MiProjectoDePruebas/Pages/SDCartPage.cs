using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using MiProyectoPruebas.Framework;
using MiProyectoPruebas.Utils;

namespace MiProyectoPruebas
{
    public class SDCartPage : BasePage
    {
        private readonly IConfiguration config;

        public SDCartPage(IWebDriver driver, IConfiguration config) : base(driver)
        {
            this.config = config;
        }

        public IWebElement ProductPriceInCartLocator(string productName)
        {
            string dynamicXPath = $"//div[normalize-space(text())='{productName}']/ancestor::div[@class='cart_item_label']//div[@class='inventory_item_price']";
            return FindElement(By.XPath(dynamicXPath)); // Modification: Use of BasePage method
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
    }
}