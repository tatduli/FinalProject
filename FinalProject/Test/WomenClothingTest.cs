using FinalProject.Enumeration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

        [TestCase("brasher,whitaker", TestName = "Test 2 brand")]
        [TestCase("merrell,eurohike,columbia", TestName = "Test 3 brand")]
        public void TestOrSelectedBrandLoadInPage(string brands)
        {
            womenClothingPage.NavigateToDafaultPage();            
            List<string> brand = brands.Split(',').ToList();
            womenClothingPage.ClickOnSelectedBrand(brand);
            Thread.Sleep(2000);           
            womenClothingPage.CheckOrInNewPageAreSelectedBrand(brand);
        }

        [TestCase(50, 300, TestName = "Slider range [50, 300]")]
        [TestCase(0, 300, TestName = "Slider range [0, 300]")]
        [TestCase(50, 100, TestName = "Slider range [50, 100]")]
        [TestCase(200, 300, TestName = "Slider range [200, 300]")]

        public void TestPriceSlider(int minPrice, int maxPrice)
        {
            womenClothingPage.NavigateToDafaultPage();
            womenClothingPage.MoveSliderWantedRange(minPrice, maxPrice);
            womenClothingPage.TestSliderPriceWithPagePrice(minPrice, maxPrice);
        }
    }
}
