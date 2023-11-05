namespace API.Configurations;
public partial class Extension
{
    public static List<TicketDto> ToTicketsDtoList(this IReadOnlyCollection<Ticket> tickets)
    {
        return tickets
            .Select(ticket => (TicketDto)ticket)
            .ToList();
    }

}
