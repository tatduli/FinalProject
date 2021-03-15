using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Test 
{
    public class WomenClothingTest : BaseTest
    {
        [Repeat(3)]
        [Test]
        public void TestSelectedSize()
        {
            womenClothingPage.NavigateToDafaultPage();           
            womenClothingPage.CheckSelectedSize();
           
        }

        [Repeat(2)]
        [TestCase(false, false, TestName = "Test price without delivery")]
        [TestCase(true, false, TestName = "Test price with standard delivery")]
        [TestCase(false, true, TestName = "Test price with next day delivery")]

        public void TestBasketPrice(bool standardDelivery, bool nextDayDelivery)
        {
            womenClothingPage.NavigateToDafaultPage();
            womenClothingPage.AddProductToTheBasket();          
            womenClothingPage.CheckTotalPrice(standardDelivery, nextDayDelivery);
        }
    
        //[Order(1)]
        //[Repeat(3)]
        //[Test]
        //public void TestOrAllProductInBasket ()
        //{
        //    int count = 0;
        //    womenClothingPage.NavigateToDafaultPage();
        //    womenClothingPage.AddProductInBasket(ref count);
        //    Console.WriteLine("nnn");
        //    Console.WriteLine(count);
        //    //womenClothingPage.ChekQuantityInTheShoppingCart(3);

        //}

        //[Order(2)]
        //[Test]
        //public void gggggggg()
        //{
        //    womenClothingPage.ChekQuantityInTheShoppingCart(3);
        //}

        [Test]
        public void bandau()
        {
            womenClothingPage.NavigateToDafaultPage();
            womenClothingPage.MMMMMMM("Whitaker");
            naujas.NaujasSarasas("Whitaker");
            //womenClothingPage.Naviguoti("Whitaker");
            //womenClothingPage.NaujasSarasas("Whitaker");
            //womenClothingPage.Rikiuoti();
        }
    }
}
