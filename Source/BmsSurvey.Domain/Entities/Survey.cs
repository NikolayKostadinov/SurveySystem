using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Domain.Entities
{
    using Abstract;
    using Common.Abstract;

    public class Survey : DeletableEntity
    {
        private DateTime activeFrom;
        private DateTime activeTo;

        public Survey()
        {
            this.Questions = new HashSet<Question>();
            this.CompletedSurveys = new HashSet<CompletedSurvey>();
        }

        public override int Id { get; set; }

        public string SurveyTitle { get; set; }

        public string Description { get; set; }

        public int PageSize { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<CompletedSurvey> CompletedSurveys { get; set; }

        public DateTime ActiveFrom
        {
            get => this.activeFrom;
            set => this.activeFrom = value.Date;
        }
        public DateTime ActiveTo
        {
            get => this.activeTo;
            set => this.activeTo = value.Date;
        }

        public bool IsActive => this.ActiveFrom <= TimeProvider.Current.UtcNow.Date
                                && TimeProvider.Current.UtcNow.Date <= this.ActiveTo;

    }
}
