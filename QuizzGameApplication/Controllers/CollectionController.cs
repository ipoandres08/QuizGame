using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuizzGameApplication.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CollectionController : ControllerBase
    {

    }
}
