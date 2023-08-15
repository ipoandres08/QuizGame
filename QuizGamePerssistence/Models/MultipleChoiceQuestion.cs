using System;
using System.Collections.Generic;

namespace QuizGamePerssistence.Models;

public partial class MultipleChoiceQuestion
{
    public Guid? QuestionId { get; set; }

    public Guid MultipleChoiceId { get; set; }

    public virtual ICollection<Choice> Choices { get; set; } = new List<Choice>();

    public virtual Question? Question { get; set; }
}
