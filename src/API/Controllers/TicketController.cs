namespace API.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ILogger<TicketController> _logger;
    private readonly TicketAddressDto _ticketAddressDto;

    public TicketController(ITicketRepository ticketRepository, ILogger<TicketController> logger, TicketAddressDto ticketAddressDto)
    {
        _ticketRepository = Guard.Against.Null(ticketRepository, nameof(ticketRepository));
        _logger = Guard.Against.Null(logger, nameof(logger));
        _ticketAddressDto = ticketAddressDto;
    }

    [HttpGet("Tickets", Name = "GetTickets")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Pagination), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Pagination>> GetTickets([FromQuery] TicketsPaginationParams ticketsPaginationParams)
    {
        var totalCount = await _ticketRepository.GetTotalCount();
        var tickets = await _ticketRepository.GetTickets(ticketsPaginationParams);
        var ticketsToReturn = tickets.ToTicketsDtoList();
        var paginationToReturn = GeneratePagination(ticketsPaginationParams, ticketsToReturn, totalCount);
        return Ok(paginationToReturn);
    }


    [HttpGet("{id:int}", Name = "GetTicket")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(TicketDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<TicketDto>> GetTicket(int id)
    {
        var ticket = await _ticketRepository.GetTicket(id);
        if (ticket.IsNullTicket)
        {
            _logger.LogError($"Ticket with id: {id}, not found.");
            return NotFound();
        }
        return Ok((TicketDto)ticket);
    }


    [HttpPost]
    [ProducesResponseType(typeof(TicketDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<TicketDto>> CreateTicket([FromBody] TicketDto ticketDto)
    {
        var ticket = (Ticket)ticketDto;
        await _ticketRepository.CreateTicket(ticket);
        await _ticketRepository.Complete();
        return CreatedAtRoute("GetTicket", new { id = ticket.Id }, ticket);
    }

    [HttpGet("TicketsAddress", Name = "GetTicketAddress")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(TicketAddressDto), (int)HttpStatusCode.OK)]
    public ActionResult<TicketAddressDto> GetTicketAddress()
    {
        return Ok(_ticketAddressDto);
    }

    private Pagination GeneratePagination(TicketsPaginationParams ticketsParams, IReadOnlyList<TicketDto> tickets, int totalCount)
    {
        var ticketsCount = tickets?.Count ?? 0;
        return new Pagination
        {
            Count = totalCount,
            Tickets = tickets!,
            PageIndex = ticketsParams.PageIndex,
            PageSize = ticketsParams.PageSize <= ticketsCount ? ticketsParams.PageSize : ticketsCount
        };
    }
}
