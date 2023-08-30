using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentAssertions;
using NSubstitute;
using Collection = QuizGamePerssistence.Models.Collection;
using QuizGamePerssistence.Models;
using QuizGamePerssistence.Repositories;
using QuizGame.Service;

namespace RepositoryTests
{
    public class CategoryRepositoryTests
    {
        private readonly IQuizRepository _quizRepository;

        public CategoryRepositoryTests()
        {
            _quizRepository = Substitute.For<IQuizRepository>();
        }

        [Fact]
        public async Task GetCategory_WhenCategoryExists_ReturnsCategory()
        {
            // Arrange
            var dbContextOptions = CreateNewContextOptions();
            using var context = new QuizGameContext(dbContextOptions);
            var collectionRepository = new CollectionRepository(
                context, _quizRepository);
            Guid id = new Guid();
            Collection newCollection = new Collection();
            newCollection.CollectionId = id;
            newCollection.Description = "Description";
            newCollection.Name = "None";
            await context.Collections.AddAsync(newCollection);
            await context.SaveChangesAsync();

            var collections = await context.Collections.ToArrayAsync();

            // Act
            var categoryResult = await collectionRepository
                .GetCollectionByID(newCollection.CollectionId, default);

            // Assert
            categoryResult.IsT0.Should().BeTrue();
        }

        private DbContextOptions<QuizGameContext> CreateNewContextOptions()
        {
            // Use in-memory database for testing
            var optionsBuilder =
                new DbContextOptionsBuilder<QuizGameContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging();

            return optionsBuilder.Options;
        }

    }
}