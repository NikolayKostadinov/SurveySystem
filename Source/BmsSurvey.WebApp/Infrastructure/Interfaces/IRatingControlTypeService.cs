using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BmsSurvey.WebApp.Infrastructure.Interfaces
{
    using Domain.Entities;

    public interface IRatingControlTypeService
    {
        string GetRatingControlType(QuestionType questionType);
    }
}
