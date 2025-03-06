using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using MiProyectoPruebas.Elements;
using MiProyectoPruebas.Framework;
using MiProyectoPruebas.Utils;

namespace MiProyectoPruebas
{
    public class SDHomePage : BasePage
    {
        private readonly IConfiguration config;

        public SDHomePage(IWebDriver driver, IConfiguration config) : base(driver)
        {
            this.config = config;
        }

        public IWebElement GetProductPriceLocator(string productName)
        {
            string dynamicXPath = $"//div[contains(text(), '{productName}')]/ancestor::div[@class='inventory_item_description']//div[@class='inventory_item_price']";
            return FindElement(By.XPath(dynamicXPath));
        }

        public IWebElement GetAddToCartButtonLocator(string productName)
        {
            string dynamicXPath = $"//div[normalize-space(text())='{productName}']/ancestor::div[@class='inventory_item_description']//button[text()='Add to cart']";
            return FindElement(By.XPath(dynamicXPath));
        }


        public void ClickCartIcon()
        {
            Click(SDHomePageElements.cart);
        }

        public void SelectProducts(string[] products)
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
                Logger.LogAction($"{priceElement}");
            }
            return listPrices;
        }
    }
}