using OpenQA.Selenium;
using MiProyectoPruebas.Framework;
using MiProyectoPruebas.Elements;
using MiProyectoPruebas.Utils;

namespace MiProyectoPruebas
{
    public class SDCheckoutCompletePage : BasePage
    {
        public SDCheckoutCompletePage(IWebDriver driver) : base(driver) {}

        public bool AreHeaderAndTextDisplayed(string header, string text)
        {
            var headerElement = FindElement(SDCheckoutCompleteElements.CompleteHeader);
            var textElement = FindElement(SDCheckoutCompleteElements.CompleteText);
            if(headerElement.Text.Trim() != header.Trim() || textElement.Text.Trim() != text.Trim())
            {
                Logger.LogAction($"The header or text doesn't match - Expected: {header} - {text} - Actual: {headerElement.Text} - {textElement.Text}");
                return false;
            }
            return true;
        }

        public void BackHome()
        {
            Click(SDCheckoutCompleteElements.BackHomeButton);
        }

    }
}