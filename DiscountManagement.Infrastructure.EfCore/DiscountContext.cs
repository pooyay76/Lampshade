using DiscountManagement.Infrastructure.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infrastructure.EfCore
{
    public class DiscountContext:DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(DiscountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
