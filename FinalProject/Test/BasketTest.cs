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
        [Repeat(2)]
        [TestCase(false, false, TestName = "Test price without delivery")]
        [TestCase(true, false, TestName = "Test price with standard delivery")]
        [TestCase(false, true, TestName = "Test price with next day delivery")]

        public void TestBasketPrice(bool standardDelivery, bool nextDayDelivery)
        {
            womenClothingPage.NavigateToDafaultPage();
            basketPage.AddProductToTheBasket();

            basketPage.CheckTotalPrice(standardDelivery, nextDayDelivery);
        }
    }
}
