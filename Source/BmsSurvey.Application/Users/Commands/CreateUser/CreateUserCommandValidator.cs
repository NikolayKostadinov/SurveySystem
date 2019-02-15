namespace BmsSurvey.Application.Users.Commands.CreateUser
{
    using System.Text.RegularExpressions;
    using FluentValidation;
    using Resources;

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.TabNumber).NotEmpty().WithLocalizedMessage(typeof(MessageResource), "TABNUMBER_REQUIRED"); 
            RuleFor(x => x.TabNumber).Must(tn =>
            {
                Regex regex = new Regex(@"^\d{5}$",RegexOptions.Multiline|RegexOptions.Compiled);
                Match match = regex.Match(tn);
                return match.Success;
            }).WithLocalizedMessage(typeof(MessageResource), "TABNUMBER_INVALID_FORMAT"); 
            RuleFor(x => x.UserName).MinimumLength(4).MaximumLength(60).NotEmpty();
            RuleFor(x => x.FirstName).MaximumLength(60).NotEmpty();
            RuleFor(x => x.SirName).MaximumLength(60).NotEmpty();
            RuleFor(x => x.LastName).MaximumLength(60).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(6).NotEmpty();
        }
    }
}
