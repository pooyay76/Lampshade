﻿using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Infrastructure.EfCore.Mappings
{
    public class CustomerDiscountMapping : IEntityTypeConfiguration<CustomerDiscount>
    {
        public void Configure(EntityTypeBuilder<CustomerDiscount> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Reason).HasMaxLength(255).IsRequired();
            builder.Property(x=>x.EndDate).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x=>x.DiscountPercentage).IsRequired();
            builder.Property(x=>x.ProductId).IsRequired();


        }
    }
}
