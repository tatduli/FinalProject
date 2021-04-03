using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Test
{
    public class BasketTest : BaseTest
    {
        [Order(1)]
        [Repeat(2)]       

        [Test]
        public void TestBasketPrice()
        {
            womenClothingPage.NavigateToDafaultPage();
            basketPage.AddProductToTheBasket();
            basketPage.CheckTotalPrice();           
        }

        [Order(2)]
        [Test]

        public void TestProductCountInBasket()
        {
            basketPage.NavigateToDafaultPage();
            basketPage.CheckProductCountInBasket();
        }

        [TestCase(3, TestName = "3 times increase and check product count")]
        public void TestIncreaseButton(int howMuchIncrease)
        {
            womenClothingPage.NavigateToDafaultPage();
            basketPage.CheckProductCountInBasketAfterIncrease(howMuchIncrease);           
        }

        [TestCase(2, TestName = "2 times increase and check produkt total price")]
       
        public void TestProductAmountAfterTheQuantityIncrease(int howMuchIncrease)
        {            
            womenClothingPage.NavigateToDafaultPage();
            basketPage.CheckProductPriceInBasketAfterIncrease(howMuchIncrease);
        }

        [Test]
        public void TestBasketTotalPriceAfterIncrease()
        {
            womenClothingPage.NavigateToDafaultPage();
            basketPage.AddProductToTheBasket();
            basketPage.Increase();
            basketPage.CheckTotalPrice();
        }

    }
}
