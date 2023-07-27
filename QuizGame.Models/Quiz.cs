using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Quiz : IValidatableObject
    {
        [Required(ErrorMessage = "You should fill out a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You should fill out a description.")]
        public string Description { get; set; }
        public List<Categorie> Categories { get; set; }

        public List<Question> Questions { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Questions.Count < 1)
            {
                yield return new ValidationResult(
                    "The list should have at least one question.",
                    new[] { "QuizValidator" });
            }
        }

        public Quiz(string name, string description, List<Categorie> categories, List<Question> questions) 
        {
            Name = name;
            Description = description;
            Categories = categories;
            Questions = questions;
        }
    }
}
