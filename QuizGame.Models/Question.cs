using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Question
    {
        public string Text { get; set; }
        public string CorrectAnswer { get; set; }
        public int Score { get; set; }

        public Question(string text, string correctAnswer, int score) 
        { 
            this.Text = text;
            this.CorrectAnswer = correctAnswer;
            this.Score = score;
        }

        public override string ToString() 
        {
            return this.Text + "\n";
        }
    }
}
