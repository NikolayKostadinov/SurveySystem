using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Users.Commands.SendConfirmationEmail
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities.Identity;
    using Exceptions;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Notifications.UserConfirmEmail;

    public class SendConfirmationEmailCommandHandler:IRequestHandler<SendConfirmationEmailCommand>
    {
        private readonly UserManager<User> userManager;
        private readonly IMediator mediator;

        public SendConfirmationEmailCommandHandler(UserManager<User> userManager, IMediator mediator)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Unit> Handle(SendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await this.userManager.FindByIdAsync(request.Id.ToString());

            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.Id);
            }

            await this.mediator.Publish(new UserConfirmEmailNotification(user));

            return await Unit.Task;
        }
    }
}
