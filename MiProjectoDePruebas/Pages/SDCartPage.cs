using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using MiProyectoPruebas.Elements;
using SeleniumExtras.WaitHelpers;

namespace MiProyectoPruebas
{
    public class SDCartPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly IConfiguration config;

        public SDCartPage(IWebDriver driver, IConfiguration config)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            this.config = config;
        }

        public IWebElement productPriceInCartLocator(string productName)
        {
            string dynamicXPath = $"//div[normalize-space(text())='{productName}']/ancestor::div[@class='cart_item_label']//div[@class='inventory_item_price']";
            return driver.FindElement(By.XPath(dynamicXPath));
        }

        public bool isPriceDisplayed(string[] products, List<string> pricesFromHomepage)
        {
            for (int i=0; i<products.Length; i++)
            {
                // localize the price in the cart based in the product name
                var priceElementInCart = productPriceInCartLocator(products[i]);
                //verify if the price text in the cart is equal to the price in the homepage
                if(priceElementInCart.Text.Trim() != pricesFromHomepage[i].Trim())
                {
                    Console.WriteLine($"the price of the product '{products[i]}' doesn't match: " + $"Homepage: {pricesFromHomepage}, CartPage: {priceElementInCart.Text}");
                    return false; //if any price doesn't match, return false
                }
            }

            //if all elements where found, return true
            return true;
        }
    }
}