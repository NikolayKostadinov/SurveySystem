using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Domain.Entities
{
    using Abstract;

    public class CompletedSurvey : DeletableEntity
    {
        public override int Id { get; set; }

        public int SurveyId { get; set; }

        public string IpAddress { get; set; }

        public virtual Survey Survey { get; set; }

        public string Email { get; set; }
    }
}
