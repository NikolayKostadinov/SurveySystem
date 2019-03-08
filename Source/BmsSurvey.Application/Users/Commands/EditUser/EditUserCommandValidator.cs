namespace BmsSurvey.Application.Users.Commands.EditUser
{
    using System;
    using System.Text.RegularExpressions;
    using FluentValidation;
    using Resources;

    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            RuleFor(euc => euc.Id).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(euc => euc.Email).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(euc => euc.Email).EmailAddress().WithMessage(MessageResource.INVALID_EMAIL);
            RuleFor(euc => euc.FirstName).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(euc => euc.FirstName).MaximumLength(60).WithMessage(MessageResource.MAXIMUM_LENGTH);
            RuleFor(euc => euc.SirName).MaximumLength(60).WithMessage(MessageResource.MAXIMUM_LENGTH);
            RuleFor(euc => euc.LastName).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(euc => euc.LastName).MaximumLength(60).WithMessage(MessageResource.MAXIMUM_LENGTH);
            RuleFor(euc => euc.TabNumber).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(euc => euc.TabNumber).Must(tn =>
            {
                Regex regex = new Regex(@"^\d{5}$", RegexOptions.Multiline | RegexOptions.Compiled);
                Match match = regex.Match(tn ?? string.Empty);
                return match.Success;
            }).WithMessage(MessageResource.TABNUMBER_INVALID_FORMAT);
        }
    }


}
