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
    /// <summary>
    /// Logika interakcji dla klasy IncidentDetailView.xaml
    /// </summary>
    public partial class IncidentDetailView : UserControl
    {
        public IncidentDetailView()
        {
            InitializeComponent();
            using (StreamReader r = new StreamReader("../../../city.list.json"))
            {
                string json = r.ReadToEnd();
                cityList = JsonConvert.DeserializeObject<List<City>>(json);
            }
        }
        public class City
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        const string APP_ID = "e369989e9b2808f8cb6fd9d08d9c76e6";
        List<City> cityList;
        private int isCityValid()
        {
            return cityList.FindIndex(x =>
            x.Name.ToLower().Equals(txtCityName.Text.ToLower()));
        }
        private void txtCityName_KeyDown(object sender, KeyEventArgs e)
        {
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
                            // displayWeatherImage(Convert.ToInt32(response.SelectToken("weather[0].id").ToString()));
                            //displayWeatherImage = (response.SelectToken("weather[0].icon").ToString());
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
                            //wrapPanel1.Children.Add(image);
                        }
                        else if (response.SelectToken("cod").ToString().Equals("429"))
                        {
                            MessageBox.Show("The account is temporary blocked due to exceeding the requests limitition.\nPlease try agian later.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter a valid city name", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }
    }
}
