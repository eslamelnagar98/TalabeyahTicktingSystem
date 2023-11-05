using System.Collections.Generic;
using Core.Helpers;

namespace Core.Interfaces;
public interface ITicketRepository
{
    Task<Ticket> GetTicket(int id);
    Task Complete();
    Task<IReadOnlyList<Ticket>> GetTickets(TicketsPaginationParams ticketsPaginationParams);
    //Task<Ticket> GetTicketAsync(ISpecification<Ticket> specification);
    //Task<IReadOnlyList<Ticket>> ListAsync(ISpecification<Ticket> specification);
    //Task<int> CountAsync(ISpecification<Ticket> specification);
    Task CreateTicket(Ticket ticket);
    //Task UpdateAsync(Expression<Func<Ticket, bool>> predicate, Expression<Func<SetPropertyCalls<Ticket>, SetPropertyCalls<Ticket>>> setPropertyCalls);
    //Task DeleteAsync(Expression<Func<Ticket, bool>> predicate);
}
