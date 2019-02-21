using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Interfaces
{
    using Answers.Models;
    using Domain.Abstract;
    using Domain.Entities;
    
    public interface IAnswerFactory
    {
        Answer GetAnswer(QuestionType questionType, AnswerViewModel answer);
    }
}
