namespace BmsSurvey.Domain.Abstract
{
    using Entities;

    public abstract class Answer : DeletableEntity
    {
        public override int Id { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

    }
}
