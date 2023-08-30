using FluentValidation;
using Mapster;
using OneOf;
using QuizGamePerssistence.Models;
using QuizGamePerssistence.Models.DTOs;
using QuizGamePerssistence.Repositories;
using System.Net;

namespace QuizGame.Service
{
    public class CollectionService : ICollectionService
    {
        private readonly IValidator<CollectionForUpsert> _validator;
        private readonly ICollectionRepository _collectionRepository;

        public CollectionService(
            IValidator<CollectionForUpsert> validator,
            ICollectionRepository collectionRepository)
        {
            ArgumentNullException.ThrowIfNull(validator);
            ArgumentNullException.ThrowIfNull(collectionRepository);
            _validator = validator;
            _collectionRepository = collectionRepository;
        }

        public async Task<OneOf<Collection, RequestError>> CreateCollection(CollectionForUpsert newCollection, bool addNewQuizzes, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(newCollection);
            if (!result.IsValid)
            {
                return new RequestError(
                HttpStatusCode.UnprocessableEntity,
                result.ToString());
            }

            var createdCollection = await _collectionRepository
                    .AddCollection(newCollection.Adapt<Collection>(), cancellationToken, addNewQuizzes);
            if (createdCollection.IsT1)
            {
                return createdCollection.AsT1;
            }

            var createdCollectionDto = createdCollection.AsT0.Adapt<Collection>();
            return createdCollectionDto;
        }

        public async Task<OneOf<Collection, RequestError>> DeleteCollection(Guid id, CancellationToken cancellationToken)
        {
            var collectionResult = await _collectionRepository.DeleteCollection(id, cancellationToken);
            if (collectionResult.IsT1)
            {
                return collectionResult.AsT1;
            }

            var foundedCollection = collectionResult.AsT0;
            return foundedCollection;
        }

        public async Task<IEnumerable<Collection>> RetrieveCollections(CancellationToken cancellationToken)
        {
            var collections = await _collectionRepository
            .GetCollections(cancellationToken);

            var collectionsDtos = collections.Adapt<ICollection<Collection>>();
            return collectionsDtos;
        }

        public async Task<OneOf<Collection, RequestError>> RetrieveCollection(Guid id, CancellationToken cancellationToken)
        {
            var collectionResult = await _collectionRepository.GetCollectionByID(id, cancellationToken);
            if (collectionResult.IsT1)
            {
                return collectionResult.AsT1;
            }

            var foundedCollection = collectionResult.AsT0;
            return foundedCollection;
        }

        public async Task<OneOf<Collection, RequestError>> UpdateCollection(Guid id, CollectionForUpsert collection, CancellationToken cancellationToken)
        {
            var collectionToUpdate = collection.Adapt<Collection>();
            var collectionResult = await _collectionRepository.UpdateCollection(id, collectionToUpdate, cancellationToken);

            if (collectionResult.IsT1)
            {
                return collectionResult.AsT1;
            }

            var foundedCollection = collectionResult.AsT0;
            return foundedCollection;
        }
    }
}
