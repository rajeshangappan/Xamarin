using System;

namespace XamSample.Models
{
    /// <summary>
    /// Defines the <see cref="User" />.
    /// </summary>
    public class User
    {
        #region PUBLIC_PPTY

        /// <summary>
        /// Gets or sets the EmailAddress.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
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
