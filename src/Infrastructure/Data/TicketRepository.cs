namespace Infrastructure.Data;
public class TicketRepository : ITicketRepository
{
    private readonly TalabeyahTicktingContext _talabeyahTicktingContext;
    public TicketRepository(TalabeyahTicktingContext talabeyahTicktingContext)
    {
        _talabeyahTicktingContext = Guard.Against.Null(talabeyahTicktingContext, nameof(talabeyahTicktingContext));
    }

    public async Task CreateTicket(Ticket ticket)
    {
        await _talabeyahTicktingContext
            .Tickets
            .AddAsync(ticket);
    }

    public async Task<Ticket> GetTicket(int id)
    {
        return await _talabeyahTicktingContext
            .Tickets
            .FindAsync(id) ?? Ticket.CreateNullTicket;
    }

    public async Task<IReadOnlyList<Ticket>> GetTickets(TicketsPaginationParams ticketsPaginationParams)
    {
        var skip = (ticketsPaginationParams.PageIndex - 1) * ticketsPaginationParams.PageSize;
       return await _talabeyahTicktingContext
            .Tickets
            .AsQueryable()
            .AsNoTracking()
            .Skip(skip)
            .Take(ticketsPaginationParams.PageSize)
            .ToListAsync()??
             Enumerable.Empty<Ticket>()
            .ToList();
    }
    public async Task Complete()
    {
        await _talabeyahTicktingContext.SaveChangesAsync();
    }

    public async Task<int> GetTotalCount()
    {
        return await _talabeyahTicktingContext.Tickets.CountAsync();
    }
}
