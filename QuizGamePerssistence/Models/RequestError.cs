using System.Net;

namespace QuizGamePerssistence.Models
{
    public record RequestError(HttpStatusCode StatusCode, string Message);
}
