using System;
using System.Collections.Generic;

namespace QuizGamePerssistence.Models;

public partial class Question
{
    public Guid QuestionId { get; set; }

    public Guid? QuizId { get; set; }

    public string? Text { get; set; }

    public string? CorrectAnswer { get; set; }

    public int? Score { get; set; }

    public virtual ICollection<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; } = new List<MultipleChoiceQuestion>();

    public virtual Quiz? Quiz { get; set; }
}
