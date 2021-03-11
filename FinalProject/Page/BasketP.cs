using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Page
{
    public class BasketP : BasePage
    {
        private IWebElement _searchField => Driver.FindElement(By.Id("productsearch"));
        private IWebElement _searchButton => Driver.FindElement(By.CssSelector(".input-submit:nth-child(4)"));
        private IReadOnlyCollection<IWebElement> _productList => Driver.FindElements(By.CssSelector(".product-img-box"));
        public BasketP(IWebDriver webDriver) : base(webDriver) { }

        public void ChlickAndWriteOnSearchField(string text)
        {
            _searchField.Click();
            _searchField.SendKeys(text);
        }
        public void ClickSearchButton()
        {
            _searchButton.Click();
        }

        public void ChlickOnRandomProduct()
        {
            List<IWebElement> productList = new List<IWebElement>(_productList);
            Random random = new Random();
            int randomElementIndex = random.Next(productList.Count);
            productList[randomElementIndex].Click();
        }
    }
}
