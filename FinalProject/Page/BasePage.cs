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
        
        public void CloseBrowser()
        {
            Driver.Quit();
        }

    }
}
