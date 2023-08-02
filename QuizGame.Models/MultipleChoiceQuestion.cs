using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace QuizGame.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public List<string> Choices { get; set; }

        public MultipleChoiceQuestion(int questionID, string text, string correctAnswer, int score, List<string> choices) : base(questionID, text, correctAnswer, score)
        {
            this.Choices = choices;
        }

        public override string ToString()
        {
            StringBuilder question = new StringBuilder();
            question.Append(base.ToString());
            foreach (string choice in Choices)
            {
                question.AppendLine(choice);
            }
            question.AppendLine();
            question.AppendLine("Enter your answer: ");
            return question.ToString();
        }
    }
}
