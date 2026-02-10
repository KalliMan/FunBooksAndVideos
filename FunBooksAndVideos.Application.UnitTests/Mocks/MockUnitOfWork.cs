using FunBooksAndVideos.Application.Contracts.Persistence;
using Moq;

namespace FunBooksAndVideos.Application.UnitTests.Mocks;

internal class MockUnitOfWork
{
    public static Mock<IUnitOfWork> GetMock()
    {
        var mock = new Mock<IUnitOfWork>();

        mock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        mock.Setup(uow => uow.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        mock.Setup(uow => uow.CommitTransactionAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        mock.Setup(uow => uow.RollbackTransactionAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        return mock;
    }
}
