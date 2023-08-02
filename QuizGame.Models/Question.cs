using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Question
    {
        public int QuizId { get; set; }
        public int QuestionId { get; set; } = new Random().Next();
        public string Text { get; set; }
        public string CorrectAnswer { get; set; }
        public int Score { get; set; }

        public Question(int quizId, string text, string correctAnswer, int score) 
        { 
            this.QuizId = quizId;
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
