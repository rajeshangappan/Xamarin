using SQLite;
using System;

namespace XamSample.Models.DAO
{
    /// <summary>
    /// Defines the <see cref="UserDAO" />.
    /// </summary>
    public class UserDAO
    {
        #region PUBLIC_PPTY

        /// <summary>
        /// Gets or sets the EmailAddress.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [PrimaryKey]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether LoggedIn.
        /// </summary>
        public bool LoggedIn { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Role.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        public string Username { get; set; }

        #endregion
    }
}
