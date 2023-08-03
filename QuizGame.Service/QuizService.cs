using QuizGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public class QuizService : IQuizService
    {
        private readonly List<Quiz> quizzes = new List<Quiz>();
        public Quiz AddQuiz(Quiz quiz)
        {
            quizzes.Add(quiz);
            return quiz;
        }

        public void DeleteQuiz(int id)
        {
            Quiz foundQuiz = GetQuizById(id);
            if (foundQuiz == null)
            {
                throw new ArgumentException("The Quiz Id doesn't exists");
            }
            quizzes.Remove(foundQuiz);
        }

        public List<Quiz> GetAllQuizzes()
        {
            return quizzes;
        }

        public Quiz GetQuizById(int id)
        {
            return quizzes.Find(q => q.Id == id);
        }

        public Quiz UpdateQuiz(int id, Quiz quiz)
        {
            Quiz foundQuiz = GetQuizById(id);
            if (foundQuiz == null)
            {
                throw new ArgumentException("The Quiz Id doesn't exists");
            }
            foreach(Quiz q in quizzes)
            {
                if(q.Id == id)
                {
                    q.Description = quiz.Description;
                    q.Name = quiz.Name;
                    q.Categories = quiz.Categories;
                    q.Questions = quiz.Questions;
                }
            }
            
            return quiz;
        }
    }
}
