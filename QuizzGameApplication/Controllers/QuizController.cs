using Microsoft.AspNetCore.Mvc;
using QuizGame.Models;
using QuizGame.Service;

namespace QuizzGameApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        DataService service;
        Dictionary<int, Quiz> quizzes = new Dictionary<int, Quiz>();
        int i = 0;
        public QuizController() 
        {
            service = new DataService();
            //quizzes = service.ReadQuizzes();
        }

        /// <summary>
        /// Get quizzes 
        /// </summary>
        /// <returns>A enumerable of quizzes</returns>
        /// <response code="200">Returns the requested Quizzes</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not found</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Quiz))]
        [HttpGet(Name = "GetQuiz")]
        public IEnumerable<Quiz> Get()
        {
            return quizzes.Values;
        }

        /// <summary>
        /// Add a quiz
        /// </summary>
        /// <returns>Empty</returns>
        /// <response code="201">Returns nothing</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unautharized</response>
        /// <response code="404">Not found</response>
        /// <response code="422">This is not valid</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost(Name = "PostQuizzes")]
        public void Post([FromBody]Quiz quiz) 
        {
            quizzes.Add(i, quiz);
            i++;
        }

        /// <summary>
        /// Delete a quiz
        /// </summary>
        /// <param name="id">This is the quiz Id</param>
        /// <returns>Empty</returns>
        /// <response code="200">Returns nothing</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not found</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("delete/{id}")]
        public void Delete([FromRoute] int id) 
        {
                quizzes.Remove(id);
        }

        /// <summary>
        /// Edit a quiz
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("put/{id}")]
        public void Put([FromRoute] int id, [FromBody] Quiz quiz)
        {
            quizzes[id] = quiz;
        }
    }
}
