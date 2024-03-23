using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.PortfolioAggregate;
using MyStocks.Domain.Shares;
using MyStocks.Domain.SharesAggregate;
using MyStocks.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<CurrencyTypes> CurrencyTypes { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<ShareDetail> ShareDetails { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Mystocks_db;User Id=dev-user;Password=dev-run;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
