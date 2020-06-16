using FinalProject.Models;
using FinalProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    public class SqlAttendEventsRepo
    {
        private readonly DatabaseContext _context;
        public SqlAttendEventsRepo(DatabaseContext context)
        {
            _context = context;
        }

        // ALL EVENTS
        /*        public IEnumerable<AttendEvent> GetAllEvents()
                {
                    return _context.Events.ToList();
                }*/

        public Event GetEventAttendants(int ToFindEventId)
        {
            var foundEvent = _context.Events.FirstOrDefault(x => x.eventId == ToFindEventId);
            var checkAttendants = _context.AttendEvent.ToList();

            foreach (var person in checkAttendants)
            {
                if(person.eventId == ToFindEventId)
                {
                    foundEvent.participants.Add(_context.Persons.FirstOrDefault(p => p.personId == person.personId));
                }
            }
            return foundEvent;
        }

    }
}
