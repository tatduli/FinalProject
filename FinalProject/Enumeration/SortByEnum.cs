using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FinalProject.Enumeration
{
    enum SortByEnum
    {
        Recommended,
        Customer_Review,
        Best_Sellers,
        High_to_Low,
        Low_to_High,
        A_to_Z,
        Z_to_A

        
    }

    //public static class EnumExtensionMethods
    //{
    //    public static string GetEnumDescription(this Enum enumValue)
    //    {
    //        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

    //        var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

    //        return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
    //    }
    //}  

    
}
