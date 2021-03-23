using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Page
{
    public class OneProductPage : BasePage
    {
        private const string UrlAddress = "https://www.blacks.co.uk/basket/";       
      
        IReadOnlyCollection<IWebElement> findedProductList => Driver.FindElements(By.CssSelector(".product-img-box"));
        private IWebElement _productNameOnNewPage => Driver.FindElement(By.CssSelector(".product-name"));
        private IWebElement _productSalePriceOnNewPage => Driver.FindElement(By.CssSelector(".special"));
        private IWebElement _productRegularPriceOnNewPage => Driver.FindElement(By.CssSelector(".regular-price"));        

        public OneProductPage(IWebDriver webDriver) : base(webDriver) 
        {  }

        public OneProductPage NavigateToDafaultPage()
        {
            if (Driver.Url != UrlAddress)
                Driver.Url = UrlAddress;
            return this;
        }

        public int RandomProduct()
        {
            List<IWebElement> productList = new List<IWebElement>(findedProductList);
            Random random = new Random();
            int randomElementIndex = random.Next(productList.Count);            
            return randomElementIndex;
        }
        public void ClickOnRandomProduct(int randomElementIndex)
        {
            List<IWebElement> productList = new List<IWebElement>(findedProductList);
            productList[randomElementIndex].Click();
        }
                
        public (string, string) ReturnSelectedProductDescriptionAndPriceAndClick()
        {
            int randomElementIndex = RandomProduct();
            string nameSelectedItem = Driver.FindElement(By.XPath("//article[" + (randomElementIndex + 1).ToString() + "]/div/a/span/h2/span[2]")).Text;
            string priceSelectedItem = Driver.FindElement(By.XPath("//article[" + (randomElementIndex + 1).ToString() + "]/div/a/span/span")).Text;
          
            ClickOnRandomProduct(randomElementIndex);            
            return (nameSelectedItem, priceSelectedItem);
        }

        /// <summary>
        /// TryCatch, nes yra sale kaina ir regular kaina
        /// </summary>
        public void TestSelectedProductDescriptionAndPriceOnOpenedPage()
        {
            (string, string) randomProduct = ReturnSelectedProductDescriptionAndPriceAndClick();
            var selectedProductName = randomProduct.Item1;
            Assert.AreEqual(selectedProductName, _productNameOnNewPage.Text, "product names do not match");
            try
            {
                Assert.AreEqual(randomProduct.Item2, _productSalePriceOnNewPage.Text, "product price do not match");
            }
            catch (Exception exception)
            {
                if (exception is NullReferenceException || exception is NoSuchElementException)
                {
                    Assert.AreEqual(randomProduct.Item2, _productRegularPriceOnNewPage.Text, "product price do not match");
                }
            }
        }            
        
    }
}
