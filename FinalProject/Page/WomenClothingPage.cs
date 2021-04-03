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
    public class WomenClothingPage : BasePage
    {
        private const string UrlAddress = "https://www.blacks.co.uk/womens/womens-clothing/";

        IReadOnlyCollection<IWebElement> womenClothingCollection => Driver.FindElements(By.CssSelector(".product-item"));
        private SelectElement _sortByDropDown => new SelectElement(Driver.FindElement(By.Id("productlist_sort_by_top")));
        private IWebElement _addToBasketButton => Driver.FindElement(By.XPath("//input[@type = 'submit']"));
        private IWebElement _allProducstSize => Driver.FindElement(By.XPath("//ul[@class = 'attribute_value_list group']"));
        private IWebElement _selectedSize => Driver.FindElement(By.CssSelector(".selected-size"));
        private IWebElement _allProductBrand => Driver.FindElement(By.XPath("//ul[@class = 'facet-list brand_list filter_container ac_filter_static template_nav_filter_overflow']"));
        private IWebElement _scrollMouseToSize => Driver.FindElement(By.CssSelector(".facet_size:nth-child(5) .no-ajax"));
        private IWebElement _sortByField => Driver.FindElement(By.Id("productlist_sort_by_top"));
        private IWebElement _priceSlider => Driver.FindElement(By.Id("price-slider"));
        private IWebElement _scrollMouseToReview => Driver.FindElement(By.XPath("//nav/div[11]/h3/span"));
        private IWebElement _rightSlider => Driver.FindElement(By.XPath("//div[@id='price-slider']/div/div[3]/div"));
        private IWebElement _leftSlider => Driver.FindElement(By.XPath("//div[@id='price-slider']/div/div/div"));
        private IWebElement _priceMaxSlider => Driver.FindElement(By.CssSelector(".facet:nth-child(8) .price_max"));
        private IWebElement _priceMinSlider => Driver.FindElement(By.CssSelector(".facet:nth-child(8) .price_min"));

        List<IWebElement> productList;
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

        //========================================================================
        //                             TEST SIZE
        //========================================================================

        /// <summary>
        /// pasirenka atsitiktinį produktą ir grąžina jo kainos ir išrinkto produkto pavadinimo indeksus
        /// </summary>
        /// <returns></returns>
        public (int, int) SelectAndReturnIndexProductNameAndIndexSize()
        {
            productList = new List<IWebElement>(womenClothingCollection);

            int productIndex = RandomProduct(productList);
            ClickOnRandomProduct(productIndex, productList);

            MouseScrollDownPage(_addToBasketButton);//nuleidžiu puslapį iki mygtuko

            IReadOnlyCollection<IWebElement> productSizeCollection = _allProducstSize.FindElements(By.TagName("li"));
            sizeList = new List<IWebElement>(productSizeCollection);
            int sizeIndex = RandomProductSize(_allProducstSize);
            ClickOnRandomProductSize(sizeIndex, sizeList);

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
    
        public void ClickOnSelectedBrand(List<string> brand)
        {
            MessageBoxShow();
            var productBrandCollection = _allProductBrand.FindElements(By.XPath("//nav/div[3]/div/ul/li/a"));

            MouseScrollDownPage(_scrollMouseToSize);

            for (int i = 0; i < productBrandCollection.Count; i++)
            {
                foreach (string item in brand)
                {
                    if (productBrandCollection[i].Text.ToLower().Contains(item))
                    {
                        MouseScrollDownPage(_scrollMouseToSize);
                        productBrandCollection[i].Click();
                    }
                }
            }
        }

        public void NavigateToNewPage(string brand)
        {
            string urlAddress = "https://www.blacks.co.uk/womens/womens-clothing/";
            string newUrlAddress = urlAddress + "br:" + brand;
            Driver.Navigate().GoToUrl(newUrlAddress);
        }

        public void CheckOrInNewPageAreSelectedBrand(List<string> brands)
        {
            productList = new List<IWebElement>(womenClothingCollection);           
           
            for (int i = 0; i < productList.Count; i++)
            {
                string brandNameOneItem = Driver.FindElement(By.CssSelector(".product-item:nth-child(2) .brand")).Text;
               
                if (!brands.Contains(brandNameOneItem.ToLower()))
                    Assert.IsFalse(true,"Somthing wrong on page");              
            }
            Assert.IsTrue(true);           
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
        //                            TEST PRICE SLIDER
        //========================================================================

        public void MoveSliderWantedRange(int minPrice, int maxPrice)
        {
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
            if (maxPrice <= converPriceMaxSlider + 3 && maxPrice >= converPriceMaxSlider - 3)//pix paklaida
            {
                for (int i = 0; i < womenClothingCollection.Count - 1; i++)//
                {
                    string priceItemInWomenCollection = Driver.FindElement(By.XPath("//article[(" + (i + 1).ToString() + ")]/div/a/span/span")).Text;
                    double convertedPriceItemInWomenCollection = ConvertFromStringToDouble(priceItemInWomenCollection);
                    //imu 3 paklaidą, nes tiksliai neapskaiciuoja
                    Assert.That(convertedPriceItemInWomenCollection, Is.GreaterThanOrEqualTo(minPrice - 3), "Price is less than I want");
                    Assert.That(convertedPriceItemInWomenCollection, Is.LessThanOrEqualTo(maxPrice), "Price is greater than I want");
                }
            }
            else
                Assert.True(maxPrice <= converPriceMaxSlider + 3 && maxPrice >= converPriceMaxSlider - 3, "Slider don't move or don't fix");
        }
        //===========================================================================================

        /// <summary>
        /// Panaikina pirmą simbolį ir konertuoja tekstinį skaičių į double
        /// </summary>
        /// <param name="money">string tipo pinigai</param>
        /// <returns>double tipo pinigus</returns>
        public static double ConvertFromStringToDouble(string money)
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

        //========================================================================
        //                              PRIVATE METODE
        //========================================================================

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
        /// sukuriamas naujas sąrašas atsižvelgiat į prekės aprašą
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
