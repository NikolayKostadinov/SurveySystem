using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Domain.Entities
{
    using Abstract;
    using Common.Abstract;

    public class Survey : DeletableEntity
    {
        public Survey()
        {
            this.Questions = new HashSet<Question>();
        }

        public override int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int PageSize { get; set; }

        public ICollection<Question> Questions { get; set; }

        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }

        public bool IsActive => this.ActiveFrom <= TimeProvider.Current.UtcNow
                                && TimeProvider.Current.UtcNow <= this.ActiveTo;

    }
}
