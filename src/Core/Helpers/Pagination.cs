namespace Core.Helpers;
public record class Pagination
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public IReadOnlyList<TicketDto> Tickets { get; set; } = Enumerable
        .Empty<TicketDto>()
        .ToList();
}
