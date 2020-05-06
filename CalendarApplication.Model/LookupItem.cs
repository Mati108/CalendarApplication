using System;

namespace CalendarApplication.Model
{
    /// <summary>Klasa zawierająca pola opisujące wyświetlany element</summary>
    public class LookupItem
    {
        /// <summary>Deklaracja właściwości identyfikatora wydarzenia.</summary>
        /// <value>Identyfikator wyświetlanego wydarzenia.</value>
        public int Id { get; set; }

        /// <summary>Deklaracja właściwości nazwy wyświetlanego wydarzenia.</summary>
        /// <value>Nazwa wyświetlanego wydarzenia.</value>
        public string DisplayIncident { get; set; }

        /// <summary>Deklaracja właściwości daty wyświetlanego wydarzenia.</summary>
        /// <value>Data wyświetlanego wydarzenia.</value>
        public DateTime DisplayDate { get; set; }
    }
}

