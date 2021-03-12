using OpenQA.Selenium;
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
        
        IReadOnlyCollection<IWebElement> womenClothingList => Driver.FindElements(By.CssSelector(".product-item"));
        //IReadOnlyCollection<IWebElement> sizeList => Driver.FindElements(By.CssSelector(".attribute_value_list group"));
        
        //IReadOnlyCollection<IWebElement> sizeList => Driver.FindElements(By.XPath("//a[@rel ='nofollow']"));
        IReadOnlyCollection<IWebElement> sizeList => Driver.FindElements(By.XPath("//ul[@rel ='nofollow']"));
        private IWebElement _addToBasketButton => Driver.FindElement(By.XPath("//input[@type = 'submit']"));
        
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
            List<IWebElement> productList = new List<IWebElement>(womenClothingList);
            Random random = new Random();
            Console.WriteLine("kiekis produktu");
            Console.WriteLine(productList.Count);
            int randomElementIndex = random.Next(productList.Count);
            return randomElementIndex;
        }
        public void ClickOnRandomProduct(int randomElementIndex)
        {
            List<IWebElement> productList = new List<IWebElement>(womenClothingList);
            productList[randomElementIndex].Click();
        }

        public int RandomProductSize()
        {
            
            List<IWebElement> productSizeList = new List<IWebElement>(sizeList);
            Random random = new Random();
            Console.WriteLine("kiekis ismieru");
            Console.WriteLine(productSizeList.Count);
            int randomSizeIndex = random.Next(productSizeList.Count);
            return randomSizeIndex;
        }
        public void ClickOnRandomProductSize(int randomElementIndex)
        {
            List<IWebElement> productSizeList = new List<IWebElement>(womenClothingList);
            productSizeList[randomElementIndex].Click();
        }
        public void Bandau()
        {
            //string nameSelectedItem = Driver.FindElement(By.XPath("//article[" + (randomElementIndex + 1).ToString() + "]/div/a/span/h2/span[2]")).Text;
            int productIndex = RandomProduct();

            Console.WriteLine("rasto produkto indeksas");
            Console.WriteLine(productIndex);

            ClickOnRandomProduct(productIndex);

            MouseScrollDownPage(_addToBasketButton);//nuleidžiu puslapį iki mygtuko
            int sizeIndex = RandomProductSize();

            Console.WriteLine("rasto dydzio indeksas");
            Console.WriteLine(sizeIndex);
            ClickOnRandomProductSize(sizeIndex);//
           
        }
    }
}
