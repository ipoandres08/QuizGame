using OneOf;
using QuizGamePerssistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGamePerssistence.Repositories
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> GetQuizzes(CancellationToken cancellationToken);
        Task<OneOf<Quiz, RequestError>> GetQuizByID(Guid id, CancellationToken cancellationToken);
        Task<(IEnumerable<Quiz> existingQuizzes, IEnumerable<Guid> newQuizzes)> GetQuizzesByID(IEnumerable<Guid> ids, CancellationToken cancellationToken);
        Task<OneOf<Quiz, RequestError>> AddQuiz(Quiz quiz, CancellationToken cancellationToken);
        Task<OneOf<IEnumerable<Quiz>, RequestError>> AddQuizzes(IEnumerable<Quiz> newQuizzes, CancellationToken cancellationToken);
        Task<OneOf<Quiz, RequestError>> DeleteQuiz(Guid id, CancellationToken cancellationToken);
        Task<OneOf<Quiz, RequestError>> UpdateQuiz(Guid id, Quiz quiz, CancellationToken cancellationToken);
    }
}
