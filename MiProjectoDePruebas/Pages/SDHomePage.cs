using OpenQA.Selenium;
using MiProyectoPruebas.Elements;
using MiProyectoPruebas.Framework;
using MiProyectoPruebas.Utils;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace MiProyectoPruebas
{
    public class SDHomePage : BasePage
    {
        public SDHomePage(IWebDriver driver) : base(driver) { }

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

        public List<string> GetAllProductNamesInHomePage ()
        {
            // Find all product elements using the locator
            var productElements = Driver.FindElements(SDHomePageElements.elementsInHomePageLocator);

            // "Extract non-empty text values using LINQ for cleaner code
            List<string> listProducts = productElements
                .Select(element => element.Text)
                .Where(text => !string.IsNullOrWhiteSpace(text))
                .ToList();
           
           // Log each product name for debugging purposes
            foreach (var element in productElements)
            {
                string productText = element.Text;
                Logger.LogAction($"Product found: {productText}");
            }

            return listProducts;
        }

        public List<string> GetPriceFromAllProductsInHomePage ()
        {
            //Find all product prices using the locator
            var priceElements = Driver.FindElements(SDHomePageElements.pricesInHomePageLocator);

            List<string> listPrices = priceElements
                .Select(element => element.Text)
                .Where(text => !string.IsNullOrWhiteSpace(text))
                .ToList();

            foreach (var element in priceElements)
            {
                string priceText = element.Text;
                Logger.LogAction($"Price found: {priceText}");
            }

            return listPrices;
        }

        public bool LogoutFromSite()
        {
            Click(SDHomePageElements.BurgerMenu);
            Click(SDHomePageElements.LogoutOption);
            return IsElementDisplayed(SDLoginPageElements.UsernameIput);
        }

        public void selectSort(string sortBy)
        {
            Click(SDHomePageElements.SortByLocator);
            By optionLocator = By.XPath($"//option[text()='{sortBy}']");
            Click(optionLocator);
        }

        public bool verifySorted<T>(IEnumerable<T> listOne, IEnumerable<T> listTwo)
        {
            bool areEqual = listOne.SequenceEqual(listTwo);
            return areEqual;
        }

        public List<double> convertToNumber (List<string> prices)
        {
            List<double> pricesAsDoubles = prices
                .Select(price => double.Parse(price.Replace("$", "").Replace(",", "").Trim()))
                .ToList();

            return pricesAsDoubles;
        }

        public List<double> orderLowToHighPrice (List<double> prices)
        {
            List<double> sortedPrices = prices
                .OrderBy(p => p)
                .ToList();

            return sortedPrices;
        }

    }
}