using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Event
    {
        [Key]
        public int eventId { get; set; }
        [Required]
        public string eventName { get; set; }
        [Required]
        public string eventDescription { get; set; }
        [Required]
        public string eventDate { get; set; }
        [Required]
        public string streetLocation { get; set; }
        [NotMapped]
        public List<Person> participants { get; set; }
    }
}
