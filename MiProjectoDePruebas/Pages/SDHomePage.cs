using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using MiProyectoPruebas.Elements;
using SeleniumExtras.WaitHelpers;

namespace MiProyectoPruebas
{
    public class SDHomePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly IConfiguration config;

        public SDHomePage(IWebDriver driver, IConfiguration config)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            this.config = config;
        }

        public IWebElement GetProductPriceLocator(string productName)
        {
            string dynamicXPath = $"//div[contains(text(), '{productName}')]/ancestor::div[@class='inventory_item_description']//div[@class='inventory_item_price']";
            return driver.FindElement(By.XPath(dynamicXPath));
        }

        public IWebElement GetAddToCartButtonLocator(string productName)
        {
            string dynamicXPath = $"//div[normalize-space(text())='{productName}']/ancestor::div[@class='inventory_item_description']//button[text()='Add to cart']";
            return driver.FindElement(By.XPath(dynamicXPath));
        }


        public void clickCartIcon()
        {
            driver.FindElement(SDHomePageElements.cart).Click();
        }

        public void selectProducts(string[] products)
        {
            foreach (var productName in products)
            {
                var addToCartButton = GetAddToCartButtonLocator(productName);

                addToCartButton.Click();
            }
        }

        public List<string> GetPricesFromHomepage (string[] products)
        {
            List<string> listPrices = new List<string>();
            foreach (var productName in products)
            {
                var priceElement = GetProductPriceLocator(productName);
                listPrices.Add(priceElement.Text);
                Console.WriteLine(priceElement);
            }
            return listPrices;
        }
    }
}