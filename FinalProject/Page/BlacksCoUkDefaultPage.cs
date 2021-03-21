﻿using FinalProject.Enumeration;
using FinalProject.List;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject.Page
{
    public class BlacksCoUkDefaultPage : BasePage
    {
        private const string EmailDataFile = "\\DataAndResult\\InvalidEmailData.txt";
        private const string EmailResultFile = "\\DataAndResult\\EmailResultFile.txt";
        private const string UrlAddress = "https://www.blacks.co.uk";
      
        private IWebElement _searchField => Driver.FindElement(By.Id("productsearch"));
        private IWebElement _searchButton => Driver.FindElement(By.CssSelector(".input-submit:nth-child(4)"));
        private IWebElement _searchResulText => Driver.FindElement(By.XPath("//div[@id='grid-title']/h1"));
        private IWebElement _searchResulMeniuText1 => Driver.FindElement(By.XPath("//div[@class='landing-page title']/h1"));       
        private IWebElement _searchResulMeniuText3 => Driver.FindElement(By.XPath("//a[@class='blog-home']"));        
        IReadOnlyCollection<IWebElement> allButton => Driver.FindElements(By.CssSelector(".nav-level-1"));
        private IWebElement _submitEmailField => Driver.FindElement(By.Id("email_signup"));
        private IWebElement _submitEmailButton => Driver.FindElement(By.XPath("//div[@id='subscribe-info']/form/fieldset/input"));
        private IWebElement _lastElementOnPage => Driver.FindElement(By.CssSelector(".copyright"));       
        private IWebElement _popupWindows => Driver.FindElement(By.Id("cboxContent"));
        private IWebElement _popupWindowsClose => Driver.FindElement(By.Id("cboxClose"));

        private string dataFile = TestContext.CurrentContext.TestDirectory + EmailDataFile;
        private string resultFile = TestContext.CurrentContext.TestDirectory + EmailResultFile;

        public BlacksCoUkDefaultPage(IWebDriver webDriver) : base(webDriver) { }

        public BlacksCoUkDefaultPage NavigateToDafaultPage()
        {
            if (Driver.Url != UrlAddress)
                Driver.Url = UrlAddress;
            return this;
        }

        //=========================================================
        //      Tests search field by brands
        //=========================================================

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

        public void CheckBrandResultThroughTheSearchField(string expentedBrandResult)
        {

            Assert.True(expentedBrandResult.ToLower().Contains(
                    _searchResulText.Text.ToLower()), "There is no such BRAND");
        }

        //=========================================================
        //                  Tests all button
        //=========================================================

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

        //=========================================================
        //                  Test email from txt file
        //                  Print result to txt file
        //=========================================================
               

        public void CheckEmailSubmitFieldFromDataFile()
        {
            const string top = "|-----------------------------------------------|-------------|-------------|\r\n" 
                             + "|                  Email                        |   Expected  |   Result    | \r\n"
                             + "|-----------------------------------------------|-------------|-------------|";

            if (File.Exists(resultFile))
            {
                File.Delete(resultFile);
            }
            using (StreamWriter writeText = new StreamWriter(resultFile))
            {
                if (File.Exists(dataFile))
                {
                    string[] allEmail = File.ReadAllLines(dataFile);
                    bool emailBool = true;
                    writeText.WriteLine(top);

                    foreach (string email in allEmail)
                    {
                        MouseScrollDownPage(_lastElementOnPage);
                        _submitEmailField.Click();
                        _submitEmailField.SendKeys(email);
                        _submitEmailButton.Click();

                        Thread.Sleep(2000);                        

                        if (!_popupWindows.Displayed || _popupWindows.Text.Contains("The entered email address is not valid"))
                        {
                            if(_popupWindows.Text.Contains("The entered email address is not valid"))
                            {
                                _popupWindowsClose.Click();
                            }
                            //writeText.WriteLine($"{email} is bad, test result is good");
                            writeText.WriteLine("| {0, -45} | {1, -10}   | {2, -10} |", email, "Invalid", "Passed");                            
                        }
                        else
                        {
                            _popupWindowsClose.Click();
                            emailBool = false;
                            // writeText.WriteLine($"{email} is bad, test result is bad");
                            writeText.WriteLine("| {0, -45} | {1, -10}   | {2, -10} |", email, "Invalid", "Failed");
                        }

                        _submitEmailField.Clear();
                    }
                    Assert.IsTrue(emailBool, "Some email incorrect. Look in file EmailResultFile.txt");
                }
                else
                    Console.WriteLine("Data file does not exist");
            }          
        }

        //public void CheckBrandResultThroughTheSearchField()
        //{ IWebElement blockA = Driver.FindElement(By.Id("brands_A"));
        //ICollection<IWebElement> list = blockA.FindElements(By.TagName("li"));
        //    Assert.AreEqual(Driver.Url, BrandsUrlAddress, "Address isn't corect");

        //}

    }
}
