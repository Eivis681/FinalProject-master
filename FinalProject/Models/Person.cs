using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Person
    {
        [Key]
        public int personId { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public long phoneNumber { get; set; }
        [NotMapped]
        public  List<Event> ParticipatingInEvents { get; set; }

    }
}
