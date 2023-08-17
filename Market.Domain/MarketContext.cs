using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Market.Domain.Entity;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Market.Domain
{
    public class MarketContext : DbContext
    {
        public MarketContext() { }
        public MarketContext(DbContextOptions<MarketContext> options, IConfiguration configuration) :
            base(options)
        { Configuration = configuration; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Market.Domain.Entity.Attribute> Attributes { get; set; }


        protected readonly IConfiguration Configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = Configuration.GetConnectionString("MSSQL");

            optionsBuilder.UseSqlServer(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(b =>
            {
                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.Attributes)
                    .WithOne(e => e.Product)
                    .HasForeignKey(ur => ur.ProductId)
                    .IsRequired();
            });

        }
    }
}
