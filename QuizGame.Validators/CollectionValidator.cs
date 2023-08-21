using FluentValidation;
using QuizGame.Models;
using QuizGamePerssistence.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Validators
{
    public class CollectionValidator : AbstractValidator<CollectionForUpsert>
    {
        public CollectionValidator() 
        { 
            RuleFor(c =>  c.Name).NotEmpty();
            RuleFor(c => c.Description).MaximumLength(150);
        }
    }
}
