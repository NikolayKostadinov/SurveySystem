namespace BmsSurvey.Application.Users.Commands.CreateUser
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Common.Interfaces;
    using Domain.Entities.Identity;
    using Interfaces.Mapping;
    using MediatR;
    using Models;

    public class CreateUserCommand : IRequest<IStatus>, IHavePassword
    {
        public string TabNumber { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string SirName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
        
        public ICollection<RoleSimpleViewModel> Roles { get; set; }

    }
}
