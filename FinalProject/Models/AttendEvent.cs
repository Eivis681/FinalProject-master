using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class AttendEvent
    {
        [Required]
        public int eventId { get; set; }
        [Required]
        public int personId { get; set; }

        // relationships
        public Event Event { get; set; }
        public Person Person { get; set; }
    }
}
