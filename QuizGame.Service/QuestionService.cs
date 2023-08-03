using QuizGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly Dictionary<int, Question> questionsList = new Dictionary<int, Question>();
        int i = 0;

        public Question AddQuestion(Question question)
        {
            questionsList.Add(i, question);
            i++;
            return question;
        }

        public void DeleteQuestion(int questionId)
        {
            if (!FindQuestion(questionId))
            {
                throw new ArgumentException("The question Id doesn't exists");
            }
            questionsList.Remove(questionId);
        }

        public List<Question> GetAllQuestions()
        {
            return questionsList.Values.ToList();
        }

        public Question GetQuestion(int questionId)
        {
            if(!FindQuestion(questionId))
            {
                throw new ArgumentException("The question Id doesn't exists");
            }
            return questionsList[questionId];
        }

        public Question UpdateQuestion(int questionId, Question question)
        {
            return questionsList[questionId] = question;
        }

        private bool FindQuestion(int questionId)
        {
            return questionsList.ContainsKey(questionId);
        }
    }
}
