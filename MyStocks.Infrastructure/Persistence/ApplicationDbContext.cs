using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyStocks.Domain.Common.Abstractions;
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

        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        public ApplicationDbContext(IConfiguration configuration,
            IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var identity = _contextAccessor.HttpContext.User.Identity.Name;
                modelBuilder.Entity<IHasOwner>().HasQueryFilter(x => x.OwnerId == Guid.Parse(identity));
            }
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //var Identity = Thread.CurrentPrincipal.Identity.Name;
            //builder.HasQueryFilter(x => x.OwnerId == Guid.Parse(Identity));
        }
    }
}
