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
        /// <summary>
        /// Į paieškos lauką užrašau norimos prekės pavadinimą.
        /// Atsitiktiniu būdu yra išrenkama prekė iš pateikto sąrašo.
        /// Patikrinam ar išrinktos prekės duomenys sutampa su prekės duomenimis najame lange.
        /// </summary>
        /// <param name="productName"></param>
        [Repeat(3)]
        [TestCase("Cooler", TestName = "Check Cooler product description and price in basket")]
        [TestCase("Boots", TestName = "Check Boots product description and price in basket")]
        [TestCase("Dress", TestName = "Check Dress product description and price in basket")]
        public void CheckRandomCoolerProductDescriptionAndPriceInBasket(string productName)
        {            
            blacksCoUkPage.NavigateToDafaultPage();
            blacksCoUkPage.ChlickAndWriteOnSearchField(productName);
            blacksCoUkPage.ClickSearchButton();
            oneProductPage.TestSelectedProductDescriptionAndPriceOnOpenedPage();
            
        }

       
        //[TestCase("Dress")]
        //public void Bandau(string produktas)
        //{
        //    blacksCoUkPage.NavigateToDafaultPage();
        //    blacksCoUkPage.ChlickAndWriteOnSearchField(produktas);
        //    blacksCoUkPage.ClickSearchButton();
            
        //}

        //[Test, Order(2)]

        //public void TestRandomProductSalePriceInBasket()
        //{
        //    oneProductPage.IDETIIKrepsiali();
        //}

        //[Test]
         
        //public void TotalSum()
        //{
        //    oneProductPage.NavigateToDafaultPage();
        //    oneProductPage.CalculateBasketSum();
        //}
    }
}
