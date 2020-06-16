using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    [Route("api/attendevent")]
    [ApiController]
    [Produces("application/json")]
    public class AttendEventsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AttendEventsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/AttendEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendEvent>>> GetAttendEvent()
        {
            return Ok(await _context.AttendEvent.ToListAsync());
        }

        // GET: api/AttendEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendEvent>> GetAttendEvent(int id)
        {
            var attendEvent = await _context.AttendEvent.FindAsync(id);

            if (attendEvent == null)
            {
                return NotFound();
            }

            return Ok(attendEvent);
        }

        // PUT: api/AttendEvents/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendEvent(int id, AttendEvent attendEvent)
        {
            if (id != attendEvent.eventId)
            {
                return BadRequest();
            }

            _context.Entry(attendEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendEventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AttendEvents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AttendEvent>> PostAttendEvent(AttendEvent attendEvent)
        {
            _context.AttendEvent.Add(attendEvent);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AttendEventExists(attendEvent.eventId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAttendEvent", new { id = attendEvent.eventId }, attendEvent);
        }

        // DELETE: api/AttendEvents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AttendEvent>> DeleteAttendEvent(int id)
        {
            var attendEvent = await _context.AttendEvent.FindAsync(id);
            if (attendEvent == null)
            {
                return NotFound();
            }

            _context.AttendEvent.Remove(attendEvent);
            await _context.SaveChangesAsync();

            return attendEvent;
        }

        private bool AttendEventExists(int id)
        {
            return _context.AttendEvent.Any(e => e.eventId == id);
        }
    }
}
