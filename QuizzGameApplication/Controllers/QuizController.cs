using Microsoft.AspNetCore.Mvc;
using QuizGame.Models;
using QuizGame.Service;

namespace QuizzGameApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService service;

        public QuizController(IQuizService quizService) 
        {
            service = quizService;
        }

        /// <summary>
        /// Get a quiz by id 
        /// </summary>
        /// <returns>A enumerable of quizzes</returns>
        /// <response code="200">Returns the requested Quiz</response>
        /// <response code="404">Not found</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Quiz> GetById([FromRoute] int id)
        {
            Quiz quizFound = service.GetQuizById(id);
            if(quizFound != null)
            {
                return Ok(quizFound);
            }
            return NotFound();
        }

        /// <summary>
        /// Get quizzes 
        /// </summary>
        /// <returns>A enumerable of quizzes</returns>
        /// <response code="200">Returns the requested Quizzes</response>
        /// <response code="400">Bad Request</response>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(Name = "GetQuiz")]
        public IEnumerable<Quiz> Get()
        {
            return service.GetAllQuizzes();
        }

        /// <summary>
        /// Add a quiz
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
        [HttpPost(Name = "PostQuizzes")]
        public ActionResult Post([FromBody]Quiz quiz) 
        {
            if(ModelState.IsValid)
            {
                service.AddQuiz(quiz);
                return Created("/api/Quiz/{id}", quiz);
            }
            return UnprocessableEntity();
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
        [HttpDelete]
        [Route("{id}")]
        public ActionResult<Quiz> Delete([FromRoute] int id) 
        {
            Quiz quizFound = service.GetQuizById(id);
            if (quizFound != null)
            {
                service.DeleteQuiz(id);
                return Ok(quizFound);
            }
            return NotFound();
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
        [HttpPut]
        [Route("{id}")]
        public ActionResult<Quiz> Put([FromRoute] int id, [FromBody] Quiz quiz)
        {
            Quiz quizFound = service.GetQuizById(id);
            if (quizFound != null)
            {
                service.UpdateQuiz(id, quiz);
                return Ok(quizFound);
            }
            return NotFound();
        }
    }
}
