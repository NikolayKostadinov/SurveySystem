using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Users.Commands.DeleteUser
{
    using System.Security.Principal;
    using MediatR;

    public class DeleteUserCommand:IRequest
    {
        public DeleteUserCommand(int id, IPrincipal currentUser)
        {
            Id = id;
            CurrentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public int Id { get; }
        public IPrincipal CurrentUser { get;}
    }
}
