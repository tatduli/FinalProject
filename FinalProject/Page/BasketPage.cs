using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FinalProject.Page
{
    public class BasketPage : BasePage
    {
        private const string UrlAddress = "https://www.blacks.co.uk/basket/";

        private IWebElement _standardDeliveryPrice => Driver.FindElement(By.XPath("//label/span/span[2]"));
        private IWebElement _totalPriceInBasket => Driver.FindElement(By.CssSelector(".basket_total"));
        private IWebElement _scrollMouseToSubtotal => Driver.FindElement(By.CssSelector(".basket_totals_container"));
        private IWebElement _viewBasketButton => Driver.FindElement(By.CssSelector(".cta.btn-buy-process-primary.ga-ip"));
        private IWebElement _miniBasketWindows => Driver.FindElement(By.XPath("//div[@id='miniBagWrapper']/a"));
        private IWebElement _addToBasketButton => Driver.FindElement(By.XPath("//input[@type = 'submit']"));
        private IWebElement _allProducstSize => Driver.FindElement(By.XPath("//ul[@class = 'attribute_value_list group']"));
        private IWebElement _totalProductInBasket => Driver.FindElement(By.CssSelector(".total-items"));
        private IWebElement _productPriceInBasket => Driver.FindElement(By.XPath("//form[@id='basket_form']/table/tbody/tr/td/table/tbody/tr/td[2]/span"));
        private IWebElement _oneProductPriceInBasket => Driver.FindElement(By.XPath("//form[@id='basket_form']/table/tbody/tr/td/table/tbody/tr/td/span/p/span[2]"));
        private IWebElement _buttonIncrease => Driver.FindElement(By.CssSelector("#basket_form > table > tbody > tr > td > table > tbody > tr > td.basket-item > div > span > button.increment"));

        private IReadOnlyCollection<IWebElement> womenClothingCollection => Driver.FindElements(By.CssSelector(".product-item"));
        private IReadOnlyCollection<IWebElement> allProductInBasket => Driver.FindElements(By.XPath("//span[@class = 'basket_item_text']"));

        public BasketPage(IWebDriver webDriver) : base(webDriver) { }

        public BasketPage NavigateToDafaultPage()
        {
            if (Driver.Url != UrlAddress)
                Driver.Url = UrlAddress;
            return this;
        }

        //========================================================================
        //                             CHECK TOTAL PRICE IN THE BASKET
        //========================================================================

        public void AddProductToTheBasket()
        {
            // MessageBoxShow();
            Thread.Sleep(500);
            SelectProduct();
            MouseScrollDownPage(_addToBasketButton);
            SelectSize();

            AddToBasket();
            CloseMiniBasket();
            ViewBasket();
            MouseScrollDownPage(_scrollMouseToSubtotal);
        }

        /// <summary>
        /// Suskaičiuoja krepšialyje esančių prekių bendrą sumą
        /// </summary>
        /// <returns>Grąžina prekių bendrą kainą</returns>
        public double CountTotalPriceInBasket()
        {
            IReadOnlyCollection<IWebElement> allProductPriceInBasket = Driver.FindElements(By.XPath("//tr[@class = 'basket_border']"));// kai iškeliu į išorę vienu mažiau krepšelyje būna
            List<IWebElement> allProductInBasketList = new List<IWebElement>(allProductPriceInBasket);
            double allProductPrice = 0;

            for (int i = 1; i < allProductInBasketList.Count; i++)
            {
                var moneyString = Driver.FindElement(By.XPath("//tr[" + (i).ToString() + "]/td/table/tbody/tr/td[2]/span")).Text.ToString();
                double productPrice = WomenClothingPage.ConvertFromStringToDouble(moneyString);
                allProductPrice += productPrice;
            }
            return allProductPrice;
        }

        public void CheckTotalPrice()
        {
            double calculatedTotalPrice = CountTotalPriceInBasket() + ConvertStandardDeliveryPrice();
            double totalPriceInBasket = ConvertTotalPriceInBasket();

            Assert.AreEqual(calculatedTotalPrice, totalPriceInBasket, $"The total amount varies. As suskaiciuoju {calculatedTotalPrice}" +
                                                                      $" is basketo pareina {totalPriceInBasket}");
        }

        //========================================================================
        //                 Checking product quantities in the basket
        //========================================================================

        public void CheckProductCountInBasket()
        {
            string totalProductCountOnPage = _totalProductInBasket.Text;
            totalProductCountOnPage.Split(' ');
            int totalCountInt = Convert.ToInt32(totalProductCountOnPage[0].ToString());

            int totalCountsTheQuantityOfProducts = CountsTheQuantityOfProducts();

            Assert.AreEqual(totalCountInt, totalCountsTheQuantityOfProducts, $"Product quantities do not match. " +
                                                                             $"On page {totalCountInt}, my calculater" +
                                                                             $" sum {totalCountsTheQuantityOfProducts}");
        }

        public void AddProductsInBasketAndIncrease(int howMuchIncrease)
        {
            SelectProduct();
            MouseScrollDownPage(_addToBasketButton);
            SelectSize();
            AddToBasket();
            ViewBasket();

            GetWait(10);
            for (int i = 0; i < howMuchIncrease - 1; i++)
            {
                Thread.Sleep(1000);
                MouseScrollDownPage(_buttonIncrease);
                Increase();
                Driver.SwitchTo();
            }
        }

        public void CheckProductCountInBasketAfterIncrease(int howMuchIncrease)
        {
            //MessageBoxShow();
            AddProductsInBasketAndIncrease(howMuchIncrease);
            MouseScrollDownPage(_totalProductInBasket);
            CheckProductCountInBasket();
        }

        //========================================================================
        //            Checking product price in the basket after increase
        //========================================================================

        public void CheckProductPriceInBasketAfterIncrease(int howMuchIncrease)
        {
            //MessageBoxShow();
            AddProductsInBasketAndIncrease(howMuchIncrease);
            Thread.Sleep(1000);
            double oneProductPriceInBasketConver = WomenClothingPage.ConvertFromStringToDouble(_oneProductPriceInBasket.Text);
            double productPriceInBasketConver = WomenClothingPage.ConvertFromStringToDouble(_productPriceInBasket.Text);

            double sum = oneProductPriceInBasketConver * howMuchIncrease;

            Assert.AreEqual(sum, productPriceInBasketConver, $"Price isn't correct. as suskaiciuoju {sum}");
        }

        public void Increase()
        {
            _buttonIncrease.Click();
        }

        //========================================================================
        //                              PRIVATE METODE
        //========================================================================

        private void SelectProduct()
        {
            List<IWebElement> productList = new List<IWebElement>(womenClothingCollection);
            int productIndex = RandomProduct(productList);
            ClickOnRandomProduct(productIndex, productList);
        }

        private void SelectSize()
        {
            IReadOnlyCollection<IWebElement> productSizeCollection = _allProducstSize.FindElements(By.TagName("li"));
            List<IWebElement> sizeList = new List<IWebElement>(productSizeCollection);
            int sizeIndex = RandomProductSize(_allProducstSize);
            ClickOnRandomProductSize(sizeIndex, sizeList);
        }

        private void AddToBasket()
        {
            _addToBasketButton.Click();
        }

        private void CloseMiniBasket()
        {
            _miniBasketWindows.Click();
        }

        private void ViewBasket()
        {
            _viewBasketButton.Click();
        }

        private int CountsTheQuantityOfProducts()
        {
            int sum = 0;
            List<IWebElement> productCountList = new List<IWebElement>(allProductInBasket);

            for (int i = 0; i < productCountList.Count; i++)
            {
                string quantityOfOneProduct = Driver.FindElement(By.XPath("//tbody/tr[" + (i + 1).ToString() + "]/td/table/tbody/tr/td/div/span/input")).GetAttribute("value");

                int convertQuantityOfOneProduct = Convert.ToInt32(quantityOfOneProduct);
                sum += convertQuantityOfOneProduct;
            }
            return sum;
        }

        private double ConvertTotalPriceInBasket()
        {
            string totalPriceInString = _totalPriceInBasket.Text;
            double totalPriceInDouble = WomenClothingPage.ConvertFromStringToDouble(totalPriceInString);
            return totalPriceInDouble;
        }

        private double ConvertStandardDeliveryPrice()
        {
            double standardDeliveryPrice = WomenClothingPage.ConvertFromStringToDouble(_standardDeliveryPrice.Text);
            return standardDeliveryPrice;
        }
    }
}
