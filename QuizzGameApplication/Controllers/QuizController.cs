using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizGamePerssistence.Models;
using QuizGame.Service;
using QuizGamePerssistence.Models.DTOs;
using QuizzGameApplication.Helpers;
using OneOf.Types;

namespace QuizzGameApplication.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService service;

        public QuizController(IQuizService quizService) 
        {
            service = quizService;
        }
        /*
        /// <summary>
        /// Get a quiz by id 
        /// </summary>
        /// <returns>A enumerable of quizzes</returns>
        /// <response code="200">Returns the requested Quiz</response>
        /// <response code="404">Not found</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Quiz>> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await service.RetrieveQuizz(id, cancellationToken);
            if (result.IsT0)
            {
                return Ok(result);
            }
            return result.HandleError(this);
        }
        
        /// <summary>
        /// Get quizzes 
        /// </summary>
        /// <returns>A enumerable of quizzes</returns>
        /// <response code="200">Returns the requested Quizzes</response>
        /// <response code="400">Bad Request</response>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IEnumerable<Quiz>> Get(CancellationToken cancellationToken)
        {
            return await service.RetrieveQuizzes(cancellationToken);
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
        public async Task<ActionResult> Post([FromBody] QuizForUpsert quiz, CancellationToken cancellationToken) 
        {
            var result = await service.CreateQuizz(quiz, cancellationToken);
            if (result.IsT0)
            {
                // TODO: Improve this
                var resourceURL = Url.Action(
                    "GetCollection",
                    "Collections",
                    new { result.AsT0.CollectionId, cancellationToken }, Request.Scheme);
                return Created("Created", result.AsT0);
            }
            return result.HandleError(this);
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
        public async Task<ActionResult<Quiz>> Delete([FromRoute] Guid id, CancellationToken cancellationToken) 
        {
            var result = await service.DeleteQuizz(id, cancellationToken);
            if (result.IsT0)
            {
                return Ok(result.AsT0);
            }
            return result.HandleError(this);
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
        public async Task<ActionResult<Quiz>> Put([FromRoute] Guid id, [FromBody] QuizForUpsert quiz, CancellationToken cancellationToken)
        {
            var result = await service.UpdateQuizz(id, quiz, cancellationToken);
            if (result.IsT0)
            {
                return Ok(result.AsT0);
            }
            return result.HandleError(this);
        }*/
    }
}
