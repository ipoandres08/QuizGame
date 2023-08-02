using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class FillBlankQuestion : Question
    {
        public int BlankPosition { get; set; }

        public FillBlankQuestion(int questionID, string text, string correctAnswer, int score, int blankPosition) : base(questionID, text, correctAnswer, score)
        {
            BlankPosition = blankPosition;
        }

        public override string ToString()
        {
            return $"{base.ToString()}Enter your answer: ";
        }
    }
}
