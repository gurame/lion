using Lion.Core.Domain.Entities;
using Lion.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Lion.Infrastructure.Persistence.Context
{
    public class LionDbContext : DbContext
    {
        public LionDbContext(DbContextOptions<LionDbContext> options) : base(options)
        {

        }
        public DbSet<Seller> Sellers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LionDbContext).Assembly);
            modelBuilder.RemovePluralTableNameConvention();
            modelBuilder.RemoveDefaultDeleteBehavior();

            base.OnModelCreating(modelBuilder);
        }
    }
}
