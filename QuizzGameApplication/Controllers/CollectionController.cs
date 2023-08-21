using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizGame.Service;
using QuizGamePerssistence.Models;

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
        public Task<IEnumerable<Collection>> Get(CancellationToken cancellationToken)
        {
            return _collectionService.RetrieveCollections(cancellationToken);
        }

    }
}
