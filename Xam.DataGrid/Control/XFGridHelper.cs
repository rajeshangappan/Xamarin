using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Xam.DataGrid.Control
{
    /// <summary>
    /// Defines the <see cref="XFGridHelper" />.
    /// </summary>
    public class XFGridHelper
    {
        #region Methods

        /// <summary>
        /// The ConvertToListOf.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="iList">The iList<see cref="IEnumerable"/>.</param>
        /// <returns>The <see cref="IList{T}"/>.</returns>
        public static IList<T> ConvertToListOf<T>(IEnumerable iList)
        {
            if (iList == null)
                return null;

            IList<T> result = new List<T>();
            foreach (T value in iList)
                result.Add(value);

            return result;
        }

        /// <summary>
        /// The SortList.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="sortDirection">The sortDirection<see cref="SortType"/>.</param>
        /// <param name="sortExpression">The sortExpression<see cref="string"/>.</param>
        /// <param name="data">The data<see cref="List{T}"/>.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public List<T> SortList<T>(SortType sortDirection, string sortExpression, List<T> data)
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

        /// <summary>
        /// The GetDynamicSortProperty.
        /// </summary>
        /// <param name="item">The item<see cref="object"/>.</param>
        /// <param name="propName">The propName<see cref="string"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object GetDynamicSortProperty(object item, string propName)
        {
            return item.GetType().GetProperty(propName).GetValue(item, null);
        }

        /// <summary>
        /// The GetPropCount.
        /// </summary>
        /// <param name="ItemsSource">The ItemsSource<see cref="IList{object}"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetPropCount(IList<object> ItemsSource)
        {
            var count = 0;
            if (ItemsSource == null || ItemsSource.Count == 0)
                return 0;
            foreach (var prop in ItemsSource[0].GetType().GetProperties())
            {
                if (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
                {
                    count++;
                }
            }
            return count;
        }

        #endregion
    }
}
