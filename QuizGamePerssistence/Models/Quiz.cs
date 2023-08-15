using System;
using System.Collections.Generic;

namespace QuizGamePerssistence.Models;

public partial class Quiz
{
    public Guid QuizId { get; set; }

    public Guid? CollectionId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual Collection? Collection { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
