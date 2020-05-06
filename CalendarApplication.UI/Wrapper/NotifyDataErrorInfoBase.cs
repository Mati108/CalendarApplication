using CalendarApplication.UI.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CalendarApplication.UI.Wrapper
{
    /// <summary>Klasa obługująca informacje o błędach. Dziedziczy po <see cref="ViewModelBase"/> oraz <see cref="INotifyDataErrorInfo"/></summary>
    public class NotifyDataErrorInfoBase : ViewModelBase, INotifyDataErrorInfo
    {
        /// <summary>Słownik przechowującego błędy i nazwy właściwości jako klucz.</summary>
        private Dictionary<string, List<string>> _errorsByPropertyName
        = new Dictionary<string, List<string>>();

        /// <summary>Występuje, gdy błędy sprawdzania poprawności uległy zmianie dla właściwości lub całej jednostki.</summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>Pobiera wartość wskazującą, czy jednostka ma błędy sprawdzania poprawności.</summary>
        public bool HasErrors => _errorsByPropertyName.Any();

        /// <summary>Metoda wywoływana, aby zmienić informacje o błędzie dla danej właściwości.</summary>
        /// <param name="propertyName">Nazwa właściwości, dla której został zgłoszony błąd.</param>
        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            base.OnPropertyChanged(nameof(HasErrors));
        }

        /// <summary>Metoda dodająca błędy do słownika.</summary>
        /// <param name="propertyName">Nazwa właściwości, dla której został zgłoszony błąd. Jest ona kluczem słownika.</param>
        /// <param name="error">Błąd dodawany do listy, przyporządkowanej konretnej właściwości.</param>
        protected void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName[propertyName] = new List<string>();
            }
            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        /// <summary>Metoda czyszcząca błędy dla danej właściwości.</summary>
        /// <param name="propertyName">Nazwa właściwości, dla której został zgłoszony błąd.</param>
        protected void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        /// <summary>Metoda, która pobiera błędy sprawdzania poprawności dla określonej właściwości lub dla całej encji.</summary>
        /// <param name="propertyName">Nazwa właściwości, dla której należy pobrać błędy sprawdzania poprawności.</param>
        /// <returns>Błędy sprawdzania poprawności właściwości lub obiektu.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName)
                ? _errorsByPropertyName[propertyName]
                : null;
        }
    }
}
