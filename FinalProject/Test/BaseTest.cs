using FinalProject.Driver;
using FinalProject.Page;
using FinalProject.Tools;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Test
{
    public class BaseTest
    {        
            public static IWebDriver driver;
            public static BlacksCoUkPage blacksCoUkPage;
            public static OneProductPage oneProductPage;
            public static BasketP basket;

        [OneTimeSetUp]
            public static void SetUp()
            {
                driver = CustomDriver.GetIncognitoChrome();
                blacksCoUkPage = new BlacksCoUkPage(driver);
                oneProductPage = new OneProductPage(driver);
                basket = new BasketP(driver);
            }

        
        
        [TearDown]
        public static void TakeScreenshot()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                MyScreenshot.MakeScreenshot(driver);
        }

        [OneTimeTearDown]
            public static void TearDown()
            {
                //driver.Quit();
            }
        }
}
