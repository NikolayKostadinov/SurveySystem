namespace BmsSurvey.Application.Customers.Queries.GetCustomerDetail
{
    using FluentValidation;

    public class GetCustomerDetailQueryValidator : AbstractValidator<GetCustomerDetailQuery>
    {
        public GetCustomerDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().Length(5);
        }
    }
}
