using System;
using System.Collections.Generic;
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
using Microsoft.Phone.Shell;
using System.Windows.Navigation;
using System.IO.IsolatedStorage;

namespace NetworkingAndTombstoning
{
    public partial class Page1 : PhoneApplicationPage
    {
        string weatherInfo;
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        public Page1()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            backPressed = false;

            if (((string)PhoneApplicationService.Current.State["city"]).Equals("") && ((string)PhoneApplicationService.Current.State["weather"]).Equals(""))
            {
                string city;
                settings.TryGetValue("weatherLocation", out city);
                PageTitle.Text = city;

                string weather;
                settings.TryGetValue("weatherInfo", out weather);
                weatherInfo = weather;
            }
            else
            {
                PageTitle.Text = (string)PhoneApplicationService.Current.State["city"];
                weatherInfo = (string)PhoneApplicationService.Current.State["weather"];
            }

            webBrowser1.NavigateToString("<head><style>* {  color: white; background-color: black; }</style></head><body>" + weatherInfo + "</body>");
            webBrowser1.Visibility = Visibility.Visible;
        }

        bool backPressed = false; 

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            backPressed = true;
            base.OnBackKeyPress(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            if(settings.Contains("currentPage"))
            {
                settings.Remove("currentPage");
            }

            if(!backPressed) settings.Add("currentPage", "weatherInfoPage");

            if (settings.Contains("weatherInfo"))
            {
                settings.Remove("weatherInfo");
            }

            settings.Add("weatherInfo", weatherInfo);


            if (settings.Contains("weatherLocation"))
            {
                settings.Remove("weatherLocation");
            }


            settings.Add("weatherLocation", PageTitle.Text);

            settings.Save();
        }   
     
    }
}