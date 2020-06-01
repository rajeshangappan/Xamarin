namespace XamSample.AppHelper
{
    /// <summary>
    /// Defines the <see cref="SampleHelper" />.
    /// </summary>
    public class SampleHelper
    {
        #region PUBLIC_METHODS

        /// <summary>
        /// The IsAdmin.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsAdmin()
        {
            return Xamarin.Essentials.SecureStorage.GetAsync("role").GetAwaiter().GetResult() == "admin";
        }

        #endregion
    }
}
