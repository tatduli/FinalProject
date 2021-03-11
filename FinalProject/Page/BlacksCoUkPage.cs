using FinalProject.Enumeration;
using NUnit.Framework;
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
    public class BlacksCoUkPage : BasePage
    {
         private const string UrlAddress = "https://www.blacks.co.uk";
        
        //private const string BrandsUrlAddress = "https://www.blacks.co.uk/brands";
        private IWebElement _searchField => Driver.FindElement(By.Id("productsearch"));
        private IWebElement _searchButton => Driver.FindElement(By.CssSelector(".input-submit:nth-child(4)"));
        private IWebElement _searchResulText => Driver.FindElement(By.XPath("//div[@id='grid-title']/h1"));
        private IWebElement _searchResulMeniuText1 => Driver.FindElement(By.XPath("//div[@class='landing-page title']/h1"));       
        private IWebElement _searchResulMeniuText3 => Driver.FindElement(By.XPath("//a[@class='blog-home']"));        
        IReadOnlyCollection<IWebElement> allButton => Driver.FindElements(By.CssSelector(".nav-level-1"));
        private IWebElement _twitterButton => Driver.FindElement(By.CssSelector("li:nth-child(1) .footer-social-icon"));
        private IWebElement _lastElementOnPage => Driver.FindElement(By.CssSelector(".copyright"));





        public BlacksCoUkPage(IWebDriver webDriver) : base(webDriver) { }

        public BlacksCoUkPage NavigateToDafaultPage()
        {
            if (Driver.Url != UrlAddress)
                Driver.Url = UrlAddress;
            return this;
        }
        /// <summary>
        /// brand'ą ieškome
        /// </summary>
        /// <param name="text">Brand'o pavadinimas</param>
        public void ChlickAndWriteOnSearchField(string text)
        {
            _searchField.Click();
            _searchField.SendKeys(text);
        }

        public void ClickSearchButton()
        {
            _searchButton.Click();
        }

        public void ClickSelectedButton(Enum meniuButtonName)
        {           
            foreach (IWebElement button in allButton)
            {                
                string span = button.FindElement(By.TagName("span")).Text;
                Console.WriteLine(span);
                if (span == meniuButtonName.ToString())
                {
                    button.Click();
                    break;
                }                   
            }
        }

        public void CheckOrALLMeniuWork(Enum meniuBottonName)
        {
            if (meniuBottonName.ToString() == MeniuEnumeration.Activities.ToString() ||
                meniuBottonName.ToString() == MeniuEnumeration.Cycling.ToString())
                CheckOrMeniuWork(meniuBottonName, _searchResulText);
            else if (meniuBottonName.ToString() == MeniuEnumeration.Blog.ToString())
                CheckOrMeniuWork(meniuBottonName, _searchResulMeniuText3);
            else 
                CheckOrMeniuWork(meniuBottonName, _searchResulMeniuText1);          
        }

        public void CheckOrMeniuWork(Enum meniuBottonName, IWebElement element)
        {
            Console.WriteLine(element.Text);            
            Assert.True(element.Text.ToLower().Contains(
                         meniuBottonName.ToString().ToLower()), "The page isn't correct");           
        }

        
        public void CheckBrandResultThroughTheSearchField(string expentedBrandResult)
        {
            
                Assert.True(expentedBrandResult.ToLower().Contains(
                        _searchResulText.Text.ToLower()), "There is no such BRAND");
        }

        //public void CheckBrandResultThroughTheSearchField()
        //{ IWebElement blockA = Driver.FindElement(By.Id("brands_A"));
        //ICollection<IWebElement> list = blockA.FindElements(By.TagName("li"));
        //    Assert.AreEqual(Driver.Url, BrandsUrlAddress, "Address isn't corect");

        //}

        public void CheckOrSocialButtonWork()
        {
            MouseScrollDownPage(_lastElementOnPage);            
            _twitterButton.Click();
            var naujas= Driver.SwitchTo().ActiveElement();
            
            Console.WriteLine(naujas.Equals("https://twitter.com/blacks_online")); 

            //Assert.AreEqual(Driver.Url, twitter);

        }


    }
}
