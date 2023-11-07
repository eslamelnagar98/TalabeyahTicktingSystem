using System.Text.Json.Serialization;
namespace Core.Helpers;
public class TicketDto
{
    public int Id { get; init; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public required string PhoneNumber { get; init; } = string.Empty;
    public required string Governorate { get; init; } = string.Empty;
    public required string City { get; init; } = string.Empty;
    public required string District { get; init; } = string.Empty;
    public bool IsHandled { get; set; }=false;

    public static explicit operator TicketDto(Ticket ticket)
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
