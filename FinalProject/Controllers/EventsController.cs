using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Controllers
{
    [Route("api/events")]
    [ApiController]
    [Produces("application/json")]
    public class EventsController : ControllerBase
    {
        private readonly EventRepoInterface _repository;
        
        public EventsController(EventRepoInterface repository)
        {
            _repository = repository;
        }

        // GET: api/events 
        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetAllEvents()
        {
            var allEvents = _repository.GetAllEvents();
            return Ok(allEvents);
        }

        // GET api/events/{id}
        [HttpGet("{id}", Name = "GetEventById")]
        public ActionResult<Event> GetEventById(int id)
        {
            var eventFound = _repository.GetEventById(id);
            if(eventFound != null)
            {
                return Ok(eventFound);
            }
            return NotFound();
        }

        // POST api/events/
        [HttpPost] // GALIMA neduot eventId (DB pati susigeneruos), bet bus 500 error nes nesusigaudys createdAtRoute.
        public ActionResult<Event> CreateCommand(Event newEvent)
        {
            _repository.CreateEvent(newEvent);
            _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetEventById), new { id = newEvent.eventId }, newEvent);
        }

        // PUT api/events/{id}
        [HttpPut("{id}")] // Nereikia eventId Json, susiranda pagal http put id.
        public ActionResult UpdateEvent(int id, Event updatedEvent)
        {
            var existingEvent = _repository.GetEventById(id);
            if (existingEvent == null)
            {
                return NotFound();
            }

            _repository.UpdateEvent(id, updatedEvent);

            return NoContent();
        }

        // DELETE api/events/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteEvent(int id)
        {
            var findEvent = _repository.GetEventById(id);
            if (findEvent == null)
            {
                return NotFound();
            }
            _repository.DeleteEvent(findEvent);
            _repository.SaveChanges();

            return NoContent();
        }

        // ATTEND EVENTS
        // GET api/events/{id}
        [HttpGet("attend/{id}")]
        public ActionResult<Event> GetEventAttendants(int id)
        {
            var eventFound = _repository.GetEventAttendants(id);
            if (eventFound != null)
            {
                return Ok(eventFound);
            }
            return NotFound();
        }

        // PUT api/events/{id}
        [HttpPut("attend/{eventId}/{personId}")]
        public ActionResult AddParticipant(int eventId, int personId)
        {
            var existingEvent = _repository.GetEventById(eventId);
            if (existingEvent == null)
            {
                return NotFound(" Event Not Found ");
            }

            _repository.AddPersonToEvent(eventId, personId);

            return NoContent();
        }
        [HttpDelete("attend/{eventId}/{personId}")]
        public ActionResult DeletePaticipant(int eventId, int personId)
        {
            var existingEvent = _repository.GetEventById(eventId);
            if (existingEvent == null)
            {
                return NotFound(" Event Not Found ");
            }

            _repository.RemovePersonFromEvent(eventId, personId);

            return NoContent();
        }
    }
}
