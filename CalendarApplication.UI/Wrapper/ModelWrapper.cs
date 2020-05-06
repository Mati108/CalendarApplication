using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CalendarApplication.UI.Wrapper
{
    /// <summary>Bazowa generyczna klasa opakowująca. Służy do sprawdzania sprawdzania poprawności danych i ich ustawiania. Dziedziczy po <see cref="NotifyDataErrorInfoBase"/>.</summary>
    /// <typeparam name="T">Generyczny typ danych.</typeparam>
    public class ModelWrapper<T> : NotifyDataErrorInfoBase
    {
        public T Model { get; }
        public ModelWrapper(T model)
        {
            Model = model;
        }

        /// <summary>Sprawdza poprawność adnotacji danych. Wykryte niepoprawności zapisuje na liście, z której jest później wyłuskiwana wiadomość o błędzie, a następnie dodawana jako błąd do słownika.</summary>
        /// <param name="propertyName">Nazwa sprawdzanej właściwości.</param>
        /// <param name="currentValue">Obecna wartość do zweryfikowania.</param>
        private void ValidateDataAnnotations(string propertyName, object currentValue)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(Model) { MemberName = propertyName };
            Validator.TryValidateProperty(currentValue, context, results);

            foreach (var result in results)
            {
                AddError(propertyName, result.ErrorMessage);
            }
        }

        /// <summary>Metoda sprawdzająca, czy nie występują błędy niestandardowe dla danej właściwości. Jeśli tak, dodaje je do słownika.</summary>
        /// <param name="propertyName">Nazwa sprawdzanej właściwości.</param>
        private void ValidateCustomErrors(string propertyName)
        {
            var errors = ValidateProperty(propertyName);
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    AddError(propertyName, error);
                }
            }
        }

        /// <summary>Metoda pobierająca wartość.</summary>
        /// <typeparam name="TValue">Generyczny typ danych wartości.</typeparam>
        /// <param name="propertyName">Nazwa wlaściwości.</param>
        /// <returns>Pobrana wartość</returns>
        protected virtual TValue GetValue<TValue>([CallerMemberName]string propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);
        }

        /// <summary>Metoda sprawdzająca poprawność badanej właściwości.</summary>
        /// <param name="propertyName">Nazwa badanej właściwości.</param>
        /// <returns>Null</returns>
        protected virtual IEnumerable<string> ValidateProperty(string propertyName)
        {
            return null;
        }

        /// <summary>Metoda ustawiająca wartość.</summary>
        /// <typeparam name="TValue">Generyczny typ danych wartości.</typeparam>
        /// <param name="value">Wartość jaka jest ustawiana.</param>
        /// <param name="propertyName">Nazwa właściwości.</param>
        protected virtual void SetValue<TValue>(TValue value,
            [CallerMemberName]string propertyName = null)
        {
            typeof(T).GetProperty(propertyName).SetValue(Model, value);
            OnPropertyChanged(propertyName);
            ValidatePropertyInternal(propertyName, value);
        }

        /// <summary>Metoda sprawdzająca poprawność danej właściwości.</summary>
        /// <param name="propertyName">Nazwa badanej właściwości.</param>
        /// <param name="currentValue">Obecna wartość.</param>
        private void ValidatePropertyInternal(string propertyName, object currentValue)
        {
            ClearErrors(propertyName);

            ValidateDataAnnotations(propertyName, currentValue);

            ValidateCustomErrors(propertyName);
        }
    }
}
