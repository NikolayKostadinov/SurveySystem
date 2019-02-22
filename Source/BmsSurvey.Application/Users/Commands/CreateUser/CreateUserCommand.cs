namespace BmsSurvey.Application.Users.Commands.CreateUser
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Common.Interfaces;
    using Domain.Entities.Identity;
    using Interfaces.Mapping;
    using MediatR;
    using Models;

    public class CreateUserCommand : IRequest<IStatus>, IHavePassword
    {
        [Required]
        public string TabNumber { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SirName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }
        
        public ICollection<RoleSimpleViewModel> Roles { get; set; }

    }
}
