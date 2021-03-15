using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Page
{
    public class WomenClothingPage : BasePage
    {
        private const string UrlAddress = "https://www.blacks.co.uk/womens/womens-clothing/";

        IReadOnlyCollection<IWebElement> womenClothingCollection => Driver.FindElements(By.CssSelector(".product-item"));     
        private IWebElement _addToBasketButton => Driver.FindElement(By.XPath("//input[@type = 'submit']"));
        private IWebElement _allProducstSize => Driver.FindElement(By.XPath("//ul[@class = 'attribute_value_list group']"));
        private IWebElement _selectedSize => Driver.FindElement(By.CssSelector(".selected-size"));
        private IWebElement _viewBasketButton => Driver.FindElement(By.CssSelector(".cta.btn-buy-process-primary.ga-ip"));
        IReadOnlyCollection<IWebElement> allProductInBasket => Driver.FindElements(By.XPath("//span[@class = 'basket_item_text']"));        
        private IWebElement _miniBasketWindows => Driver.FindElement(By.XPath("//div[@id='miniBagWrapper']/a"));

        IReadOnlyCollection<IWebElement> allProductPrice = Driver.FindElements(By.XPath("//article"));
        IReadOnlyCollection<IWebElement> allProductPriceInBasket = Driver.FindElements(By.XPath("//tr[@class = 'basket_border']"));
        private IWebElement _allProductBrand => Driver.FindElement(By.XPath("//ul[@class = 'facet-list brand_list filter_container ac_filter_static template_nav_filter_overflow']"));
        private IWebElement _scrollMouseToSize => Driver.FindElement(By.CssSelector(".facet_size:nth-child(5) .no-ajax"));
        private IWebElement _totalPriceInBasket => Driver.FindElement(By.CssSelector(".basket_total"));
        // private IWebElement _totalPriceInBasket => Driver.FindElement(By.CssSelector(".basket_black"));
        private IWebElement _standardDeliveryCheckBox => Driver.FindElement(By.Id("delivery_option_87"));
        private IWebElement _nextDayDeliveryCheckBox => Driver.FindElement(By.Id("delivery_option_1031"));
        private IWebElement _standardDeliveryPrice => Driver.FindElement(By.XPath("//label/span/span[2]"));
        private IWebElement _nextDayDeliveryPrice => Driver.FindElement(By.XPath("//div[2]/label/span/span[2]"));
        private IWebElement _scrollMouseToSubtotal => Driver.FindElement(By.CssSelector(".basket_totals_container"));
        //

        List<IWebElement> productList;
        IReadOnlyCollection<IWebElement> productSizeCollection; 
        List<IWebElement> sizeList;               


        /// <summary>
        /// Konstruktorius
        /// </summary>
        /// <param name="webDriver"></param>
        public WomenClothingPage(IWebDriver webDriver) : base(webDriver) { }        
               
        public WomenClothingPage NavigateToDafaultPage()
        {
            if (Driver.Url != UrlAddress)
                Driver.Url = UrlAddress;
            return this;
        }       

        /// <summary>
        /// Randą pasirinktinai produktą iš sąrašo
        /// </summary>
        /// <returns>grąžina produkto vietą sąraše</returns>
        public int RandomProduct()
        {
            productList = new List<IWebElement>(womenClothingCollection);            
            Random random = new Random();           
            int randomElementIndex = random.Next(productList.Count);
            return randomElementIndex;
        }
        public void ClickOnRandomProduct(int randomElementIndex)
        {
            productList = new List<IWebElement>(womenClothingCollection);            
            productList[randomElementIndex + 1].Click(); //+1
        }

        public int RandomProductSize()
        {            
            Random random = new Random();
            productSizeCollection = _allProducstSize.FindElements(By.TagName("li"));
            int randomSizeIndex = random.Next(productSizeCollection.Count);
            return randomSizeIndex;
        }
        public void ClickOnRandomProductSize(int randomElementIndex)
        {
            productSizeCollection = _allProducstSize.FindElements(By.TagName("li"));
            sizeList = new List<IWebElement>(productSizeCollection);           
            sizeList[randomElementIndex].Click();
        }

        public void AddToBasket()
        {
            _addToBasketButton.Click();
        }

        public void CloseMiniBasket()
        {
            _miniBasketWindows.Click();
        }

        public void ViewBasket()
        {
            _viewBasketButton.Click();
        }

        public void StandardDeliveryCheckBox(bool standardDelivery)
        {
            if (standardDelivery != _standardDeliveryCheckBox.Selected)
                _standardDeliveryCheckBox.Click();            
        }

        public void NextDayDeliveryCheckBox(bool nextDayDelivery)
        {
            if (nextDayDelivery != _nextDayDeliveryCheckBox.Selected)
                _nextDayDeliveryCheckBox.Click();
        }

        public (int, int) SelectAndReturnIndexProductNameAndIndexSize()
        {
            int productIndex = RandomProduct();
            ClickOnRandomProduct(productIndex);

            MouseScrollDownPage(_addToBasketButton);//nuleidžiu puslapį iki mygtuko

            int sizeIndex = RandomProductSize();            
            ClickOnRandomProductSize(sizeIndex);            
           
            return (productIndex, sizeIndex);   
            
        }

        public void CheckSelectedSize()
        {
            (int, int) randomProduct = SelectAndReturnIndexProductNameAndIndexSize();
            int sizeIndex = randomProduct.Item2;
            
            Assert.AreEqual(_selectedSize.Text, sizeList[sizeIndex].Text, 
                           ($"Size isn't the same. The size chosen was " +
                           $"{sizeList[sizeIndex].Text}, and the recorded size was {_selectedSize.Text}"));
        }

        public void MMMMMMM(string wantedBrand)
        {
            var productBrandCollection = _allProductBrand.FindElements(By.XPath("//nav/div[3]/div/ul/li/a"));

            MouseScrollDownPage(_scrollMouseToSize);
            for (int i = 0; i < productBrandCollection.Count; i++)
            {
               
                if (productBrandCollection[i].Text.ToUpper().Contains(wantedBrand.ToUpper()))
                {
                    MouseScrollDownPage(_scrollMouseToSize);                    
                    productBrandCollection[i].Click();
                   
                }                
            }            

        }

        public void Naviguoti(string wantedBrand)           
        {
            ////Driver.SwitchTo().Window("https://www.blacks.co.uk/womens/womens-clothing/br:whitaker/");
            //var window_after = Driver.WindowHandles[0];
            //Driver.SwitchTo(window_after);
            //Driver.SwitchTo();


        }

        public void NaujasSarasas(string wantedBrand)
        {
            productList = new List<IWebElement>(womenClothingCollection);
            for (int i = 0; i < productList.Count; i++)
            {
                string nameSelectedItem = Driver.FindElement(By.CssSelector(".product-item:nth-child(2) .brand")).Text;
                Console.WriteLine(nameSelectedItem);

                Assert.IsTrue(nameSelectedItem.Contains(wantedBrand));
            }
           
            
        }

        public void Rikiuoti()
        {
            Console.WriteLine("111");
            //productList = new List<IWebElement>(womenClothingCollection);
            //for (int i = 0; i < productList.Count; i++)
            //{
            string priceSelectedItem = Driver.FindElement(By.XPath("//article/div/a/span/span")).Text;
            Console.WriteLine("2");
            Console.WriteLine(priceSelectedItem);
            //}
            //productList.sovar expectedList = studyFeeds.OrderByDescending(x => x.Date);
            var bandauRikiuoti = allProductPrice.OrderBy(item=>item.FindElement(By.XPath("//article/div/a/span/span")).Text);
            foreach (var item in allProductPrice)
            {
                Console.WriteLine(item);
            }

        }

        //public void AddProductInBasket(ref int kiekis)
        //{
        //    (int, int) randomProduct = SelectAndReturnIndexProductNameAndIndexSize();
        //    AddToBasket();
        //    kiekis++;
        //    CloseMiniBasket();
        //}

        //public void ChekQuantityInTheShoppingCart(int selectedProductCount)
        //{
        //    (int, int) randomProduct = SelectAndReturnIndexProductNameAndIndexSize();
        //    AddToBasket();
        //    CloseMiniBasket();
        //    allProductInBasket.Count();
        //    Console.WriteLine("111");
        //    Console.WriteLine(allProductInBasket.Count());
        //    Assert.AreEqual(allProductInBasket.Count(), selectedProductCount,
        //            "the quantity of products does not match");
        //}
        
        /// <summary>
        /// Suskaičiuoja krepšialyje esančių prekių bendrą sumą
        /// </summary>
        /// <returns></returns>
        public double CountTotalPriceInBasket()
        {
            IReadOnlyCollection<IWebElement> allProductPriceInBasket = Driver.FindElements(By.XPath("//tr[@class = 'basket_border']"));// kai iškeliu į išorę vienu mažiau krepšelyje būna
            List<IWebElement> allProductInBasketList = new List<IWebElement>(allProductPriceInBasket);
            double allProductPrice = 0;

            for (int i = 1; i < allProductInBasketList.Count; i++)
            {
                
                var moneyString = Driver.FindElement(By.XPath("//tr[" + (i).ToString() + "]/td/table/tbody/tr/td[2]/span")).Text.ToString();                
                double productPrice = ConvertFromStringToDouble(moneyString);
                allProductPrice += productPrice;              
                
            }
            return allProductPrice;
        }

        public double ConvertTotalPriceInBasket()
        {
            string totalPriceInString = _totalPriceInBasket.Text;
            double totalPriceInDouble = ConvertFromStringToDouble(totalPriceInString);
            return totalPriceInDouble;
        }

        public double ConvertStandardDeliveryPrice()
        {
            double standardDeliveryPrice = ConvertFromStringToDouble(_standardDeliveryPrice.Text);
            return standardDeliveryPrice;            
        }


        public double ConvertNextDayDeliveryPrice()
        {
            double nextDayDeliveryPrice = ConvertFromStringToDouble(_nextDayDeliveryPrice.Text);
            return nextDayDeliveryPrice;
        }
        public void AddProductToTheBasket()
        {
            SelectAndReturnIndexProductNameAndIndexSize();
            AddToBasket();
            CloseMiniBasket();
            ViewBasket();
            MouseScrollDownPage(_scrollMouseToSubtotal);
        }

        public void CheckTotalPrice(bool standardDelivery, bool nextDayDelivery)
        {            

            if (standardDelivery == true && nextDayDelivery == false)
            {
                StandardDeliveryCheckBox(standardDelivery);
                double calculatedTotalPrice = CountTotalPriceInBasket() + ConvertStandardDeliveryPrice();
                double totalPriceInBasket = ConvertTotalPriceInBasket();
                Assert.AreEqual(calculatedTotalPrice, totalPriceInBasket, "The total amount varies.");
            }
            else if (nextDayDelivery == true && standardDelivery == false)
            {               

                NextDayDeliveryCheckBox(nextDayDelivery);                

                double calculatedTotalPrice = CountTotalPriceInBasket() + ConvertNextDayDeliveryPrice();
                double totalPriceInBasket = ConvertTotalPriceInBasket();                

                Assert.AreEqual(calculatedTotalPrice, totalPriceInBasket, $"the total amount varies. As suskaiciuoju {calculatedTotalPrice}" +
                                                                          $" is basketo pareina {totalPriceInBasket}");
            }
            else if(standardDelivery == false && nextDayDelivery == false)
            {
                double calculatedTotalPrice = CountTotalPriceInBasket();
                double totalPriceInBasket = ConvertTotalPriceInBasket();
                Assert.AreEqual(calculatedTotalPrice, totalPriceInBasket, "the total amount varies");
            }
               
        }


        /// <summary>
        /// Panaikina pirmą simbolį ir konertuoja tekstinį skaičių į double
        /// </summary>
        /// <param name="money">string tipo pinigai</param>
        /// <returns>double tipo pinigus</returns>
        private double ConvertFromStringToDouble(string money)
        {            
            string moneyWihtoutSimbol = money.Substring(1, money.Length - 1);
            string integerPartOfMoneyInString = moneyWihtoutSimbol.Substring(0, moneyWihtoutSimbol.IndexOf('.'));           
            double integerPartOfMoneyInDouble = Convert.ToDouble(integerPartOfMoneyInString);

            string realPartOfMoneyInString = moneyWihtoutSimbol.Substring(moneyWihtoutSimbol.IndexOf('.') + 1, 
                                                                          moneyWihtoutSimbol.Length - moneyWihtoutSimbol.IndexOf('.') - 1);
            double realPartOfMoneyInDouble = Convert.ToDouble(realPartOfMoneyInString);

            double moneyInDouble = integerPartOfMoneyInDouble + (realPartOfMoneyInDouble / 100);
            
            return moneyInDouble;
        }
        
    }
}
