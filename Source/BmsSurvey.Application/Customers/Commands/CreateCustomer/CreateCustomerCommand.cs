namespace BmsSurvey.Application.Customers.Commands.CreateCustomer
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Interfaces;
    using MediatR;
    using Persistence;

    public class CreateCustomerCommand : IRequest
    {
        public string Id { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string ContactTitle { get; set; }

        public string Country { get; set; }

        public string Fax { get; set; }

        public string Phone { get; set; }

        public string PostalCode { get; set; }

        public string Region { get; set; }
    }
}
