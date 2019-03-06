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
            RuleFor(uuc => uuc.Id).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(uuc => uuc.Email).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(uuc => uuc.Email).EmailAddress().WithMessage(MessageResource.INVALID_EMAIL);
            RuleFor(uuc => uuc.FirstName).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(uuc => uuc.LastName).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(uuc => uuc.TabNumber).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.TabNumber).Must(tn =>
            {
                Regex regex = new Regex(@"^\d{5}$", RegexOptions.Multiline | RegexOptions.Compiled);
                Match match = regex.Match(tn??string.Empty);
                return match.Success;
            }).WithMessage(MessageResource.TABNUMBER_INVALID_FORMAT);
        }
    }


}
