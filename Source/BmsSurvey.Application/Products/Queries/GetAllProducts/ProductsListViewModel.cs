namespace BmsSurvey.Application.Products.Queries.GetAllProducts
{
    using System.Collections.Generic;

    public class ProductsListViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
