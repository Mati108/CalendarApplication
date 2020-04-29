using System;
using System.ComponentModel.DataAnnotations;

namespace CalendarApplication.Model
{
    public class Incident
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        [Required]
        public DateTime DateStop { get; set; }
    }
}
