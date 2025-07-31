using Core.Entities;
using Moq;
using Xunit;
using Infrastructure.Base;
using Infrastructure.Data;

public class UnitOfWorkTests
{
    [Fact]
    public void Repository_ReturnsGenericRepositoryInstance()
    {
        // Arrange
        var mockContext = new Mock<AppDbContext>();
        var unitOfWork = new UnitOfWork(mockContext.Object);

        // Act
        var repo = unitOfWork.Repository<Course>();

        // Assert
        Assert.NotNull(repo);
        Assert.IsType<GenericRepository<Course>>(repo);
    }
}