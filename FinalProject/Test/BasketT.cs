using FinalProject.Page;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Test
{
    public class BasketT : BaseTest
    {
        [Test]

        public void selectProduct()
        {
            oneProductPage.NavigateToDafaultPage();
            basket.ChlickAndWriteOnSearchField("Cooler");
            basket.ClickSearchButton();
            basket.ChlickOnRandomProduct();
           
        }

        
    }
}
