using System.Collections.Generic;
using Core.Helpers;

namespace Core.Interfaces;
public interface ITicketRepository
{
    Task<Ticket> GetTicket(int id);
    Task Complete();
    Task<IReadOnlyList<Ticket>> GetTickets(TicketsPaginationParams ticketsPaginationParams);
    Task<int> GetTotalCount();
    Task CreateTicket(Ticket ticket);

}
