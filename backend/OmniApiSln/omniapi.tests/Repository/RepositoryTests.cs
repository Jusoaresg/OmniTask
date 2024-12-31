using Microsoft.EntityFrameworkCore;

public class RepositoryTests
{
    private Repository<TestingClass> repository;

    public RepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var dbContext = new ApplicationDbContextTests(options);

        repository = new Repository<TestingClass>(dbContext);
    }

    [Fact]
    public async Task AddAsync_SendingNullEntity()
    {
        TestingClass? testingClass = null;

        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await repository.AddAsync(testingClass)
        );

        Assert.Equal("entity", exception.ParamName);
        Assert.Contains("A entidade não pode ser nula.", exception.Message);
    }

    [Fact]
    public async Task AddAsync_ShouldAddANewEntity()
    {
        TestingClass testingClass = new TestingClass
        {
            Id = Guid.NewGuid(),
            Name = "Roberto",
            Email = "123@gmail.com",
            Password = "123",
        };

        await repository.AddAsync(testingClass);
        await repository.SaveChangesAsync();
        var objFromDb = await repository.GetAsync(testingClass.Id);

        Assert.Equal(testingClass.Id, objFromDb.Id);
        Assert.Equal(testingClass.Name, objFromDb.Name);
        Assert.Equal(testingClass.Email, objFromDb.Email);
        Assert.Equal(testingClass.Password, objFromDb.Password);
    }

    [Fact]
    public async Task GetAsync_WithoutId()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await repository.GetAsync(Guid.Empty)
        );
        Assert.Contains("O Id não pode ser nulo", exception.Message);
    }

    [Fact]
    public async Task GetAsync_WithoutObjectToGet()
    {
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
            async () => await repository.GetAsync(Guid.NewGuid())
        );

        Assert.Contains("Não foi encontrada uma entidade com o id", exception.Message);
    }

    [Fact]
    public async Task GetAsync_WithEntity()
    {
        TestingClass testingClass = new TestingClass
        {
            Id = Guid.NewGuid(),
            Name = "Roberto",
            Email = "123@gmail.com",
            Password = "123",
        };

        await repository.AddAsync(testingClass);
        await repository.SaveChangesAsync();

        var result = await repository.GetAsync(testingClass.Id);

        Assert.NotNull(result);
        Assert.Equal(testingClass.Id, result.Id);
        Assert.Equal(testingClass.Name, result.Name);
        Assert.Equal(testingClass.Email, result.Email);
        Assert.Equal(testingClass.Password, result.Password);
    }

    [Fact]
    public async Task GetAllAsync_ReturnEntities_WhenExist()
    {
        TestingClass testingClass1 = new TestingClass
        {
            Id = Guid.NewGuid(),
            Name = "Roberto",
            Email = "123@gmail.com",
            Password = "123",
        };

        TestingClass testingClass2 = new TestingClass
        {
            Id = Guid.NewGuid(),
            Name = "Alexandre",
            Email = "321@hotmail.com",
            Password = "hotmail321.com",
        };

        await repository.AddAsync(testingClass1);
        await repository.AddAsync(testingClass2);

        await repository.SaveChangesAsync();

        var result = await repository.GetAllAsync();

        var firstEntity = result.First();
        var lastEntity = result.Last();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        Assert.Equal(testingClass1.Id, firstEntity.Id);
        Assert.Equal(testingClass1.Name, firstEntity.Name);
        Assert.Equal(testingClass1.Email, firstEntity.Email);
        Assert.Equal(testingClass1.Password, firstEntity.Password);

        Assert.Equal(testingClass2.Id, lastEntity.Id);
        Assert.Equal(testingClass2.Name, lastEntity.Name);
        Assert.Equal(testingClass2.Email, lastEntity.Email);
        Assert.Equal(testingClass2.Password, lastEntity.Password);
    }

    [Fact]
    public async Task RemoveAsync_WithoutObjectToRemove()
    {
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
            async () => await repository.RemoveAsync(Guid.NewGuid())
        );

        Assert.Contains("Usuario não encontrado", exception.Message);
    }

    [Fact]
    public async Task RemoveAsync_WithObject()
    {
        TestingClass testingClass = new TestingClass
        {
            Id = Guid.NewGuid(),
            Name = "Alexandre",
            Email = "321@hotmail.com",
            Password = "hotmail321.com",
        };

        await repository.AddAsync(testingClass);

        await repository.SaveChangesAsync();

        var result = await repository.GetAsync(testingClass.Id);

        Assert.NotNull(result);

        await repository.RemoveAsync(testingClass.Id);

        await repository.SaveChangesAsync();

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await repository.GetAsync(testingClass.Id));

        Assert.Contains("Não foi encontrada uma entidade com o id", exception.Message);
    }
}
