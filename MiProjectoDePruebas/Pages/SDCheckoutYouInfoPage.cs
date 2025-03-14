using OpenQA.Selenium;
using MiProyectoPruebas.Elements;
using MiProyectoPruebas.Framework;

namespace MiProyectoPruebas
{
    public class SDCheckoutYourInfoPage : BasePage
    {
        public SDCheckoutYourInfoPage(IWebDriver driver) : base(driver) {}

        public void FillInfo(string firstName, string lastName, string zipCode)
        {
            EnterText(SDCheckoutYourInfoElements.FirstNameInput, firstName);
            EnterText(SDCheckoutYourInfoElements.LastNameInput, lastName);
            EnterText(SDCheckoutYourInfoElements.ZipCodeInput, zipCode);
        }

        public void Continue()
        {
            Click(SDCheckoutYourInfoElements.ContinueButton);
        }

        public void Cancel()
        {
            Click(SDCheckoutYourInfoElements.CancelButton);
        }
    }
}