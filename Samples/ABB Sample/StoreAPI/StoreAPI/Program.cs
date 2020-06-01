using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace StoreAPI
{
    /// <summary>
    /// Defines the <see cref="Program" />.
    /// </summary>
    public class Program
    {
        #region PUBLIC_METHODS

        /// <summary>
        /// The CreateWebHostBuilder.
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
.UseStartup<Startup>();
        }

        /// <summary>
        /// The Main.
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        #endregion
    }
}
