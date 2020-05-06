using System.Threading.Tasks;

namespace CalendarApplication.UI.ViewModel
{
    /// <summary>Interfejs do obsługi modelu widoku wydarzenia.</summary>
    public interface IIncidentDetailViewModel
    {
        /// <summary>Metoda asynchronicznie ładująca dane o wydarzeniu. Jeśli dane wydarzenie nie istnieje, to zostaje utworzone. Jeśli w wydarzeniu zaszły zmiany to są one obsługiwane.</summary>
        /// <param name="incidentId">Identyfikator wydarzenia, który może być NULL.</param>
        Task LoadAsync(int? incidentId);

        /// <summary>Deklaracja instancji informującej, czy zaszły w niej zmiany.</summary>
        /// <value>
        ///   <c>true</c> jeśli zaszły zmiany. W przeciwnym przypadku <c>false</c>.
        /// </value>
        bool HasChanges { get; }
    }
}