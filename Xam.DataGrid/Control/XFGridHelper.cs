using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace Xam.DataGrid.Control
{
    public class XFGridHelper
    {
        public static IList<T> ConvertToListOf<T>(IList iList)
        {
            IList<T> result = new List<T>();
            foreach (T value in iList)
                result.Add(value);

            return result;
        }

        public List<T> Sort_List<T>(string sortDirection, string sortExpression, List<T> data)
        {

            List<T> data_sorted = new List<T>();

            if (sortDirection == "Ascending")
            {
                data_sorted = (from n in data
                               orderby GetDynamicSortProperty(n, sortExpression) ascending
                               select n).ToList();
            }
            else if (sortDirection == "Descending")
            {
                data_sorted = (from n in data
                               orderby GetDynamicSortProperty(n, sortExpression) descending
                               select n).ToList();

            }

            return data_sorted;

        }

        public object GetDynamicSortProperty(object item, string propName)
        {
            //Use reflection to get order type
            return item.GetType().GetProperty(propName).GetValue(item, null);
        }
    }
}
