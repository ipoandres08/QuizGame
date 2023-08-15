using System;
using System.Collections.Generic;

namespace QuizGamePerssistence.Models;

public partial class FillBlanckQuestion
{
    public Guid? QuestionId { get; set; }

    public int? BlankPosition { get; set; }

    public virtual Question? Question { get; set; }
}
