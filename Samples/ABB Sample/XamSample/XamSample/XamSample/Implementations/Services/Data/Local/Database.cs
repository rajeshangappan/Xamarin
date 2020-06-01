using SQLite;
using System;
using XamSample.AppHelper;

namespace XamSample.Services
{
    /// <summary>
    /// Defines the <see cref="Database" />.
    /// </summary>
    public class Database
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the _lazyInstance.
        /// </summary>
        private static readonly Lazy<Database> _lazyInstance = new Lazy<Database>(() => new Database());

        #endregion

        #region PUBLIC_PPTY

        /// <summary>
        /// Gets the Instance.
        /// </summary>
        public static Database Instance => _lazyInstance.Value;

        /// <summary>
        /// Gets the Connection.
        /// </summary>
        public SQLiteAsyncConnection Connection { get; }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Prevents a default instance of the <see cref="Database"/> class from being created.
        /// </summary>
        private Database()
        {
            if (Connection == null)
            {
                Connection = new SQLiteAsyncConnection(AppConstants.DatabasePath);
            }
        }

        #endregion
    }
}
