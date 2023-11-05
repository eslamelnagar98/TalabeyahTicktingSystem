using Core.Helpers;

namespace Core.Entities;
public class Ticket
{
    public int Id { get; init; }
    public DateTime CreationDate { get; private set; } = DateTime.Now;
    public required string PhoneNumber { get; init; }
    public required string Governorate { get; init; } = string.Empty;
    public required string City { get; init; } = string.Empty;
    public required string District { get; init; } = string.Empty;
    public bool IsHandled { get; set; } = false;
    public bool IsNullTicket { get; private set; } = false;
    public static Ticket CreateNullTicket =>
        new Ticket
        {
            Id = -1,
            PhoneNumber = string.Empty,
            IsHandled = default,
            IsNullTicket = true,
            Governorate = string.Empty,
            City = string.Empty,
            District = string.Empty,
        };

    public static explicit operator Ticket(TicketDto ticket)
    {
        return new()
        {
            Id = ticket.Id,
            CreationDate = ticket.CreationDate,
            PhoneNumber = ticket.PhoneNumber,
            Governorate = ticket.Governorate,
            City = ticket.City,
            District = ticket.District,
            IsHandled = ticket.IsHandled,
        };
    }
}
