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
            if (iList == null)
                return null;

            IList<T> result = new List<T>();
            foreach (T value in iList)
                result.Add(value);

            return result;
        }

        public List<T> Sort_List<T>(SortType sortDirection, string sortExpression, List<T> data)
        {

            List<T> data_sorted = new List<T>();

            if (sortDirection == SortType.Ascending)
            {
                data_sorted = (from n in data
                               orderby GetDynamicSortProperty(n, sortExpression) ascending
                               select n).ToList();
            }
            else if (sortDirection == SortType.Descending)
            {
                data_sorted = (from n in data
                               orderby GetDynamicSortProperty(n, sortExpression) descending
                               select n).ToList();

            }
            return data_sorted;

        }

        public object GetDynamicSortProperty(object item, string propName)
        {
            return item.GetType().GetProperty(propName).GetValue(item, null);
        }

        public static int GetPropCount(IList ItemsSource)
        {
            var count = 0;
            foreach (var prop in ItemsSource[0].GetType().GetProperties())
            {
                if (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
