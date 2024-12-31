public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
{
    ApplicationDbContext _context;
    public InvoiceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }
}
