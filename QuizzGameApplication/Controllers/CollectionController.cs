using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizGame.Service;
using QuizGamePerssistence.Models;
using QuizGamePerssistence.Models.DTOs;
using QuizzGameApplication.Helpers;

namespace QuizzGameApplication.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(Name = "GetCollections")]
        public async Task<IEnumerable<Collection>> GetCollection(CancellationToken cancellationToken)
        {
            return await _collectionService.RetrieveCollections(cancellationToken);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Collection>> GetCollectionById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _collectionService.RetrieveCollection(id, cancellationToken);

            if (result.IsT0)
            {
                return Ok(result);
            }
            return result.HandleError(this);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Collection>> CreateCollection([FromBody] CollectionForUpsert collectioToCreate, [FromQuery] bool addNewQuizzes, CancellationToken cancellationToken)
        {
            var result = await _collectionService.CreateCollection(collectioToCreate, addNewQuizzes, cancellationToken);
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
        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Collection>> UpdateCollection(Guid id, [FromBody] CollectionForUpsert collectioToUpdate, CancellationToken cancellationToken)
        {
            var result = await _collectionService.UpdateCollection(id, collectioToUpdate, cancellationToken);
            if (result.IsT0)
            {
                return Ok(result.AsT0);
            }
            return result.HandleError(this);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Collection>> DeleteCollection(Guid id, CancellationToken cancellationToken)
        {
            var result = await _collectionService.DeleteCollection(id, cancellationToken);
            if (result.IsT0)
            {
                return Ok(result.AsT0);
            }
            return result.HandleError(this);
        }
    }
}
