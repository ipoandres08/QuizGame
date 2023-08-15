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
        private readonly IValidator<Collection> _validator;

        public CollectionRepository(QuizGameContext context, IValidator<Collection> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<OneOf<Collection, RequestError>> AddCollection(Collection collection, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(collection);
            if (!validationResult.IsValid)
            {
                return new RequestError(
                    HttpStatusCode.UnprocessableEntity, validationResult.ToString());
            }

            await _context.Collections
                .AddAsync(collection, cancellationToken);
            var debugView = _context.ChangeTracker.DebugView.ShortView; //TODO Remove once its used
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result == 0)
            {
                return new RequestError(
                    HttpStatusCode.BadRequest, RequestErrorMessages.NoChanges);
            }

            return collection;
        }

        public Task<OneOf<Collection, RequestError>> DeleteCollection(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<OneOf<Collection, RequestError>> GetCollectionByID(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Collections
                .Where(collection => collection.CollectionId == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Collection>> GetCollections(CancellationToken cancellationToken)
        {
            return await _context.Collections.ToListAsync(cancellationToken);
        }

        public Task<OneOf<Collection, RequestError>> UpdateCollection(Guid id, Collection collection, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
