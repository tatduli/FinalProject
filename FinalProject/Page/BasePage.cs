using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Page
{
    public class BasePage
    {
        protected static IWebDriver Driver;


        public BasePage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        public WebDriverWait GetWait(int seconds = 5)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
            return wait;
        }

        public BasePage DismissConfirmationAlert()
        {
            IAlert alert = Driver.SwitchTo().Alert();
            alert.Dismiss();
            return this;
        }

        public BasePage MouseScrollDownPage(IWebElement element)
        {
            Actions actions = new Actions(Driver);
            actions.MoveToElement(element);
            actions.Perform();
            return this;
        }

        /// <summary>
        /// Randą pasirinktinai produktą iš sąrašo
        /// </summary>
        /// <returns>grąžina produkto vietą sąraše</returns>
        public int RandomProduct(List<IWebElement> productList)
        {            
            Random random = new Random(); 
            int randomElementIndex = random.Next(productList.Count);
            return randomElementIndex;
        }

        /// <summary>
        /// išrenkamas atsitiktinis produktas
        /// </summary>
        /// <param name="randomElementIndex"></param>
        public void ClickOnRandomProduct(int randomElementIndex, List<IWebElement> productList)
        {            
            productList[randomElementIndex].Click(); //+1
        }

        public int RandomProductSize(IWebElement _allProducstSize)
        {
            Random random = new Random();
            IReadOnlyCollection<IWebElement> productSizeCollection = _allProducstSize.FindElements(By.TagName("li"));
            
            int randomSizeIndex = random.Next(productSizeCollection.Count);
            
            return randomSizeIndex;
        }

        /// <summary>
        /// išrenkamas atsitiktis rūbo dydis
        /// </summary>
        /// <param name="randomElementIndex">atsitiktinai išrinkto rūbo dydžio indeksas</param>
        public void ClickOnRandomProductSize(int randomElementIndex, List<IWebElement> sizeList)
        {
            sizeList[randomElementIndex].Click();
        }

        public void CloseBrowser()
        {
            Driver.Quit();
        }

    }
}
