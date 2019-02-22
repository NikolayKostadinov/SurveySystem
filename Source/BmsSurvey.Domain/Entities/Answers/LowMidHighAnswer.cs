using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Domain.Entities.Answers
{
    using Abstract;

    public class LowMidHighAnswer : Answer
    {
        private int value;
        public int Value
        {
            get => this.value;
            set
            {
                if (0 > value || value > 3)
                {
                    throw new ArgumentOutOfRangeException(nameof(Value)
                        , $"The argument \"{nameof(Value)}\" must be between 0 and 2 but is {Value}");
                }

                this.value = value;
            }
        }
    }
}
