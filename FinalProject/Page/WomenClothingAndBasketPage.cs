using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject.Page
{
    public class WomenClothingAndBasketPage : BasePage
    {
        private const string UrlAddress = "https://www.blacks.co.uk/womens/womens-clothing/";

        IReadOnlyCollection<IWebElement> womenClothingCollection => Driver.FindElements(By.CssSelector(".product-item"));
        IReadOnlyCollection<IWebElement> allProductInBasket => Driver.FindElements(By.XPath("//span[@class = 'basket_item_text']"));
        IReadOnlyCollection<IWebElement> allProductPrice = Driver.FindElements(By.XPath("//article"));
        IReadOnlyCollection<IWebElement> allProductPriceInBasket = Driver.FindElements(By.XPath("//tr[@class = 'basket_border']"));
        private IWebElement _addToBasketButton => Driver.FindElement(By.XPath("//input[@type = 'submit']"));
        private IWebElement _allProducstSize => Driver.FindElement(By.XPath("//ul[@class = 'attribute_value_list group']"));
        private IWebElement _selectedSize => Driver.FindElement(By.CssSelector(".selected-size"));
        private IWebElement _viewBasketButton => Driver.FindElement(By.CssSelector(".cta.btn-buy-process-primary.ga-ip"));     
        private IWebElement _miniBasketWindows => Driver.FindElement(By.XPath("//div[@id='miniBagWrapper']/a"));        
        private IWebElement _allProductBrand => Driver.FindElement(By.XPath("//ul[@class = 'facet-list brand_list filter_container ac_filter_static template_nav_filter_overflow']"));
        private IWebElement _scrollMouseToSize => Driver.FindElement(By.CssSelector(".facet_size:nth-child(5) .no-ajax"));
        private IWebElement _totalPriceInBasket => Driver.FindElement(By.CssSelector(".basket_total"));        
        private IWebElement _standardDeliveryCheckBox => Driver.FindElement(By.Id("delivery_option_87"));
        private IWebElement _nextDayDeliveryCheckBox => Driver.FindElement(By.Id("delivery_option_1031"));
        private IWebElement _standardDeliveryPrice => Driver.FindElement(By.XPath("//label/span/span[2]"));
        private IWebElement _nextDayDeliveryPrice => Driver.FindElement(By.XPath("//div[2]/label/span/span[2]"));
        private IWebElement _scrollMouseToSubtotal => Driver.FindElement(By.CssSelector(".basket_totals_container"));        
        private IWebElement _sortByField => Driver.FindElement(By.Id("productlist_sort_by_top"));
        private IWebElement _priceSlider => Driver.FindElement(By.Id("price-slider"));
        private IWebElement _scrollMouseToReview => Driver.FindElement(By.XPath("//nav/div[11]/h3/span"));
        private IWebElement _rightSlider => Driver.FindElement(By.XPath("//div[@id='price-slider']/div/div[3]/div"));
        private IWebElement _leftSlider => Driver.FindElement(By.XPath("//div[@id='price-slider']/div/div/div"));
        private IWebElement _priceMaxSlider => Driver.FindElement(By.CssSelector(".facet:nth-child(10) .price_max"));
        private IWebElement _priceMinSlider => Driver.FindElement(By.CssSelector(".facet:nth-child(10) .price_min"));
        private SelectElement _sortByDropDown => new SelectElement(Driver.FindElement(By.Id("productlist_sort_by_top")));


        //
       

        List<IWebElement> productList;
        IReadOnlyCollection<IWebElement> productSizeCollection; 
        List<IWebElement> sizeList;               


        /// <summary>
        /// Konstruktorius
        /// </summary>
        /// <param name="webDriver"></param>
        public WomenClothingAndBasketPage(IWebDriver webDriver) : base(webDriver) { }        
               
        public WomenClothingAndBasketPage NavigateToDafaultPage()
        {
            if (Driver.Url != UrlAddress)
                Driver.Url = UrlAddress;
            return this;
        }

        //========================================================================
        //                             TEST SIZE
        //========================================================================
       
        /// <summary>
        /// pasirenka atsitiktinį produktą ir grąžina jo kainos ir išrinkto produkto pavadinimo indeksus
        /// </summary>
        /// <returns></returns>
        public (int, int) SelectAndReturnIndexProductNameAndIndexSize()
        {
            int productIndex = RandomProduct();
            ClickOnRandomProduct(productIndex);

            MouseScrollDownPage(_addToBasketButton);//nuleidžiu puslapį iki mygtuko

            int sizeIndex = RandomProductSize();            
            ClickOnRandomProductSize(sizeIndex);            
           
            return (productIndex, sizeIndex);               
        }

        /// <summary>
        /// Tikrinu ar krepšelyje rodomas dydis toks pats kokį išrinko vartotojas
        /// </summary>
        public void CheckSelectedSize()
        {
            (int, int) randomProduct = SelectAndReturnIndexProductNameAndIndexSize();
            int sizeIndex = randomProduct.Item2;
            
            Assert.AreEqual(_selectedSize.Text, sizeList[sizeIndex].Text, 
                           ($"Size isn't the same. The size chosen was " +
                           $"{sizeList[sizeIndex].Text}, and the recorded size was {_selectedSize.Text}"));
        }

        //========================================================================
        //                             SELECT BRAND AND CHECK SIDE
        //========================================================================
        // NESIGAUNA - Išrinkti kelis brendus ir patikrinti puslapį
        public void ClickOnSelectedBrand(string brand)
        {
            var productBrandCollection = _allProductBrand.FindElements(By.XPath("//nav/div[3]/div/ul/li/a"));

            MouseScrollDownPage(_scrollMouseToSize);
            for (int i = 0; i < productBrandCollection.Count; i++)
            {               
                if(brand.Contains(productBrandCollection[i].Text.ToLower()))
                {
                    MouseScrollDownPage(_scrollMouseToSize);                    
                    productBrandCollection[i].Click();                    
                }                
            }          
        }

        public void NavigateToNewPage(string brand)           
        {
            string urlAddress = "https://www.blacks.co.uk/womens/womens-clothing/";
            string newUrlAddress = urlAddress + "br:" + brand;
            Driver.Navigate().GoToUrl(newUrlAddress);
        }

        public void CheckOrInNewPageAreSelectedBrand(string brand)
        {
            productList = new List<IWebElement>(womenClothingCollection);
            for (int i = 0; i < productList.Count; i++)
            {
                string brandNameOneItem = Driver.FindElement(By.CssSelector(".product-item:nth-child(2) .brand")).Text;              

                Assert.IsTrue(brand.ToLower().Contains( brandNameOneItem.ToLower()));
            }           
        }
        
        //========================================================================
        //                             SORT TEST
        //========================================================================
        public void ClickOnSortBy()
        {
            _sortByField.Click();
        }

        /// <summary>
        /// Iš enum padaromas stringas ir iš dropbox išrenkamas norimas rikiavimas
        /// </summary>
        /// <param name="mySelectBy"></param>
        public void SelectedSortByDropDown(Enum mySelectBy)
        {
            IList<IWebElement> optionsList = _sortByDropDown.Options;
            string enumToString = mySelectBy.ToString();
            string sortedBy = enumToString.Replace('_', ' ');

            for (int i = 0; i < optionsList.Count; i++)
            {
                if (optionsList[i].Text.Contains(sortedBy))
                {                    
                    optionsList[i].Click();                   
                    break;
                }
            }
        }

        /// <summary>
        /// grąžinamas naujas puslapio surikuotas sąrašas, pagal norimą kriterijų
        /// </summary>
        /// <param name="mySelectBy"></param>
        /// <returns></returns>
        public List<double> SelectedSortByDropDownAndReturnNewSortedList(Enum mySelectBy)
        {
            List<double> newSortedWomenClothingListByPrice = NewListByPrice();
            GetWait().Until(ExpectedConditions.ElementToBeClickable(_sortByField));// manau nereik
            ClickOnSortBy();
            SelectedSortByDropDown(mySelectBy);            
            return newSortedWomenClothingListByPrice;
        }

        /// <summary>
        /// Msukuriu naują sąrašą ir jį surikiuoju pagal kainą
        /// </summary>
        /// <returns></returns>
        public List<double> MySortedWomenClothingListByPriceAscending()
        {
            List<double> mySortedWomenClothingListByPrice = NewListByPrice();            

            mySortedWomenClothingListByPrice.Sort();                       
            return mySortedWomenClothingListByPrice;
        }

        /// <summary>
        /// Testuoju mano surikiuota sąraša su puslapio surikiuot sąrašu
        /// </summary>
        /// <param name="mySelectBy">pagal ką rikiuosiu</param>
        /// <param name="sortAscOrDsc">Kokia tvarka rikiuosiu</param>
        public void CompareTwoSortedListsByPrice(Enum mySelectBy, string sortAscOrDsc)
        {            
            List<double> newSortedWomenClothingListByPrice = SelectedSortByDropDownAndReturnNewSortedList(mySelectBy);
            List<double> mySortedWomenClothingListByPrice = MySortedWomenClothingListByPriceAscending();
           
            if (sortAscOrDsc == "Asc")
            {                
                Assert.AreEqual(newSortedWomenClothingListByPrice, mySortedWomenClothingListByPrice, "The products are badly sorted");
                
            }
            if (sortAscOrDsc == "Dsc")
            {                
                mySortedWomenClothingListByPrice.Reverse();
                Assert.AreEqual(newSortedWomenClothingListByPrice, mySortedWomenClothingListByPrice, "The products are badly sorted");
            }      
        }

        /// <summary>
        /// Testuoju ar puslapis gerai surikiuoja alphabeto tvarka
        /// </summary>
        /// <param name="sortAscOrDsc">Kokia tvarka rikiuosiu</param>
        public void CheckSortedListsByAlphabet(string sortAscOrDsc)
        {
            List<string> newSortedWomenClothingListByAlphabet = NewListByAlphabet();
            if (sortAscOrDsc == "Asc")
                Assert.That(newSortedWomenClothingListByAlphabet, Is.Ordered, "The products are badly sorted");
            if (sortAscOrDsc == "Dsc")
                Assert.That(newSortedWomenClothingListByAlphabet, Is.Ordered.Descending, "The products are badly sorted");
        }


        //========================================================================
        //                             CHECK TOTAL PRICE IN THE BASKET
        //========================================================================
       
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
        public void CheckTotalPrice(bool standardDelivery, bool nextDayDelivery)
        {                        
            if (standardDelivery && !nextDayDelivery)
            {
                StandardDeliveryCheckBox(standardDelivery);
              
                double calculatedTotalPrice = CountTotalPriceInBasket() + ConvertStandardDeliveryPrice();
                double totalPriceInBasket = ConvertTotalPriceInBasket();
                Assert.AreEqual(calculatedTotalPrice, totalPriceInBasket, "The total amount varies.");
            }
            else if (nextDayDelivery && !standardDelivery)
            {               
                NextDayDeliveryCheckBox(nextDayDelivery);
              
                double calculatedTotalPrice = CountTotalPriceInBasket() + ConvertNextDayDeliveryPrice();
                double totalPriceInBasket = ConvertTotalPriceInBasket();                

                Assert.AreEqual(calculatedTotalPrice, totalPriceInBasket, $"the total amount varies. As suskaiciuoju {calculatedTotalPrice}" +
                                                                          $" is basketo pareina {totalPriceInBasket}");
            }
            else if(!standardDelivery && !nextDayDelivery)
            {
                double calculatedTotalPrice = CountTotalPriceInBasket();
                double totalPriceInBasket = ConvertTotalPriceInBasket();
                Assert.AreEqual(calculatedTotalPrice, totalPriceInBasket, "the total amount varies");
            }
               
        }

        //========================================================================
        //                            TEST PRICE SLIDER
        //========================================================================
 
        public void MoveSliderWantedRange(int minPrice, int maxPrice)
        {           
            Actions move = new Actions(Driver);
            double converPriceMaxSlider = Convert.ToDouble(_priceMaxSlider.Text);
            double converPriceMinSlider = Convert.ToDouble(_priceMinSlider.Text);
            MouseScrollDownPage(_scrollMouseToReview);
            (double, double) offset = ConvertSliderMinPriceAndMaxPriceToPixselOffset(minPrice, maxPrice, 
                                                                                     converPriceMaxSlider, converPriceMinSlider);

            if (minPrice >= converPriceMinSlider)
            {
                MoveLeft((int)offset.Item1);
                Thread.Sleep(1000);
            }              
            
            if (maxPrice <= converPriceMaxSlider)
            {
                MoveRight((int)offset.Item2);
                Thread.Sleep(1000);
            }                
                   
             //move.ClickAndHold(_leftSlider).MoveByOffset((int)offsetMin, 0).Release().Perform();
            //Thread.Sleep(1000);
            ////move.MoveToElement(_priceSlider, 30, 0);
            ////MouseScrollDownPage(_scrollMouseToReview);

            //move.ClickAndHold(_rightSlider).MoveByOffset((int)-100, 0).Release().Perform() ;
            //Thread.Sleep(1000);
            //move.ClickAndHold(_rightSlider).MoveByOffset((int)-70, 0).Release().Perform();
            //MoveRight(100);
            //MoveRight(70);
        }

        public void MoveLeft(int minPrice)
        {
            Actions move = new Actions(Driver);
            move.ClickAndHold(_leftSlider).MoveByOffset((int)minPrice, 0).Release().Perform();
        }

        public void MoveRight(int maxPrice)
        {
            Actions move = new Actions(Driver);
            move.ClickAndHold(_rightSlider).MoveByOffset((int)-maxPrice, 0).Release().Perform();
        }
        
        public void TestSliderPriceWithPagePrice(int minPrice, int maxPrice)
        {
            double converPriceMaxSlider = Convert.ToDouble(_priceMaxSlider.Text);
            Console.WriteLine(maxPrice);
            Console.WriteLine(converPriceMaxSlider);
            if (maxPrice <= converPriceMaxSlider + 3  && maxPrice >= converPriceMaxSlider - 3)//pix paklaida
            {
                for (int i = 0; i < womenClothingCollection.Count - 1; i++)//
                {
                    string priceItemInWomenCollection = Driver.FindElement(By.XPath("//article[(" + (i + 1).ToString() + ")]/div/a/span/span")).Text;
                    double convertedPriceItemInWomenCollection = ConvertFromStringToDouble(priceItemInWomenCollection);

                    Assert.That(convertedPriceItemInWomenCollection, Is.GreaterThanOrEqualTo(minPrice), "Price is less than I want");
                    Assert.That(convertedPriceItemInWomenCollection, Is.LessThanOrEqualTo(maxPrice), "Price is greater than I want");
                }
            }
            else
                Assert.True(maxPrice <= converPriceMaxSlider + 3 && maxPrice >= converPriceMaxSlider - 3, "Slider don't move or don't fix");
         
        }


        //========================================================================
        //                              PRIVATE METODE
        //========================================================================

        /// <summary>
        /// Panaikina pirmą simbolį ir konertuoja tekstinį skaičių į double
        /// </summary>
        /// <param name="money">string tipo pinigai</param>
        /// <returns>double tipo pinigus</returns>
        private double ConvertFromStringToDouble(string money)
        {            
            string moneyWihtoutSimbol = money.Substring(1, money.Length - 1);//panaikina svaro zenkla
            string integerPartOfMoneyInString = moneyWihtoutSimbol.Substring(0, moneyWihtoutSimbol.IndexOf('.'));           
            double integerPartOfMoneyInDouble = Convert.ToDouble(integerPartOfMoneyInString);

            string realPartOfMoneyInString = moneyWihtoutSimbol.Substring(moneyWihtoutSimbol.IndexOf('.') + 1, 
                                                                          moneyWihtoutSimbol.Length - moneyWihtoutSimbol.IndexOf('.') - 1);
            double realPartOfMoneyInDouble = Convert.ToDouble(realPartOfMoneyInString);

            double moneyInDouble = integerPartOfMoneyInDouble + (realPartOfMoneyInDouble / 100);
            
            return moneyInDouble;
        }

        /// <summary>
        /// Sukuriamas naujas list'as iš kainų
        /// </summary>
        /// <returns>grąžiną naują list'ą</returns>
        private List<double> NewListByPrice()
        {
            List<double> priceList = new List<double>();

            for (int i = 0; i < womenClothingCollection.Count - 1; i++)//
            {
                string priceSelectedItem = Driver.FindElement(By.XPath("//article[(" + (i + 1).ToString() + ")]/div/a/span/span")).Text;   //kaina             
                priceList.Add(ConvertFromStringToDouble(priceSelectedItem));               
            }
            return priceList;
        }

        private (double, double) ConvertSliderMinPriceAndMaxPriceToPixselOffset(int minPrice, int maxPrice,
                                                                                double converPriceMaxSlider, double converPriceMinSlider)
        {            
            double sliderWidth = Convert.ToDouble(_priceSlider.Size.Width);            
            
            double offsetMin = (float)sliderWidth * (minPrice / converPriceMaxSlider);

            double offsetMax = (float)sliderWidth * ((converPriceMaxSlider - maxPrice) / converPriceMaxSlider);
            Console.WriteLine(offsetMax);

            return (offsetMin, offsetMax);
        }

        /// <summary>
        /// Randą pasirinktinai produktą iš sąrašo
        /// </summary>
        /// <returns>grąžina produkto vietą sąraše</returns>
        private int RandomProduct()
        {
            productList = new List<IWebElement>(womenClothingCollection);
            Random random = new Random();
            int randomElementIndex = random.Next(productList.Count);
            return randomElementIndex;
        }

        /// <summary>
        /// išrenkamas atsitiktinis produktas
        /// </summary>
        /// <param name="randomElementIndex"></param>
        private void ClickOnRandomProduct(int randomElementIndex)
        {
            productList = new List<IWebElement>(womenClothingCollection);
            productList[randomElementIndex].Click(); //+1
        }

        /// <summary>
        /// randamas atsitiktinis rūbo dydis
        /// </summary>
        /// <returns>atsitiktio rūbo dydžio indekso grąžinimas</returns>
        private int RandomProductSize()
        {
            Random random = new Random();
            productSizeCollection = _allProducstSize.FindElements(By.TagName("li"));
            int randomSizeIndex = random.Next(productSizeCollection.Count);
            return randomSizeIndex;
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

        /// <summary>
        /// išrenkamas atsitiktis rūbo dydis
        /// </summary>
        /// <param name="randomElementIndex">atsitiktinai išrinkto rūbo dydžio indeksas</param>
        private void ClickOnRandomProductSize(int randomElementIndex)
        {
            productSizeCollection = _allProducstSize.FindElements(By.TagName("li"));
            sizeList = new List<IWebElement>(productSizeCollection);
            sizeList[randomElementIndex].Click();
        }

        /// <summary>
        /// sukuriamas naujas sąrašas atsi-velgiat į prekės aprašą
        /// </summary>
        /// <returns>grąžiną naują list'ą</returns>
        private List<string> NewListByAlphabet()
        {
            List<string> alphabetList = new List<string>();

            for (int i = 0; i < womenClothingCollection.Count - 1; i++)//
            {
                string descriptionSelectedItem = Driver.FindElement(By.XPath("//article[(" + (i + 1).ToString() + ")]/div/a/span/h2/span[2]")).Text;   //teksta             
                alphabetList.Add(descriptionSelectedItem);                
            }
            return alphabetList;
        }
    }
}
