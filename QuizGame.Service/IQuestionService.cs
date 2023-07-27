using QuizGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public interface IQuestionService
    {
        Question AddQuestion(Question question);
        List<Question> GetAllQuestions();
        Question GetQuestion(int questionId);
        void DeleteQuestion(int questionId);
        Question UpdateQuestion(int questionId, Question question);
    }
}
