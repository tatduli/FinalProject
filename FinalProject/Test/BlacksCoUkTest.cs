using FinalProject.Enumeration;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Test
{
    public class BlacksCoUkTest : BaseTest
    {
        
        [TestCase("MAGNUM", TestName = "Check search field with Brand")]
        public static void TestSearchFieldByBrand(string brand)
        {
            blacksCoUkPage.NavigateToDafaultPage();
            blacksCoUkPage.ChlickAndWriteOnSearchField(brand);
            blacksCoUkPage.ClickSearchButton();
            blacksCoUkPage.CheckBrandResultThroughTheSearchField(brand);

        }

        [Test]
        [TestCase(MeniuEnumeration.Blog, TestName = "Check Blog meniu button ")]
        [TestCase(MeniuEnumeration.Activities, TestName = "Check Activities meniu botton")]
        [TestCase(MeniuEnumeration.Men, TestName = "Check Men meniu botton")]
        [TestCase(MeniuEnumeration.Sale, TestName = "Check Sale meniu botton")]
        public static void TestSelectedBotton(Enum text)
        {
            blacksCoUkPage.NavigateToDafaultPage();
            blacksCoUkPage.ClickSelectedButton(text);
            blacksCoUkPage.CheckOrALLMeniuWork(text);
        }

        [Test]
        public static void BANDYMAS()
        {
            blacksCoUkPage.NavigateToDafaultPage();
            //blacksCoUkPage.DismissConfirmationAlert(); //kazkas kitas ne alertas
            blacksCoUkPage.CheckOrSocialButtonWork();
        }

    }
}
