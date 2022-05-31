using Lion.Core.Domain._Common;
using Lion.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lion.Infrastructure.Persistence.Configurations
{
    public class SellerConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasKey(x => x.SellerId);
            builder.Property(x => x.SellerId)
                   .HasColumnType("char")
                   .HasMaxLength(36);

            builder.Property(x => x.TaxId)
                   .IsRequired();

            builder.Property(x => x.Name)
                   .IsRequired();

            builder.HasQueryFilter(x => x.BaseEntityState != BaseEntityState.Deleted);
        }
    }
}
