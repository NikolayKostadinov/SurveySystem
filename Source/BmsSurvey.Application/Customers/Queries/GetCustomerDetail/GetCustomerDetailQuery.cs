namespace BmsSurvey.Application.Customers.Queries.GetCustomerDetail
{
    using MediatR;

    public class GetCustomerDetailQuery : IRequest<CustomerDetailModel>
    {
        public string Id { get; set; }
    }
}
