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
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using Microsoft.Phone.Shell;
using System.Text.RegularExpressions;
using System.IO.IsolatedStorage;

namespace ProjectWork
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string access_token;
        private string sort_mode = "title";
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            SystemTray.SetProgressIndicator(this, progress);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            string at = "";
            if (settings.TryGetValue("access_token", out at))
            {
                Debug.WriteLine("Found access_token from storage");
                access_token = at;
            }

            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            // login to producteev here
            my_popup_xaml.IsOpen = true;
            // connect
            connectToProducteev();
        }

        private void connectToProducteev()
        {
            //string auth_url = "https://www.producteev.com/oauth/v2/auth?client_id=526c1ce17374607236000000_1do9mo8ue98kgcssgo8ksg84o88wkgos40ks0c44480o0w448w&response_type=token&redirect_uri=http%3A%2F%2Fwww.google.com";
            string auth_url = "http://goo.gl/b3WUJI";

            webBrowser1.Visibility = Visibility.Visible;

            Debug.WriteLine("Connecting to login...");

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                Debug.WriteLine("Connecting to login2...");
                webBrowser1.Navigate(new Uri(auth_url));
            });

        }
        
        Boolean done = false;
        private void webBrowser1_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            /*    
                if (!done) 
                {
                    string html = webBrowser1.SaveToString();
                
                    string hackstring = "<meta name=\"viewport\" content=\"width=208,height=377, user-scalable=no\" />";
                    html = html.Insert(html.IndexOf("<head>", 0) + 6, hackstring);
                    //string hackstring2 = "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=9\">";
                    //html = html.Insert(html.IndexOf("<head>", 0) + 6, hackstring2);

                    //string hackstring3 = "<script src=\"http://css3-mediaqueries-js.googlecode.com/svn/trunk/css3-mediaqueries.js\"></script><script src=\"http://html5shim.googlecode.com/svn/trunk/html5.js\"></script>";
                    //html = html.Insert(html.IndexOf("<head>", 0) + 6, hackstring3);

                    webBrowser1.NavigateToString(html);
                    done = true;
                }   
             */
        }
        

        private void btn_continue_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            my_popup_xaml.IsOpen = false;
        }




        private void webBrowser1_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            
            if (strCmp(e.Uri.AbsoluteUri, "https://www.google.") == true)
            {
                Debug.WriteLine(e.Uri.AbsoluteUri);

                foreach (KeyValuePair<string, string> keys in Helpers.ParseQueryString(e.Uri)) { 
                    if(keys.Key == "access_token") {
                        access_token = keys.Value;

                        // If the key exists
                        if (settings.Contains("access_token"))
                        {
                            // Store the new value
                            settings["access_token"] = access_token;
                        }
                        else
                        {
                            settings["access_token"] = access_token;
                            
                        }
                        settings.Save();
                        
                        Debug.WriteLine("Found access token: " + access_token);
                    }
                    /*
                    if (keys.Key == "refresh_token")
                    {
                        refresh_token = keys.Value;
                        Debug.WriteLine("Found refresh token: " + refresh_token);
                    }
                     */
                }

            }
             
                 
        }

        private bool strCmp(string a, string b)
        {
            if (a.Length < b.Length)
                return false;
            bool equal = false;
            for (int i = 0; i < b.Length; i++)
            {

                if (a[i] == b[i])
                    equal = true;
                else
                {
                    equal = false;
                    break;
                }

            }

            return equal;
        }

        ProgressIndicator progress = new ProgressIndicator
        {
            IsVisible = false,
            IsIndeterminate = true
        };

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            string tasks_query = "https://www.producteev.com/api/tasks/search?&alias=all&sort=" + sort_mode + "&order=asc&access_token=" + access_token;


            System.Uri myUri = new System.Uri(tasks_query);
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(myUri);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/json";
            
            myRequest.BeginGetResponse(new AsyncCallback(TasksCallback), myRequest);

            progress.IsVisible = true;
        }




        private void TasksCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            HttpWebResponse response;

            try
            {
                response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Connection problem: " + e.Message);
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    progress.IsVisible = false;
                });
                return;
            }

            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                // read JSON from payload and map to ProducteevTasks
                string resultString = streamReader.ReadToEnd();
                var stream = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(resultString));
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ProducteevTasks.RootObject));
                ProducteevTasks.RootObject tasksModel = (ProducteevTasks.RootObject)jsonSerializer.ReadObject(stream);

                

                List<TaskData> tasks = new List<TaskData>();


                foreach(ProjectWork.ProducteevTasks.Task task in tasksModel.tasks)
                {
                    tasks.Add(new TaskData(task.title, task.priority, task.status));
                }

                

                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    App.ViewModel.LoadData(tasks);
                    progress.IsVisible = false;
                });

                
            }
        }

        private void FirstListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void pivotControl_LoadedPivotItem(object sender, PivotItemEventArgs e)
        {
            Debug.WriteLine("Loaded pivot item: " + e.Item.Header);

            if (e.Item.Name.Equals("tasksPivotItem"))
            {
                ApplicationBar.IsVisible = true;
            }
            else
            {
                ApplicationBar.IsVisible = false;
            }
        }
    }

    
    public class TaskData
    {
        public string title { get; set; }
        public string priority { get; set; }
        public string status { get; set; }

        public TaskData(string title, int priority, int status)
        {
            this.title = title;

            switch(priority) {
                case 0: this.priority = L10N.priority + ": " + L10N.zeroPriority; break;
                case 1: this.priority = L10N.priority + ": " + L10N.onePriority; break;
                case 2: this.priority = L10N.priority + ": " + L10N.twoPriority; break;
                case 3: this.priority = L10N.priority + ": " + L10N.threePriority; break;
                case 4: this.priority = L10N.priority + ": " + L10N.fourPriority; break;
                case 5: this.priority = L10N.priority + ": " + L10N.fivePriority; break;
                default: this.priority = L10N.priority + ": " + L10N.zeroPriority; break;
            }

            this.status = status == 1 ? (L10N.status + ": " + L10N.activeStatus) : (L10N.status + ": " +L10N.completedStatus);
        }

    }

    
}

