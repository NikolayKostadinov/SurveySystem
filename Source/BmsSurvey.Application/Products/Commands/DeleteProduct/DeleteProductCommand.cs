namespace BmsSurvey.Application.Products.Commands.DeleteProduct
{
    using MediatR;

    public class DeleteProductCommand : IRequest
    {
        public int Id { get; set; }
    }
}
