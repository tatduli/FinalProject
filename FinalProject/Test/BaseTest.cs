using FinalProject.Driver;
using FinalProject.Page;
using FinalProject.Tools;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

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

        [OneTimeSetUp]
        public static void SetUp()
        {
            driver = CustomDriver.GetIncognitoChrome();
            blacksCoUkPage = new BlacksCoUkDefaultPage(driver);
            oneProductPage = new OneProductPage(driver);
            womenClothingPage = new WomenClothingPage(driver);
            socialNetworksPage = new SocialNetworksPage(driver);
            basketPage = new BasketPage(driver);            
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
            driver.Quit();
        }
    }
}