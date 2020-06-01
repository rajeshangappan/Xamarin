using Microsoft.EntityFrameworkCore;
using StoreAPI.Models;

namespace StoreAPI.AppDBContext
{
    /// <summary>
    /// Defines the <see cref="StoreDBContext" />.
    /// </summary>
    public class StoreDBContext : DbContext
    {
        #region PUBLIC_PPTY

        /// <summary>
        /// Gets or sets the Products.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the Users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreDBContext"/> class.
        /// </summary>
        /// <param name="options">The options<see cref="DbContextOptions{StoreDBContext}"/>.</param>
        public StoreDBContext(DbContextOptions<StoreDBContext> options)
              : base(options)
        {
        }

        #endregion

        /// <summary>
        /// The OnModelCreating.
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder<see cref="ModelBuilder"/>.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
