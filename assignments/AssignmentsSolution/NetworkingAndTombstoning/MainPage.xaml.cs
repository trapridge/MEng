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

        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            GetWoeid(textBox1.Text);
        }

        private void textBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("clearing");
            textBox1.Text = "";
        }


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
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(RootObject));
                RootObject myBook = (RootObject)jsonSerializer.ReadObject(stream);

                // if reslts is null there are none

                

                System.Diagnostics.Debug.WriteLine(myBook.query.results.place.woeid);

                GetWeather(myBook.query.results.place.woeid);
            }
        }

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
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(RootObject2));

                RootObject2 myBook = (RootObject2)jsonSerializer.ReadObject(stream);

                System.Diagnostics.Debug.WriteLine(myBook.query.results.channel.item.description);

                string weatherInfo = myBook.query.results.channel.item.description;


                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => {
                    PhoneApplicationService.Current.State["city"] = textBox1.Text;
                    PhoneApplicationService.Current.State["weather"] = weatherInfo;
                    NavigationService.Navigate(new Uri("/WeatherInfoPage.xaml", UriKind.Relative));
                });

                
            }
        }

        
        
    }
}

public class Place
{
    public string woeid { get; set; }
}

public class Results
{
    public Place place { get; set; }
}

public class Query
{
    public int count { get; set; }
    public string created { get; set; }
    public string lang { get; set; }
    public Results results { get; set; }
}

public class RootObject
{
    public Query query { get; set; }
}

public class Item2
{
    public string description { get; set; }
}

public class Channel2
{
    public Item2 item { get; set; }
}

public class Results2
{
    public Channel2 channel { get; set; }
}

public class Query2
{
    public int count { get; set; }
    public string created { get; set; }
    public string lang { get; set; }
    public Results2 results { get; set; }
}

public class RootObject2
{
    public Query2 query { get; set; }
}