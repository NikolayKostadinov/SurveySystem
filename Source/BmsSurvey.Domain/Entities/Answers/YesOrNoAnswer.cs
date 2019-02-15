using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Domain.Entities.Answers
{
    using Abstract;

    public class YesOrNoAnswer : Answer
    {
        public bool Value { get; set; }
    }
}
