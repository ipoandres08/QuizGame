using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using QuizGame.Models;
using QuizGamePerssistence.Models.DTOs;

namespace QuizGame.Validators
{
    public class QuizValidator : AbstractValidator<QuizForUpsert>
    {
        public QuizValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Description).MaximumLength(150);
        }
    }
}
