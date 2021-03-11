using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Test
{
    public class OneProductTest : BaseTest
    {
        [Test, Order(1)]
        public void CheckRandomCoolerProductDescriptionAndPriceInBasket()
        {            
            blacksCoUkPage.NavigateToDafaultPage();
            blacksCoUkPage.ChlickAndWriteOnSearchField("Cooler");
            blacksCoUkPage.ClickSearchButton();           
            oneProductPage.TestSelectedProductDescriptionTheSameOpenedPage();
        }

        [Test, Order(2)]
        public void CheckRandomBootsProductDescriptionAndPriceInBasket()
        {
            blacksCoUkPage.NavigateToDafaultPage();
            blacksCoUkPage.ChlickAndWriteOnSearchField("Boots");
            blacksCoUkPage.ClickSearchButton();
            oneProductPage.TestSelectedProductDescriptionTheSameOpenedPage();
        }

        [Test, Order(2)]
        public void CheckRandomDressProductDescriptionAndPriceInBasket()
        {
            blacksCoUkPage.NavigateToDafaultPage();
            blacksCoUkPage.ChlickAndWriteOnSearchField("Dress");
            blacksCoUkPage.ClickSearchButton();
            oneProductPage.TestSelectedProductDescriptionTheSameOpenedPage();
        }

        [Test, Order(2)]

        public void TestRandomProductSalePriceInBasket()
        {
            
        }

        [Test]
         
        public void TotalSum()
        {
            oneProductPage.NavigateToDafaultPage();
            oneProductPage.CalculateBasketSum();
        }
    }
}
