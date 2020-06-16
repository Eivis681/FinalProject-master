using FinalProject.Controllers;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Repository
{
    public interface EventRepoInterface
    {
        bool SaveChanges();
        IEnumerable<Event> GetAllEvents();
        Event GetEventById(int id);
        void CreateEvent(Event newEvent);
        void UpdateEvent(int id, Event newEvent);
        void DeleteEvent(Event newEvent);

        // attendEvent
        Event GetEventAttendants(int id);
        void AddPersonToEvent(int id, int id2);
        void RemovePersonFromEvent(int id, int id2);

    }
}
