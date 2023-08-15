using System;
using System.Collections.Generic;

namespace QuizGamePerssistence.Models;

public partial class Choice
{
    public Guid ChoiceId { get; set; }

    public Guid? MultipleChoiceId { get; set; }

    public string? Text { get; set; }

    public virtual MultipleChoiceQuestion? MultipleChoice { get; set; }
}
