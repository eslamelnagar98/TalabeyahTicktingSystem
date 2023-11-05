namespace API.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ILogger<TicketController> _logger;
    public TicketController(ITicketRepository ticketRepository, ILogger<TicketController> logger)
    {
        _ticketRepository = Guard.Against.Null(ticketRepository, nameof(ticketRepository));
        _logger = Guard.Against.Null(logger, nameof(logger));
    }

    [HttpGet("Tickets",Name = "GetTickets")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Pagination), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Pagination>> GetTickets([FromQuery] TicketsPaginationParams ticketsPaginationParams)
    {
        var tickets = await _ticketRepository.GetTickets(ticketsPaginationParams);
        var ticketsToReturn = tickets.ToTicketsDtoList();
        var paginationToReturn = GeneratePagination(ticketsPaginationParams, ticketsToReturn);
        return Ok(paginationToReturn);
    }


    [HttpGet("{id:int}", Name = "GetTicket")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Ticket), (int)HttpStatusCode.OK)]
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
        return CreatedAtRoute("GetTicket", new { id = ticketDto.Id }, ticketDto);
    }

    [HttpGet("TicketsAddress",Name = "GetTicketAddress")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(TicketAddressDto), (int)HttpStatusCode.OK)]
    public ActionResult<TicketAddressDto> GetTicketAddress()
    {
        return Ok(new TicketAddressDto());
    }

    private Pagination GeneratePagination(TicketsPaginationParams ticketsParams, IReadOnlyList<TicketDto> tickets)
    {
        var ticketsCount = tickets?.Count ?? 0;
        return new Pagination
        {
            Count = ticketsCount,
            Tickets = tickets!,
            PageIndex = ticketsParams.PageIndex,
            PageSize = ticketsParams.PageSize <= ticketsCount ? ticketsParams.PageSize : ticketsCount
        };
    }
}
