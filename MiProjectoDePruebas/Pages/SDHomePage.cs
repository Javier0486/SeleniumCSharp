using OpenQA.Selenium;
using MiProyectoPruebas.Elements;
using MiProyectoPruebas.Framework;
using MiProyectoPruebas.Utils;
using OpenQA.Selenium.Support.UI;

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
            List<string> listProducts = new List<string>();
            var productElements = Driver.FindElements(SDHomePageElements.elementsInHomePageLocator);
            foreach (var element in productElements)
            {
                listProducts.Add(element.Text);
                Logger.LogAction($"{listProducts}");
            }
            return listProducts;
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

        public bool verifySortedAtoZ(List<string> listOne, List<string> listTwo)
        {
            bool areEqual = listOne.SequenceEqual(listTwo);
            return areEqual;
        }
    }
}