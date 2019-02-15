namespace BmsSurvey.Application.Exceptions
{
    using System;

    public class NotFoundException : Exception
    {
        private object key;
        public object Key => this.key;
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
            this.key = key;
        }
    }
}