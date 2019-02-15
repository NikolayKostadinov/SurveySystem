namespace BmsSurvey.Application.Users.Models
{
    using Domain.Entities.Identity;
    using Interfaces.Mapping;

    public class RoleSimpleViewModel:IMapFrom<Role>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
