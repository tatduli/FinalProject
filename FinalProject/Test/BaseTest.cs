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
        public static BlacksCoUkDefaultPage blacksCoUkPage;
        public static OneProductPage oneProductPage;
        public static WomenClothingPage womenClothingPage;
        public static SocialNetworksPage socialNetworksPage;
        public static BasketPage basketPage;
        //public static WomenClothingAndBasketPage naujas;
       

        [OneTimeSetUp]
        public static void SetUp()
        {
            driver = CustomDriver.GetChromeDriver();
            blacksCoUkPage = new BlacksCoUkDefaultPage(driver);
            oneProductPage = new OneProductPage(driver);
            womenClothingPage = new WomenClothingPage(driver);
            socialNetworksPage = new SocialNetworksPage(driver);
            basketPage = new BasketPage(driver);
            //naujas = new WomenClothingAndBasketPage(driver);            
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