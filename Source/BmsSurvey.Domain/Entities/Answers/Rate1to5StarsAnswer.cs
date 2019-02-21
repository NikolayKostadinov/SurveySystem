using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Domain.Entities.Answers
{
    using Abstract;

    public class Rate1To5StarsAnswer : Answer
    {
        private int value;
        public int Value
        {
            get => this.value;
            set
            {
                if (0 > value || value > 5)
                {
                    throw new ArgumentOutOfRangeException(nameof(Value)
                        , $"The argument \"{nameof(Value)}\" must be between 0 and 5 but is {Value}");
                }

                this.value = value;
            }
        }
    }
}
