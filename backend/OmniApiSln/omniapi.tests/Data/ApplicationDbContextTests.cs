using Microsoft.EntityFrameworkCore;

public class ApplicationDbContextTests : ApplicationDbContext{
    public ApplicationDbContextTests(DbContextOptions<ApplicationDbContext> options) : base(options){}
    DbSet<TestingClass> TestingClass {get; set;}

}
