namespace BmsSurvey.Application.Roles.Queries.GetRoleForEditing
{
    using MediatR;
    using Models;

    public class RoleForEditingQuery : IRequest<RoleEditViewModel>
    {
        public int? Id { get; set; }
    }
}
