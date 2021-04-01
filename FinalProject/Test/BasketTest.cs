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
        //================PASIKEITE PUSLAPIS=================================
        //[TestCase(false, false, TestName = "Test price without delivery")]
        //[TestCase(true, false, TestName = "Test price with standard delivery")]
        //[TestCase(false, true, TestName = "Test price with next day delivery")]

        [Test]
        public void TestBasketPrice()
        {
            womenClothingPage.NavigateToDafaultPage();
            basketPage.AddProductToTheBasket();
            basketPage.CheckTotalPrice();
            //basketPage.CheckTotalPrice(standardDelivery, nextDayDelivery);
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

        [TestCase(3, TestName = "3 times increase and check produkt total price")]
        //[Test]
        public void TestProductAmountAfterTheQuantityIncrease(int howMuchIncrease)
        {
            //TestIncreaseButton(/*howMuchIncrease*/1);
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
