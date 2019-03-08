namespace BmsSurvey.Application.Users.Commands.CreateUser
{
    using System;
    using System.Text.RegularExpressions;
    using FluentValidation;
    using Resources;

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(cuc => cuc.TabNumber).NotEmpty().WithLocalizedMessage(typeof(MessageResource), "TABNUMBER_REQUIRED"); 
            RuleFor(cuc => cuc.TabNumber).Must(tn =>
            {
                Regex regex = new Regex(@"^\d{5}$",RegexOptions.Multiline|RegexOptions.Compiled);
                Match match = regex.Match(tn??String.Empty);
                return match.Success;
            }).WithLocalizedMessage(typeof(MessageResource), "TABNUMBER_INVALID_FORMAT");
            RuleFor(cuc => cuc.Email).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(cuc => cuc.Email).EmailAddress().WithMessage(MessageResource.INVALID_EMAIL);
            RuleFor(cuc => cuc.UserName).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(cuc => cuc.UserName).MinimumLength(4).WithMessage(MessageResource.MINIMUM_LENGTH);
            RuleFor(cuc => cuc.UserName).MaximumLength(60).WithMessage(MessageResource.MAXIMUM_LENGTH);
            RuleFor(cuc => cuc.FirstName).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(cuc => cuc.FirstName).MaximumLength(60).WithMessage(MessageResource.MAXIMUM_LENGTH);
            RuleFor(cuc => cuc.SirName).MaximumLength(60).WithMessage(MessageResource.MAXIMUM_LENGTH);
            RuleFor(cuc => cuc.LastName).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(cuc => cuc.LastName).MaximumLength(60).WithMessage(MessageResource.MAXIMUM_LENGTH);
            RuleFor(cuc => cuc.Password).NotEmpty().WithMessage(MessageResource.REQUIRED);
        }
    }
}
