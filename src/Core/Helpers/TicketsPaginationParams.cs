namespace Core.Helpers;
public record TicketsPaginationParams
{
    private const int MaxPageSize = 5;
    public int PageIndex { get; set; } = 1;
    private int _pageSize = MaxPageSize;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}
