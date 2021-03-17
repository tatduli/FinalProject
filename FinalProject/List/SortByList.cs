using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.List
{
    public class SortByList
    {

        List<string> sortByList = new List<string>();

        public void SortBy()
        {
            sortByList.Add("Recommended");
            sortByList.Add("Avg. Customer Review");
            sortByList.Add("Best Sellers");
            sortByList.Add("Price: High to Low");
            sortByList.Add("Price: Low to High");
            sortByList.Add("Name (A to Z)");
            sortByList.Add("Name (Z to A)");
        }        

        public string GetData(int index)
        {
            return sortByList[index];
        }

    }
}
