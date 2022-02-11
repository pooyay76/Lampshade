
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.EfCore.Mapping
{
    public class ColleagueDiscountMapping : IEntityTypeConfiguration<ColleagueDiscount>
    {
        public void Configure(EntityTypeBuilder<ColleagueDiscount> builder)
        {
            builder.ToTable("ColleagueDiscounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DiscountRate).HasColumnType("decimal(3,2)");
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
        }
    }
}
