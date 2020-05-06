using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CalendarApplication.UI.ViewModel
{
    /// <summary> Klasa bazowa dla <strong> modelu widoku </strong>, dziedzicząca po interfejsie <see cref="INotifyPropertyChanged"/>, obsługującym zmianę wartości właściwości.</summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary> Zdarzenie występujące kiedy pojawia się zmiana wartości właściwości. </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> Implementacja interfejsu obsługującego zdarzenie zmiany wartości przekazywanej właściwości. Metoda potrzebna do automatycznego odświeżenia widoku, po zmianach które nastąpiły w konkretnej właściwości.</summary>
        /// <param name="propertyName"> Badana właściwość. </param>
        /// <value> Nazwa właściwości, dla której jest wywoływana metoda, została przekazywana przy pomocy atrybutu 'CallerMemberName'. </value>
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
