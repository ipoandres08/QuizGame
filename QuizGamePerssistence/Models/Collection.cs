using System;
using System.Collections.Generic;

namespace QuizGamePerssistence.Models;

public partial class Collection
{
    public Guid CollectionId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
