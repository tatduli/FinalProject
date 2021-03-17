using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Page
{
    public class SocialNetworksPage :BasePage
    {
        private IWebElement _youtobeButton => Driver.FindElement(By.XPath("//img[@alt = 'Blacks YouTube']"));
        private IWebElement _twitterButton => Driver.FindElement(By.CssSelector("li:nth-child(1) .footer-social-icon"));       
        private IWebElement _instagramButton => Driver.FindElement(By.XPath("//img[@alt = 'Blacks Instagram']"));
        private IWebElement _facebookButton => Driver.FindElement(By.XPath("//img[@alt = 'Blacks Facebook']"));        
        private IWebElement _lastElementOnPage => Driver.FindElement(By.CssSelector(".copyright"));
    
        public SocialNetworksPage(IWebDriver webDriver) : base(webDriver) { }       

        public void ClickOnSocialButton(string socialNetworkName)
        {
            MouseScrollDownPage(_lastElementOnPage);

            switch(socialNetworkName)
            {
                case "Twitter":
                    _twitterButton.Click();
                    break;
                case "Facebook":
                    _facebookButton.Click();
                    break;
                case "Instagram":
                    _instagramButton.Click();                    
                    break;
                case "YouTube":
                    _youtobeButton.Click();
                    break;
                default:
                    Console.WriteLine("The social network is unknow");
                    break;
            }                
        }
       
        public string OpenSocialNetworksPageAndReturnTitle()
        {
            //get window handlers as list
            List<String> browserTabs = new List<String>(Driver.WindowHandles);
            //switch to new tab
            string browserTitle = Driver.SwitchTo().Window(browserTabs[1]).Title;
            Console.WriteLine(browserTitle);
            return browserTitle;
        }       

        public void CloseSocialNetworks()
        {
            List<String> browserTabs = new List<String>(Driver.WindowHandles);
            Driver.Close();
            Driver.SwitchTo().Window(browserTabs[0]);
        }

        public void TestOrSocialNetworksPageOpened(string browserTitle, Enum siocialNetworksTitle)
        {                   
            Assert.IsTrue(browserTitle.Contains(siocialNetworksTitle.ToString()));
        }
    }
}
