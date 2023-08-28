using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneOf;
using QuizGamePerssistence.Models;
using System.Net;
using static QuizGamePerssistence.Constants.Constants;

namespace QuizGamePerssistence.Repositories
{
    public class CollectionRepository: ICollectionRepository
    {
        private readonly QuizGameContext _context;
        private readonly IQuizRepository _quizRepository;  
        //private readonly IValidator<Collection> _validator;

        public CollectionRepository(QuizGameContext context, IQuizRepository quizRepository)
        {
            _context = context;
            _quizRepository = quizRepository;
            //_validator = validator;
        }

        public async Task<OneOf<Collection, RequestError>> AddCollection(Collection collection, bool addNewQuizzes = false, CancellationToken cancellationToken)
        {
            /*
            var validationResult = await _validator.ValidateAsync(collection);
            if (!validationResult.IsValid)
            {
                return new RequestError(
                    HttpStatusCode.UnprocessableEntity, validationResult.ToString());
            }*/

            if (!collection.Quizzes.Any())
            {
                await _context.Collections.AddAsync(collection, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return collection;
            }

            using var transaction = await _context.Database
                .BeginTransactionAsync(cancellationToken);
            // Adding Quizzes
            try
            {
                var (existingQuizzes, newQuizzes) =
                    await _quizRepository.GetQuizzesByID(
                    collection.Quizzes.Select(c => c.QuizId).ToArray(), cancellationToken);

                if (!addNewQuizzes)
                {
                    return new RequestError(
                        HttpStatusCode.BadRequest,
                        $"Collection contains non existing quizzes:" +
                        $" {string.Join(',', newQuizzes)}");
                }

                var addQuizzes = collection.Quizzes.Where(cq => newQuizzes.Contains(cq.QuizId));
                collection.Quizzes.Clear();
                collection.Quizzes = existingQuizzes.ToList();

                if (newQuizzes.Any())
                {
                    var result = await _quizRepository
                        .AddQuizzes(addQuizzes, cancellationToken);

                    if (result.IsT1)
                    {
                        return result.AsT1;
                    }

                    collection.Quizzes.ToList().AddRange(result.AsT0);
                }

                await _context.Collections.AddAsync(collection, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return collection;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<OneOf<Collection, RequestError>> DeleteCollection(Guid id, CancellationToken cancellationToken)
        {
            var foundedCollection = await GetCollectionByID(id, cancellationToken);
            if (foundedCollection.IsT1)
            {
                return foundedCollection;
            }

            _context.Collections.Remove(foundedCollection.AsT0);
            //var debugView = _context.ChangeTracker.DebugView.ShortView; //TODO Remove once its used
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result == 0)
            {
                return new RequestError(
                    HttpStatusCode.BadRequest, RequestErrorMessages.NoChanges);
            }

            return foundedCollection;
        }

        public async Task<OneOf<Collection, RequestError>> GetCollectionByID(Guid id, CancellationToken cancellationToken)
        {
            var collection =  await _context.Collections
                .Where(collection => collection.CollectionId == id)
                .FirstOrDefaultAsync(cancellationToken);

            if (collection is null)
            {
                return new RequestError(HttpStatusCode.NotFound, RequestErrorMessages.CategoryNotFound);
            }

            return collection;
        }

        public async Task<IEnumerable<Collection>> GetCollections(CancellationToken cancellationToken)
        {
            return await _context.Collections.ToListAsync(cancellationToken);
        }

        public async Task<OneOf<Collection, RequestError>> UpdateCollection(Guid id, Collection collection, CancellationToken cancellationToken)
        {
            var foundedCollection = await _context.Collections
           .Where(x => x.CollectionId == id)
           .FirstOrDefaultAsync(cancellationToken);
            if (foundedCollection is null)
            {
                return new RequestError(
                    HttpStatusCode.NotFound, RequestErrorMessages.CollectionNotFound);
            }
            /*
            var validationResult = await _validator
                .ValidateAsync(collection);
            if (!validationResult.IsValid)
            {
                return new RequestError(
                    HttpStatusCode.UnprocessableEntity, validationResult.ToString());
            }*/

            foundedCollection.Name = collection.Name;
            foundedCollection.Description = collection.Description;
            //var debugView = _context.ChangeTracker.DebugView.ShortView; //TODO Remove once its used
            var result = await _context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                return new RequestError(
                    HttpStatusCode.BadRequest, RequestErrorMessages.NoChanges);
            }

            return collection;
        }
    }
}
