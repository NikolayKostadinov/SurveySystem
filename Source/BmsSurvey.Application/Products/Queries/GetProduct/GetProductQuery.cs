namespace BmsSurvey.Application.Products.Queries.GetProduct
{
    using MediatR;

    public class GetProductQuery : IRequest<ProductViewModel>
    {
        public int Id { get; set; }
    }
}
