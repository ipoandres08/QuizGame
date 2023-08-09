using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizGame.Models;
using QuizGame.Service;

namespace QuizzGameApplication.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        /// <summary>
        /// Get questions
        /// </summary>
        /// <returns>A enumerable of questions</returns>
        /// <response code="200">Returns the requested questions</response>
        /// <response code="400">Bad Request</response>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(Name = "GetQuestions")]
        public IEnumerable<Question> Get()
        {
            return _questionService.GetAllQuestions();
        }

        /// <summary>
        /// Get a question by id 
        /// </summary>
        /// <returns>A enumerable of quizzes</returns>
        /// <response code="200">Returns the requested Question</response>
        /// <response code="404">Not found</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{questionId}")]
        public Question GetQuestion([FromRoute] int questionId)
        {
            return _questionService.GetQuestion(questionId);
        }

        /// <summary>
        /// Add a question
        /// </summary>
        /// <returns>Empty</returns>
        /// <response code="204">Returns nothing</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unautharized</response>
        /// <response code="422">This is not valid</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public Question Post([FromBody] Question question)
        {
            return _questionService.AddQuestion(question);
        }

        /// <summary>
        /// Delete a question
        /// </summary>
        /// <param name="id">This is the quiz Id</param>
        /// <returns>Empty</returns>
        /// <response code="200">Returns nothing</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not found</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public void Delete(int questionId)
        {
            _questionService.DeleteQuestion(questionId);
        }

        /// <summary>
        /// Edit a Question
        /// </summary>
        /// <param name="id">This is the quiz Id</param>
        /// <returns>Empty</returns>
        /// <response code="200">Returns nothing</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unautharized</response>
        /// <response code="404">Not found</response>
        /// <response code="422">This is not valid</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        [Route("{questionId}")]
        public Question Put([FromBody] Question question, [FromRoute] int questionId)
        {
            return _questionService.UpdateQuestion(questionId, question);
        }

    }
}
