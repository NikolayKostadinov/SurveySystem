namespace BmsSurvey.Application.Categories.Queries
{
    using System.Collections.Generic;
    using MediatR;
    using Models;

    public class GetCategoryPreviewQuery : IRequest<List<CategoryPreviewDto>>
    {
        public int CategoryId { get; set; }
    }
}
