using System;
using System.ComponentModel.DataAnnotations;

namespace CalendarApplication.Model
{
    /// <summary>Klasa zawierająca pola opisujące wydarzenie</summary>
    public class Incident
    {
        /// <summary>Deklaracja właściwości identyfikatora wydarzenia.</summary>
        /// <value>Identyfikator wydarzenia.</value>
        public int Id { get; set; }

        /// <summary>Deklaracja właściwości nazwy wydarzenia.</summary>
        /// <value>Nazwa wydarzenia.</value>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>Deklaracja właściwości początkowej daty wydarzenia.</summary>
        /// <value>Początkowa data wydarzenia.</value>
        [Required]
        public DateTime DateStart { get; set; }

        /// <summary>Deklaracja właściwości końcowej daty wydarzenia.</summary>
        /// <value>Końcowa data wydarzenia.</value>
        [Required]
        public DateTime DateStop { get; set; }
    }
}
