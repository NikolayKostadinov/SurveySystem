using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Exceptions
{
    public class SurveyCompletedException: Exception
    {
        private object key;
        public object Key => this.key;
        public SurveyCompletedException(object key, string ip)
            : base($"Survey \"{key}\" was already completed from IP {ip}.")
        {
            this.key = key;
        }
    }
}
