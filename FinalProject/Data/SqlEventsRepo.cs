using FinalProject.Models;
using FinalProject.Repository;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    public class SqlEventsRepo : EventRepoInterface
    {
        private readonly DatabaseContext _context;
        public SqlEventsRepo(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _context.Events.ToList();
        }

        public Event GetEventById(int id)
        {
            return _context.Events.FirstOrDefault(p => p.eventId == id);
        }

        public void CreateEvent(Event eve)
        {
            if(eve == null)
            {
                throw new ArgumentNullException(nameof(eve));
            }
            _context.Events.Add(eve);
        }

        public void UpdateEvent(int id, Event newEvent)
        {
            var entity = _context.Events.FirstOrDefault(x => x.eventId == id);

            if (entity != null)
            {
               
                // Make changes on entity
                entity.eventName = newEvent.eventName;
                entity.eventDescription = newEvent.eventDescription;
                entity.eventDate = newEvent.eventDate;
                entity.streetLocation = newEvent.streetLocation;

                /* If the entry is being tracked, then invoking update API is not needed. 
                  The API only needs to be invoked if the entry was not tracked. 
                  https://www.learnentityframeworkcore.com/dbcontext/modifying-data */
                _context.Events.Update(entity);

                // Save changes in database
                _context.SaveChanges();
            }
        }

        public void DeleteEvent(Event eve)
        {
            if (eve == null)
            {
                throw new ArgumentNullException(nameof(eve));
            }
            _context.Events.Remove(eve);
        }


        // ATTEND EVENTS
        public Event GetEventAttendants(int ToFindEventId)
        {
            var foundEvent = _context.Events.FirstOrDefault(x => x.eventId == ToFindEventId);
            foundEvent.participants = new List<Person>();
            var checkAttendants = _context.AttendEvent.ToList();

            foreach (var person in checkAttendants)
            {
                if (person.eventId == ToFindEventId)
                {
                    var personFound = _context.Persons.FirstOrDefault(p => p.personId == person.personId);
                    foundEvent.participants.Add(personFound);
                }
            }
            return foundEvent;
        }

        public void AddPersonToEvent(int eventoId, int participantId)
        {
            var entity = new AttendEvent();
            entity.personId = participantId;
            entity.eventId = eventoId;

            _context.AttendEvent.Add(entity);

            _context.SaveChanges();
        }

        public void RemovePersonFromEvent(int eventoId, int participantId)
        {
            var checkAttendants = _context.AttendEvent.ToList();
            foreach (var person in checkAttendants)
            {
                if (person.eventId == eventoId && person.personId == participantId)
                {
                    _context.AttendEvent.Remove(person);
                }
            }
            _context.SaveChanges();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
