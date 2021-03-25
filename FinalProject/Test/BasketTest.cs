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

        [TestCase(3, TestName = "3 times increase")]
        public void TestIncreaseButton(int howMuchIncrease)
        {
            womenClothingPage.NavigateToDafaultPage();
            basketPage.CheckProductCountInBasketAfterIncrease(howMuchIncrease);           
        }

        [TestCase(2, TestName = "2 times increase")]

        public void TestProductAmountAfterTheQuantityIncrease(int howMuchIncrease)
        {
            TestIncreaseButton(howMuchIncrease);
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
