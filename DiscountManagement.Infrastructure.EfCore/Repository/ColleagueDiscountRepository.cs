using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscountAgg;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Framework.Infrastructure;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Infrastructure.EfCore;
using System.Collections.Generic;
using System.Linq;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class ColleagueDiscountRepository:RepositoryBase<long,ColleagueDiscount>,IColleagueDiscountRepository
    {
        private readonly ShopContext shopContext;
        private readonly DiscountContext discountContext;

        public ColleagueDiscountRepository(ShopContext shopContext, DiscountContext discountContext) : base(discountContext)
        {
            this.shopContext = shopContext;
            this.discountContext = discountContext;
        }

        public EditColleagueDiscount EditGet(long id)
        {

            var data = discountContext.ColleagueDiscounts.FirstOrDefault(x => x.Id == id);

            var products = shopContext.Products.Select(x => new ProductMinimalViewModel()
            {
                Id = x.Id,
                CategoryName = x.Category.Name,
                Name = x.Name,
                Code = x.Code,
                CreationDateTime = x.CreationDateTime.ToFarsi(),
                Picture = x.Picture,
                ShortDescription = x.ShortDescription,
            }).ToList();


            return new EditColleagueDiscount() { Id = data.Id, DiscountRate = data.DiscountRate, Name = data.Name, ProductId = data.ProductId };
        }
        public IEnumerable<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel entity)
        {
            var query = discountContext.ColleagueDiscounts;
            var products = shopContext.Products.Select(x=>new {x.Id,x.Name }).AsEnumerable();

            if (entity.ProductId != default)
                query.FirstOrDefault(x => x.ProductId == entity.ProductId);

            //from dis in query
            //join prod in shopContext.Products on dis.ProductId equals prod.Id
            return query.AsEnumerable().Join(products, x => x.ProductId, y => y.Id, (x, y) => new ColleagueDiscountViewModel()
            {
                CreationDate = x.CreationDateTime.ToFarsi(),
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId,
                ProductName = y.Name,
                Name = x.Name,
                Id = x.Id
            });

        }
    }
}
