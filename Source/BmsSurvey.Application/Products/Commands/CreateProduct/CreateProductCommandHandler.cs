namespace BmsSurvey.Application.Products.Commands.CreateProduct
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using MediatR;
    using Persistence;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly BmsSurveyDbContext _context;

        public CreateProductCommandHandler(BmsSurveyDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = new Product
            {
                ProductName = request.ProductName,
                CategoryId = request.CategoryId,
                SupplierId = request.SupplierId,
                UnitPrice = request.UnitPrice,
                Discontinued = request.Discontinued
            };

            _context.Products.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.ProductId;
        }
    }
}
