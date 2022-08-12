using System.Collections.Generic;

namespace LampshadeQuery.Contracts.ProductAgg
{
    public interface IProductQuery
    {
        public List<ProductQueryModel> GetLatestArrivals();
    }
}
