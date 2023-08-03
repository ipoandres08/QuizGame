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
        Quiz AddQuiz(Quiz quiz);
        List<Quiz> GetAllQuizzes();
        Quiz GetQuizById(int id);
        void DeleteQuiz(int id);
        Quiz UpdateQuiz(int id, Quiz quiz);
    }
}
