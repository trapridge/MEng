using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Runtime.Serialization.Json;
using Microsoft.Phone.Shell;
using System.Windows.Navigation;
using System.IO.IsolatedStorage;

namespace NetworkingAndTombstoning
{
    public partial class MainPage : PhoneApplicationPage
    {
        // no MVVM here, just manually store persistent data to isolated storage to allow proper resuming from tombstoned state
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Length < 1)
            {
                MessageBox.Show("Type in a city name!");
            }
            else 
            {
                GetWoeid(textBox1.Text);
            }
            
        }

        private void textBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("clearing");
            textBox1.Text = "";
        }

        // handle resuming from tombstoned state
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string page;
            if (settings.TryGetValue("currentPage", out page))
            {
                if (page.Equals("weatherInfoPage"))
                {
                    System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        PhoneApplicationService.Current.State["city"] = "";
                        PhoneApplicationService.Current.State["weather"] = "";
                        NavigationService.Navigate(new Uri("/WeatherInfoPage.xaml", UriKind.Relative));
                    });
                }
            }
            else
            {
                LayoutRoot.Visibility = Visibility.Visible;
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
        }   

        // get a woeid from yahoo to enable weather query
        private void GetWoeid(string place)
        {
            string uri = "http://query.yahooapis.com/v1/public/yql?format=json&q=select%20woeid%20from%20geo.places(0%2C1)%20where%20text%3D'" + place + "'";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(uri));
            request.BeginGetResponse(new AsyncCallback(WoeidCallback), request);
        }

        private void WoeidCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
            using (StreamReader streamReader1 = new StreamReader(response.GetResponseStream()))
            {
                string resultString = streamReader1.ReadToEnd();

                var stream = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(resultString));
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Woeid.RootObject));
                Woeid.RootObject woeidResponse = (Woeid.RootObject)jsonSerializer.ReadObject(stream);

                // if reslts is null no woeid can be found
                if (woeidResponse.query.results != null)
                {
                    GetWeather(woeidResponse.query.results.place.woeid);
                }
                else 
                {
                    System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => {
                        MessageBox.Show("City not found!");
                    });
                }
                
            }
        }

        // get weather forecast from yahoo
        private void GetWeather(string woeid)
        {
            string uri = "http://query.yahooapis.com/v1/public/yql?format=json&q=select%20item.description%20from%20weather.forecast%20where%20woeid%3D" + woeid +  "%20and%20u%3D'c'";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(uri));
            request.BeginGetResponse(new AsyncCallback(WeatherCallback), request);
        }
         
        private void WeatherCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
            using (StreamReader streamReader1 = new StreamReader(response.GetResponseStream()))
            {
                string resultString = streamReader1.ReadToEnd();

                var stream = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(resultString));
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Weather.RootObject));

                Weather.RootObject myBook = (Weather.RootObject)jsonSerializer.ReadObject(stream);

                System.Diagnostics.Debug.WriteLine(myBook.query.results.channel.item.description);

                string weatherInfo = myBook.query.results.channel.item.description;

                // show weather data on a separate page
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => {
                    PhoneApplicationService.Current.State["city"] = textBox1.Text;
                    PhoneApplicationService.Current.State["weather"] = weatherInfo;
                    NavigationService.Navigate(new Uri("/WeatherInfoPage.xaml", UriKind.Relative));
                });

                
            }
        }

        
        
    }
}