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
        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationRequestService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            _logger.LogInformation("User Logged with this token ");
            return _authenticationService.AuthenticateRequest(authenticationRequestBody);
        }
    }
}
