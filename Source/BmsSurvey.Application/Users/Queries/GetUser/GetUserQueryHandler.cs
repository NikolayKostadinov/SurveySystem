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

        public GetUserQueryHandler(UserManager<User> userManger)
        {
            this.userManager = userManger ?? throw new ArgumentNullException(nameof(userManger));
        }

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await this.userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == request.User.Identity.Name 
                                          &&x.IsDeleted == false,cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.User.Identity.Name);
            }

            return user;
        }
    }
}
