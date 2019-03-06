namespace BmsSurvey.Application.Users.Commands.SendConfirmationEmail
{
    using MediatR;

    public class SendConfirmationEmailCommand : IRequest
    {
        public int Id { get; set; }
    }
}
