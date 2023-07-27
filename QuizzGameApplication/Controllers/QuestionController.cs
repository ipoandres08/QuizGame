using Microsoft.AspNetCore.Mvc;
using QuizGame.Models;
using QuizGame.Service;

namespace QuizzGameApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(ILogger<QuestionController> logger, IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet(Name = "GetQuestions")]
        public IEnumerable<Question> Get()
        {
            return _questionService.GetAllQuestions();
        }

        [HttpGet]
        [Route("{questionId}")]
        public Question GetQuestion([FromRoute] int questionId)
        {
            return _questionService.GetQuestion(questionId);
        }

        [HttpPost]
        public Question Post([FromBody] Question question)
        {
            return _questionService.AddQuestion(question);
        }

        [HttpDelete]
        public void Delete(int questionId)
        {
            _questionService.DeleteQuestion(questionId);
        }

        [HttpPut]
        [Route("{questionId}")]
        public Question Put([FromBody] Question question, [FromRoute] int questionId)
        {
            return _questionService.UpdateQuestion(questionId, question);
        }

    }
}
