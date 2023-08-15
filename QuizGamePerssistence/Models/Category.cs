using System;
using System.Collections.Generic;

namespace QuizGamePerssistence.Models;

public partial class Category
{
    public Guid CategorieId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
