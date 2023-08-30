using FluentAssertions;
using FluentAssertions.Execution;
using FluentValidation;
using NSubstitute;
using QuizGame.Service;
using QuizGamePerssistence.Models;
using QuizGamePerssistence.Models.DTOs;
using QuizGamePerssistence.Repositories;
using Collection = QuizGamePerssistence.Models.Collection;

namespace ServiceTest
{
    public class CollectionsServiceTests
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IValidator<CollectionForUpsert> _validator;

        public CollectionsServiceTests()
        {
            _collectionRepository = Substitute.For<ICollectionRepository>();
            _validator = Substitute.For<IValidator<CollectionForUpsert>>();
        }

        [Fact]
        public async Task GetCollection_ReturnsSuccessfulState()
        {
            Guid id = Guid.NewGuid();
            Collection collectionFind = new Collection();
            collectionFind.CollectionId = id;
            _collectionRepository.GetCollectionByID(id, default)
                .ReturnsForAnyArgs(collectionFind);

            var controller = ServiceInstance();

            //Act
            var result = await controller.RetrieveCollection(id, default);

            //Assert
            using AssertionScope scope = new();
            result.Value.Should().BeOfType<Collection>();
            result.AsT0.CollectionId.Should().Be(id);
        }

        private CollectionService ServiceInstance() =>
            new(_validator, _collectionRepository);
    }
}
