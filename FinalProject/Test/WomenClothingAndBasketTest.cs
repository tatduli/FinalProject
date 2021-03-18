using FinalProject.Enumeration;
using FinalProject.List;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Test 
{
    public class WomenClothingAndBasketTest : BaseTest
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
             
        [TestCase(SortByEnum.Low_to_High, "Asc")]
        [TestCase(SortByEnum.High_to_Low, "Dsc")]
       
        public void TestSortByPrice(Enum sortBy, string sortAscOrDsc)
        {
            womenClothingPage.NavigateToDafaultPage();
            womenClothingPage.SelectedSortByDropDown(sortBy);         
            womenClothingPage.SelectedSortByDropDownAndReturnNewSortedList(sortBy);
            womenClothingPage.CompareTwoSortedListsByPrice(sortBy, sortAscOrDsc);
        }

        [TestCase(SortByEnum.Z_to_A, "Dsc")]
        [TestCase(SortByEnum.A_to_Z, "Asc")]

        public void TestSortByAlphabet(Enum sortBy, string sortAscOrDsc)
        {
            womenClothingPage.NavigateToDafaultPage();
            womenClothingPage.SelectedSortByDropDown(sortBy);
            womenClothingPage.SelectedSortByDropDownAndReturnNewSortedList(sortBy);
            womenClothingPage.CheckSortedListsByAlphabet(sortAscOrDsc);
        }

        [TestCase("whitaker", TestName = "Test brand WHITAKER")]
        [TestCase("merrell", TestName = "Test brand MERRELL")]
        public void TestOrSelectedBrandLoadInPage(string brand)
        {
            womenClothingPage.NavigateToDafaultPage();
            //var brand = brands.Split(',').ToList();
            womenClothingPage.ClickOnSelectedBrand(brand);
            womenClothingPage.NavigateToNewPage(brand);
            womenClothingPage.CheckOrInNewPageAreSelectedBrand(brand);
        }

        [Test]
        public void SLIDER()
        {
            womenClothingPage.NavigateToDafaultPage();
            womenClothingPage.MoveSliderWantedRange(50, 150);
        }
    }
}
