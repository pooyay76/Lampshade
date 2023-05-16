using System.Collections.Generic;

namespace LampshadeQuery.Contracts.Product
{
    public interface IProductQuery
    {
        public List<ProductQueryModel> GetLatestArrivals();
    }
}
