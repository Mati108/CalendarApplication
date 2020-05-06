using System.Threading.Tasks;

namespace CalendarApplication.UI.View.Services
{
    /// <summary>Interfejs do komunikacji z użytkownikiem za pomocą okienek dialogowych.</summary>
    public interface IMessageDialogService
    {
        /// <summary>Metoda asynchroniczna pokazująca okno dialogowe z dwoma przyciskami.</summary>
        /// <param name="text">Informacja wyswietlana użytkownikowi w okienku.</param>
        /// <param name="title">Nagłówek wyświetlanego okienka.</param>
        /// <returns>Rezultat wciśnięcia jednego z przycisków.</returns>
        Task<MessageDialogResult> ShowOkCancelDialogAsync(string text, string title);
    }
}