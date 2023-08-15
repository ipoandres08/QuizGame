using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGamePerssistence.Constants
{
    public static class Constants
    {
        public static class RequestErrorMessages
        {
            public const string QuizNotFound = "Quiz was not found.";
            public const string QuestionNotFound = "Question was not found.";
            public const string NoChanges = "No changes were applied.";
            public const string CategoryNotFound = "Category was not found.";
        }

        public static class ValidationMessages
        {
            public const string NameNotUnique = "Name must be unique.";
        }
    }
}
