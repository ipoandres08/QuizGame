using FluentValidation;
using Mapster;
using OneOf;
using QuizGamePerssistence.Models;
using QuizGamePerssistence.Models.DTOs;
using QuizGamePerssistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public class QuizService : IQuizService
    {
        private readonly IValidator<QuizForUpsert> _validator;
        private readonly IQuizRepository _quizRepository;

        public QuizService(IValidator<QuizForUpsert> validator, IQuizRepository quizRepository)
        {
            ArgumentNullException.ThrowIfNull(validator);
            ArgumentNullException.ThrowIfNull(quizRepository);
            _validator = validator;
            _quizRepository = quizRepository;
        }

        public Task<OneOf<Quiz, RequestError>> CreateQuizz(QuizForUpsert newCollection, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OneOf<Quiz, RequestError>> DeleteQuizz(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OneOf<Quiz, RequestError>> RetrieveQuizz(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Quiz>> RetrieveQuizzes(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OneOf<Quiz, RequestError>> UpdateQuizz(Guid id, QuizForUpsert collection, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
