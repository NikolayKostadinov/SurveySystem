namespace BmsSurvey.Application.Customers.Queries.GetCustomersList
{
    using System.Collections.Generic;

    public class CustomersListViewModel
    {
        public IList<CustomerLookupModel> Customers { get; set; }
    }
}
