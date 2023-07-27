using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizGame.Models;

namespace QuizGame.Validators
{
    public class QuizValidator : IValidatableObject
    {
        [Required(ErrorMessage = "You should fill out a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You should fill out a description.")]
        public string Description { get; set; }
        public List<Categorie> Categories { get; set; }

        public List<Question> Questions { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Questions.Count < 1)
            {
                yield return new ValidationResult(
                    "The list should have at least one question.",
                    new[] { "QuizValidator" });
            }
        }
    }
}
