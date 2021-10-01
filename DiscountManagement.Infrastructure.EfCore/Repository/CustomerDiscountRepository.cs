using DiscountManagement.Application.Contract;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Framework.Infrastructure;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;
using _0_Framework.Application;

namespace DiscountManagement.Infrastructure.EfCore.Repositories
{
    public class CustomerDiscountRepository : RepositoryBase<long,CustomerDiscount>,ICustomerDiscountRepository
    {
        private readonly DiscountContext discountContext;
        private readonly ShopContext shopContext;

        public CustomerDiscountRepository(DiscountContext discountContext,
            ShopContext shopContext) : base(discountContext)
        {
            this.discountContext = discountContext;
            this.shopContext = shopContext;
        }
        public EditCustomerDiscount EditGet(long id)
        {
            return discountContext.CustomerDiscounts
                .Select(x=> new EditCustomerDiscount
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToString(),
                ProductId = x.ProductId,
                Reason = x.Reason,
                StartDate = x.StartDate.ToString()
            })
                .FirstOrDefault(x => x.Id == id);
            

        }
        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel command)
        {
            var products = shopContext.Products.Select(X => new{ X.Id,X.Name});
            var query = discountContext.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                DiscountRate = x.DiscountRate,
                StartDateFa = x.StartDate.ToFarsi(),
                EndDateFa = x.EndDate.ToFarsi(),
                ProductId = x.ProductId,
                Reason = x.Reason,
            }).AsNoTracking();
            if (command.ProductId != default)
            {
                query = query.Where(x => x.ProductId == command.ProductId);
            }
            
            //if user has startdate input, show all discounts that were expired after that time
            if (command.StartDate != default)
                query = query.Where(x => x.EndDate >= command.StartDate);

            //if user has enddate input, show all the discounts that were started before that time
            if (command.EndDate != default)
                query = query.Where(x => x.StartDate <= command.EndDate);

            if (!string.IsNullOrWhiteSpace(command.Reason))
                query=query.Where(x => x.Reason == command.Reason);
            return query.ToList();
        }
    }
}
