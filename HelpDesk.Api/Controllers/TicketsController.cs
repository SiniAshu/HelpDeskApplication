using HelpDesk.Domain;
using HelpDesk.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HeplDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService TicketService;

        public TicketController(ITicketService ticketService)
        {
            TicketService = ticketService;
        }

        // POST: api/ticket
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ticket ticket)
        {
            var id = await TicketService.Create(ticket);

            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        // GET: api/ticket/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await TicketService.GetById(id);

            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        // GET: api/ticket
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await TicketService.GetAll();

            return Ok(tickets);
        }

        // PUT: api/ticket/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.Id)
                return BadRequest("Id mismatch.");

            await TicketService.Update(ticket);

            return NoContent();
        }
    }
}
