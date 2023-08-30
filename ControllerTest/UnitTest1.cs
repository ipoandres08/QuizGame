using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using QuizGame.Service;
using QuizGamePerssistence.Models;
using QuizzGameApplication.Controllers;

namespace QuizCraft.Api.Tests;

public class CollectionsControllerTests
{
    private readonly ICollectionService _collectionService;

    public CollectionsControllerTests()
    {
        _collectionService = Substitute.For<ICollectionService>();
    }

    [Fact]
    public async Task GetCollection_ReturnsSuccessfulState()
    {
        Guid id = Guid.NewGuid();
        _collectionService.RetrieveCollection(id, default)
            .Returns(new Collection());

        var controller = ControllerInstance();

        //Act
        var result = await controller.GetCollectionById(id, default);

        //Assert
        (result.Result as OkObjectResult).StatusCode.Should().Be(200);
    }

    private CollectionController ControllerInstance() =>
        new(_collectionService);
}