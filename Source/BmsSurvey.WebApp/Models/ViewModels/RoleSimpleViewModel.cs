namespace BmsSurvey.WebApp.Models.ViewModels
{
    using Application.Interfaces.Mapping;
    using Domain.Entities.Identity;

    public class RoleSimpleViewModel : IMapFrom<Role>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
