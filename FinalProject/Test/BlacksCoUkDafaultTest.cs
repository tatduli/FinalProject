using FinalProject.Enumeration;
using NUnit.Framework;
using System;

//Test all meniu button. 
//Test search field by brand. 
//Test email from file and print result in file

namespace FinalProject.Test
{
    public class BlacksCoUkDafaultTest : BaseTest
    {
        
        [Test]
        [TestCase(MeniuEnumeration.Blog, TestName = "Check Blog meniu button ")]
        [TestCase(MeniuEnumeration.Activities, TestName = "Check Activities meniu botton")]
        [TestCase(MeniuEnumeration.Men, TestName = "Check Men meniu botton")]
        [TestCase(MeniuEnumeration.Sale, TestName = "Check Sale meniu botton")]
        [TestCase(MeniuEnumeration.Brands, TestName = "Check Brands meniu botton")]
        [TestCase(MeniuEnumeration.Camping, TestName = "Check Camping meniu botton")]
        [TestCase(MeniuEnumeration.Cycling, TestName = "Check Cycling meniu botton")]
        [TestCase(MeniuEnumeration.Kids, TestName = "Check Kids meniu botton")]
        [TestCase(MeniuEnumeration.Equipment, TestName = "Check Equipment meniu botton")]
        public static void TestSelectedBotton(Enum text)
        {
            blacksCoUkPage.NavigateToDafaultPage();
            blacksCoUkPage.ClickSelectedButton(text);
            blacksCoUkPage.CheckOrALLMeniuWork(text);
        }

        [TestCase("MAGNUM", TestName = "Check search field with Brand")]
        public static void TestSearchFieldByBrand(string brand)
        {
            blacksCoUkPage.NavigateToDafaultPage();
            blacksCoUkPage.ChlickAndWriteOnSearchField(brand);
            blacksCoUkPage.ClickSearchButton();
            blacksCoUkPage.CheckBrandResultThroughTheSearchField(brand);
        }

        [Test]
        public static void TestSubmitEmail()
        {
            blacksCoUkPage.NavigateToDafaultPage();
            blacksCoUkPage.CheckEmailSubmitFieldFromDataFile();            
        }
    }
}
