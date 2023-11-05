namespace Infrastructure.Data;
public class TalabeyahTicktingContext : DbContext
{
    public DbSet<Ticket> Tickets { get; set; }
    public TalabeyahTicktingContext(DbContextOptions<TalabeyahTicktingContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
