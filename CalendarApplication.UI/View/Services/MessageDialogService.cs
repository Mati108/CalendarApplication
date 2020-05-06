using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace CalendarApplication.UI.View.Services
{
    /// <summary>Wyliczenie obsługujące wyświetlany tekst na przyciskach, w oknie dialogowym metody <a href="ShowOkCancelDialogAsync" target="_blank">ShowOkCancelDialogAsync</a>.</summary>
    public enum MessageDialogResult
    {
        OK,
        Anuluj
    }

    /// <summary> Klasa obsługująca komunikację z użytkownikiem za pomocą okienek dialogowych, implementująca interfejs <see cref="IMessageDialogService"/>.</summary>
    public class MessageDialogService : IMessageDialogService
    {
        /// <summary>Pobiera instancję klasy <see cref="MetroWindow"/> dla głównego okna aplikacji, w celu późniejszego korzystania z bliblioteki <b>MahApps</b>, odpowiadającej za styl aplikacji.</summary>
        /// <value>The metro window.</value>
        private MetroWindow MetroWindow => (MetroWindow)App.Current.MainWindow;

        /// <summary>Metoda asynchroniczna pokazująca okno dialogowe z dwoma przyciskami.</summary>
        /// <param name="text">Informacja wyswietlana użytkownikowi w okienku.</param>
        /// <param name="title">Nagłówek wyświetlanego okienka.</param>
        /// <returns>Rezultat wciśnięcia jednego z przycisków.</returns>
        public async Task<MessageDialogResult> ShowOkCancelDialogAsync(string text, string title)
        {
            var result = await MetroWindow.ShowMessageAsync(title, text, MessageDialogStyle.AffirmativeAndNegative);

            return result == MahApps.Metro.Controls.Dialogs.MessageDialogResult.Affirmative
                ? MessageDialogResult.OK
                : MessageDialogResult.Anuluj;
        }
    }
}
