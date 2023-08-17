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

            /*var users = new User[]
            {
                new User { Id = Guid.NewGuid(), FirstName = "Dejan", LastName = "Bunic", Password = PassGenerate("0709"), Email = "dejanbunic@company.ba", IsActive= false },
                new User { Id = Guid.NewGuid(), FirstName = "Petar", LastName = "Bunic", Password = PassGenerate("0709"), Email = "petarbunic@company.ba", IsActive = false },
                new User { Id = Guid.NewGuid(), FirstName = "Zele", LastName = "Zeka", Password = PassGenerate("0709"), Email = "zelezeka@company.ba", IsActive = false },
               
            };

            builder.Entity<User>().HasData(
                users
               );*/


        }

        private string PassGenerate(string pass)
        {
            // Generate a 128-bit salt using a sequence of
            // cryptographically strong random bytes
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pass!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
