using Microsoft.EntityFrameworkCore;

public class UserRepositoryTests
{
    private UserRepository repository;

    public UserRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var dbContext = new ApplicationDbContextTests(options);

        repository = new UserRepository(dbContext);
    }

    [Fact]
    public async Task GetByEmail_WithoutEmailOnDb()
    {
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
            async () => await repository.GetByEmail("123@gmail.com")
        );

        Assert.Contains("Usuario não encontrado", exception.Message);
    }

    [Fact]
    public async Task GetByEmail_WithEmptyString()
    {
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await repository.GetByEmail("")
        );

        Assert.Contains("O email não pode ser vazio", exception.Message);
    }

    [Fact]
    public async Task RemoveByEmail_WithoutEntityOnDb()
    {
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
            async () => await repository.RemoveByEmail("123@gmail.com")
        );

        Assert.Contains("Usuario não encontrado", exception.Message);
    }

    [Fact]
    public async Task RemoveByEmail_WithEmptyString()
    {
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await repository.RemoveByEmail("")
        );

        Assert.Contains("O email não pode ser vazio", exception.Message);
    }
}
