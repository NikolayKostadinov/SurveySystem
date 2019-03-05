namespace BmsSurvey.Application.Exceptions
{
    using System;

    public class NotFoundException : Exception
    {
        public object Key { get; }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
            this.Key = key;
        }
    }
}