using QuizGame.Models;

namespace QuizzGameApplication.Services
{
    public interface IAuthenticationRequestService
    {
        string AuthenticateRequest(AuthenticationRequestBody authenticationRequestBody);
    }
}
