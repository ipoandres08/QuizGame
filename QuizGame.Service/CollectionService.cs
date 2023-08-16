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
    internal class CollectionService : ICollectionService
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

        public async Task<OneOf<Collection, RequestError>> CreateCollection(CollectionForUpsert newCollection, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(newCollection);
            if (!result.IsValid)
            {
                return new RequestError(
                HttpStatusCode.UnprocessableEntity,
                result.ToString());
            }

            var createdCollection = await _collectionRepository
                    .AddCollection(newCollection.Adapt<Collection>(), cancellationToken);
            if (createdCollection.IsT1)
            {
                return createdCollection.AsT1;
            }

            var createdCollectionDto = createdCollection.AsT0.Adapt<Collection>();
            return createdCollectionDto;
        }

        public Task<OneOf<Collection, RequestError>> DeleteCollection(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Collection>> RetrieveCollections(CancellationToken cancellationToken)
        {
            var collections = await _collectionRepository
            .GetCollections(cancellationToken);

            var collectionsDtos = collections.Adapt<ICollection<Collection>>();
            return collectionsDtos;
        }

        public Task<OneOf<Collection, RequestError>> RetrieveCollection(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OneOf<Collection, RequestError>> UpdateCollection(Guid id, CollectionForUpsert collection, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
