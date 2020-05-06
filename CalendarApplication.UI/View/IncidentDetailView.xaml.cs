using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CalendarApplication.UI.View
{
    /// <summary>Inicjalizacja interakcji dla klasy IncidentDetailView.xaml. Dziedziczy po <see cref="UserControl"/>. Zawarta jest w niej obsługa łączenia się z zewnętrznym API.</summary>
    public partial class IncidentDetailView : UserControl
    {
        /// <summary>Konstruktor klasy <see cref="IncidentDetailView" />. Inicjalizuje widok oraz wczytyje miasta do listy.</summary>
        public IncidentDetailView()
        {
            InitializeComponent();
            using (StreamReader r = new StreamReader("../../../city.list.json"))
            {
                string json = r.ReadToEnd();
                cityList = JsonConvert.DeserializeObject<List<City>>(json);
            }
        }

        /// <summary>Klasa zawierająca pola z danymi wczytanych miast z pliku <a href="city.list.json" originalTag="see">city.list.json</a>.</summary>
        public class City
        {
            /// <summary>Deklaracja właściwości identyfikatora miasta.</summary>
            /// <value>Identyfikator miasta.</value>
            public int Id { get; set; }

            /// <summary>Deklaracja właściwości nazwy miasta.</summary>
            /// <value>Nazwa miasta.</value>
            public string Name { get; set; }
        }

        /// <summary>Klucz API do strony <a href="https://openweathermap.org/">OpenWeatherMap</a>.</summary>
        const string APP_ID = "e369989e9b2808f8cb6fd9d08d9c76e6";

        /// <summary>  Lista miast</summary>
        List<City> cityList;

        /// <summary>Metoda sprawdzająca, czy wpisany ciąg znaków jest nazwą miasta, które znajduje się w pliku <a href="city.list.json" originalTag="see">city.list.json</a>.</summary>
        /// <returns>Indeks szukanego miasta</returns>
        private int isCityValid()
        {
            return cityList.FindIndex(x =>
            x.Name.ToLower().Equals(txtCityName.Text.ToLower()));
        }

        /// <summary>Metoda obsługująca zdarzenie wciśnięcia klawisza dla kontrolki <b>txtCityName</b>.</summary>
        /// <param name="sender">Referencja do kontrolki obiektu, która wywołała metodę.</param>
        /// <param name="e">Instancja klasy <see cref="KeyEventArgs" /> zawierająca dane dotyczące zdarzenia.</param>
        private async void txtCityName_KeyDown(object sender, KeyEventArgs e)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                try
                {
                    int index = isCityValid();
                    if (index >= 0)
                    {
                        string query = String.Format("http://api.openweathermap.org/data/2.5/weather?id={0}&appid={1}&units=metric&lang=pl", cityList[index].Id, APP_ID);
                        JObject response = JObject.Parse(new System.Net.WebClient().DownloadString(query));
                        if (response.SelectToken("cod").ToString().Equals("200"))
                        {
                            String iconString;
                            iconString = response.SelectToken("weather[0].icon").ToString();
                            lblCityAndCountry.Content = response.SelectToken("name").ToString() + ", " + response.SelectToken("sys.country").ToString();
                            lblWeather.Content = response.SelectToken("main.temp").ToString() + "c, " + response.SelectToken("weather[0].main").ToString();
                            lblWeatherDescription.Content = response.SelectToken("weather[0].description").ToString();

                            var image = new Image();
                            var fullFilePath = String.Format("http://openweathermap.org/img/wn/{0}@2x.png", iconString);

                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                            bitmap.EndInit();

                            icon.Source = bitmap;
                        }
                        else if (response.SelectToken("cod").ToString().Equals("429"))
                        {
                            await metroWindow.ShowMessageAsync("Ups", "Konto jest czasowo zablokowane ze względu na przekroczenie limitu żądań.\nSpróbuj ponownie trochę później.");
                        }
                    }
                    else
                    {
                        await metroWindow.ShowMessageAsync("Ups", "Wpisałeś nieistniejące miasto. Nie odkryjesz Ameryki na nowo!");
                    }
                }
                catch (Exception ex)
                {
                    await metroWindow.ShowMessageAsync("Błędzik", ex.Message);
                }
            }
        }
    }
}
