using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Domain.Entities.Answers
{
    using Abstract;

    public class FreeTextAnswer : Answer
    {
        public string Value { get; set; }
    }
}
