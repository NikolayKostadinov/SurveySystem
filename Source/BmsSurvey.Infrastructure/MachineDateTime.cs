namespace BmsSurvey.Infrastructure
{
    using System;
    using Common;
    using Common.Interfaces;

    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public int CurrentYear => DateTime.Now.Year;
    }
}
