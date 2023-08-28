using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGamePerssistence.Models.DTOs
{
    public class QuizForUpsert
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
