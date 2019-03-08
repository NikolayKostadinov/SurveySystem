using System;
using System.Text;

namespace BmsSurvey.Application.Users.Commands.ChangeUserCulture
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities.Identity;
    using Exceptions;
    using Infrastructure.Extensions;
    using Interfaces;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class ChangeUserCultureCommandHandler : IRequestHandler<ChangeUserCultureCommand, Unit>
    {
        private readonly UserManager<User> userManager;
        private readonly ISupportedCulturesService supportedCulturesService;

        public ChangeUserCultureCommandHandler(UserManager<User> userManager,
            ISupportedCulturesService supportedCulturesService)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.supportedCulturesService = supportedCulturesService ??
                                            throw new ArgumentNullException(nameof(supportedCulturesService));
        }

        public async Task<Unit> Handle(ChangeUserCultureCommand request, CancellationToken cancellationToken)
        {
            var user = await this.userManager.FindByNameAsync(request.User.Identity.Name);

            if (user is null)
            {
                throw new NotFoundException(nameof(user), request.User.Identity.Name);
            }

            if (!supportedCulturesService.IsCultureSupported(request.UiCulture))
            {
                throw new OperationFailedException(new string[] { $"Unsupported culture '{request.UiCulture}'" });
            }

            user.CultureId = request.UiCulture;

            var result = await this.userManager.UpdateAsync(user);

            result.Check(nameof(ChangeUserCulture));
            return await Unit.Task;

        }
    }
}
