namespace XamSample.Models
{
    /// <summary>
    /// Defines the <see cref="LoginResponse" />.
    /// </summary>
    public class LoginResponse
    {
        #region PUBLIC_PPTY

        /// <summary>
        /// Gets or sets the Token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the User.
        /// </summary>
        public User User { get; set; }

        #endregion
    }
}
