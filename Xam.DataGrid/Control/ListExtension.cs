using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Xam.DataGrid.Control
{
    /// <summary>
    /// Defines the <see cref="ListExtension" />.
    /// </summary>
    public static class ListExtension
    {
        #region Methods

        /// <summary>
        /// The ToObservableCollection.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="enumerable">The enumerable<see cref="IEnumerable{T}"/>.</param>
        /// <returns>The <see cref="ObservableCollection{T}"/>.</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            var col = new ObservableCollection<T>();
            foreach (var cur in enumerable)
            {
                col.Add(cur);
            }
            return col;
        }

        /// <summary>
        /// The ToListObject.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="enumerable">The enumerable<see cref="IEnumerable"/>.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<T> ToListObject<T>(this IEnumerable enumerable)
        {
            var col = new List<T>();
            foreach (var cur in enumerable)
            {
                col.Add((T)cur);
            }
            return col;
        }

        #endregion
    }
}
