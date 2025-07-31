using Core.Entities;
using Infrastructure.Base;
using Infrastructure.Data;

namespace API.Tests;
using Moq;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class GenericRepositoryTests
{
    [Fact]
    public async Task AddAsync_AddsEntityToDbSet()
    {
        var mockSet = new Mock<DbSet<Course>>();
        var mockContext = new Mock<AppDbContext>();
        mockContext.Setup(m => m.Set<Course>()).Returns(mockSet.Object);

        var repo = new GenericRepository<Course>(mockContext.Object);

        var entity = new Course();
        await repo.AddAsync(entity);

        mockSet.Verify(m => m.AddAsync(entity, default), Times.Once);
    }
}