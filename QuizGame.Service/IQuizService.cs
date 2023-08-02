using QuizGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public  interface IQuizService
    {
        Question AddQuiz(Quiz quiz);
        List<Question> GetAllQuizzes();
        Question GetQuizById(int id);
        void DeleteQuiz(int id);
        Question UpdateQuiz(int id, Quiz quiz);
    }
}
