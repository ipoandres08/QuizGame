using OneOf;
using QuizGamePerssistence.Models;
using QuizGamePerssistence.Models.DTOs;

namespace QuizGame.Service
{
    public  interface IQuizService
    {

        Task<IEnumerable<Quiz>> RetrieveQuizzes(
      CancellationToken cancellationToken);
        Task<OneOf<Quiz, RequestError>> RetrieveQuizz(
            Guid id, CancellationToken cancellationToken);
        Task<OneOf<Quiz, RequestError>> CreateQuizz(
            QuizForUpsert newCollection, CancellationToken cancellationToken);
        Task<OneOf<Quiz, RequestError>> UpdateQuizz(
            Guid id, QuizForUpsert collection, CancellationToken cancellationToken);
        Task<OneOf<Quiz, RequestError>> DeleteQuizz(
            Guid id, CancellationToken cancellationToken);

    }
}
