//  ------------------------------------------------------------------------------------------------
//   <copyright file="TestRequestValidator.cs" company="Business Management System Ltd.">
//       Copyright "" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------
namespace BmsSurvey.Application.Tests.Infrastructure
{
    using FluentValidation;

    public class TestRequestValidator : AbstractValidator<TestRequest>
    {
        public TestRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id empty");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name empty");
        }
    }
}