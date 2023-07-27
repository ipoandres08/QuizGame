using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizGame.Models;
using QuizzGameApplication.Services;

namespace QuizzGameApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRequestService _authenticationService;
        public AuthenticationController(IAuthenticationRequestService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            return _authenticationService.AuthenticateRequest(authenticationRequestBody);
        }
    }
}
