using Moq;
using ToDoApp.Application.Queries;
using ToDoApp.Application.Queries.Handlers;
using ToDoApp.Infrastructure.Entities;
using ToDoApp.Infrastructure.Repositories;

namespace ToDoApp.Tests
{
    public class GetToDoItemHandlerTests
    {
        private readonly Mock<IToDoRepository> _repositoryMock;
        private readonly GetToDoItemHandler _handler;

        public GetToDoItemHandlerTests()
        {
            _repositoryMock = new Mock<IToDoRepository>();
            _handler = new GetToDoItemHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnToDoItemEntityDto_WhenEntityExists()
        {
            // Arrange
            const int requestId = 1;
            var mockToDoItem = new ToDoItem { Id = requestId, Title = "Test Task", Description = "Test Description" };

            _repositoryMock
                .Setup(repo => repo.GetItemAsync(requestId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockToDoItem);

            // Act
            var result = await _handler.Handle(new GetToDoItem(requestId), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockToDoItem.Id, result.Id);
            Assert.Equal(mockToDoItem.Title, result.Title);
            Assert.Equal(mockToDoItem.Description, result.Description);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenEntityDoesNotExist()
        {
            // Arrange
            const int requestId = 1;
            
            _repositoryMock
                .Setup(repo => repo.GetItemAsync(requestId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((ToDoItem)null!);

            // Act
            var result = await _handler.Handle(new GetToDoItem(requestId), CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            const int requestId = 1;
            
            _repositoryMock
                .Setup(repo => repo.GetItemAsync(requestId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Repository exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                await _handler.Handle(new GetToDoItem(requestId), CancellationToken.None));
        }
    }
}
