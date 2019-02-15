namespace BmsSurvey.Application.Customers.Commands.DeleteCustomer
{
    using MediatR;

    public class DeleteCustomerCommand : IRequest
    {
        public string Id { get; set; }
    }
}
