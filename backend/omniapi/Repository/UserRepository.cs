public class UserRepository : Repository<User>, IUserRepository
{
    ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public override void Add(User entity)
    {
        User user = new User
        {
            Id = Guid.NewGuid(),
            Name = entity.Name,
            Adress = entity.Adress,
            BornDate = entity.BornDate,
            Phone = entity.Phone

        };

        base.Add(user);
    }

}
