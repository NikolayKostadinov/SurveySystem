using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Users.Queries.GetUser
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities.Identity;
    using Exceptions;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class GetUserQueryHandler:IRequestHandler<GetUserQuery,User>
    {
        private readonly UserManager<User> userManager;

        public GetUserQueryHandler(UserManager<User> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = this.userManager.Users
                .FirstOrDefault(x => x.UserName == request.User.Identity.Name 
                                          &&x.IsDeleted == false);

            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.User.Identity.Name);
            }

            return Task.FromResult(user);
        }
    }
}
