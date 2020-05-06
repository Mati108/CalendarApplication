using CalendarApplication.Model;
using System;
using System.Collections.Generic;

namespace CalendarApplication.UI.Wrapper
{
    /// <summary>Klasa opakowująca wydarzenia. Dziedziczy po <see cref="ModelWrapper{T}"/>.</summary>
    public class IncidentWrapper : ModelWrapper<Incident>
    {
        /// <summary>Konstruktor nowej instancji klasy <see cref="IncidentWrapper" />.</summary>
        /// <param name="model">Inicjalizowany model.</param>
        public IncidentWrapper(Incident model) : base(model)
        {
        }

        /// <summary>Metoda sprawdzająca poprawność badanej właściwości.</summary>
        /// <param name="propertyName">Nazwa badanej właściwości.</param>
        /// <returns>Napis ostrzegający.</returns>
        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Title):
                    if (string.Equals(Title, "Nudy", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Nudy się nie mogą nigdy wydarzyć!";
                    }
                    break;
            }
        }

        /// <summary>Deklaracja identyfikatora wydarzenia.</summary>
        /// <value>Identyfikator wydarzenia.</value>
        public int Id { get { return Model.Id; } }

        /// <summary>Deklaracja nazwy wydarzenia.</summary>
        /// <value>Nazwa wydarzenia.</value>
        public string Title
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>Deklaracja daty początkowej wydarzenia. Zapobiega ustawieniu wcześniejszej daty zakończenia, niż rozpoczęcia</summary>
        /// <value>Data początkowa wydarzenia.</value>
        public DateTime DateStart
        {
            get { return GetValue<DateTime>(); }
            set
            {
                SetValue(value);
                if (DateStop < DateStart)
                {
                    DateStop = DateStart;
                }
            }
        }

        /// <summary>Deklaracja daty końcowej wydarzenia. Zapobiega ustawieniu wcześniejszej daty zakończenia, niż rozpoczęcia.</summary>
        /// <value>Data końcowa wydarzenia.</value>
        public DateTime DateStop
        {
            get { return GetValue<DateTime>(); }
            set
            {
                SetValue(value);
                if (DateStop < DateStart)
                {
                    DateStart = DateStop;
                }
            }
        }
    }
}
