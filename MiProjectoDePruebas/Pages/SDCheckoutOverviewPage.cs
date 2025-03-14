using OpenQA.Selenium;
using MiProyectoPruebas.Framework;
using MiProyectoPruebas.Utils;
using MiProyectoPruebas.Elements;

namespace MiProyectoPruebas
{
    public class SDCheckoutOverviewPage : BasePage
    {
        public SDCheckoutOverviewPage(IWebDriver driver) : base(driver){}

        public IWebElement ProductInOverviewLocator(string productName)
        {
            string dynamicXPath = $"//div[normalize-space(text())='{productName}']";
            return FindElement(By.XPath(dynamicXPath)); // Modification: Use of BasePage method
        }
        
        public bool isProductInOverview(string[] products)
        {
            for (int i=0; i<products.Length; i++)
            {
                // localize the product in the overview based in the product name
                var productElementInOverview = ProductInOverviewLocator(products[i]);
                //verify if the product text in the overview is equal to the product in the cart
                if(productElementInOverview.Text.Trim() != products[i].Trim())
                {
                    Logger.LogAction($"the product '{products[i]}' is not in the overview");
                    return false; //if any product is not in the overview, return false
                }
            }

            //if all elements where found, return true
            return true;
        }

        public void Finish()
        {
            Click(SDCheckoutOverviewElements.FinishButton);
        }
    }
}