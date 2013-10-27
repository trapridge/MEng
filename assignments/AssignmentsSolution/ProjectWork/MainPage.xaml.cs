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

namespace ProjectWork
{
    public partial class MainPage : PhoneApplicationPage
    {

        

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
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            // login to producteev here
            my_popup_xaml.IsOpen = true;
        }

        Boolean done = false;
        private void webBrowser1_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            /*
            if (!done) 
            {
                string html = webBrowser1.SaveToString();
                string hackstring = "<meta name=\"viewport\" content=\"width=320,user-scalable=no\" />";
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
            //string auth_url = "https://www.producteev.com/oauth/v2/auth?client_id=526c1ce17374607236000000_1do9mo8ue98kgcssgo8ksg84o88wkgos40ks0c44480o0w448w&response_type=token&redirect_uri=http%3A%2F%2Fwww.google.com";
            string auth_url = "http://goo.gl/b3WUJI";
            
            webBrowser1.Visibility = Visibility.Visible;

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                webBrowser1.Navigate(new Uri(auth_url));
            });

        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            my_popup_xaml.IsOpen = false;
        }


        string access_token;
        string refresh_token;

        private void webBrowser1_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (strCmp(e.Uri.AbsoluteUri, "https://www.google.") == true || strCmp(e.Uri.AbsoluteUri, "https://localhost") == true || strCmp(e.Uri.AbsoluteUri, "test.html") == true)
            {
                Debug.WriteLine(e.Uri.AbsoluteUri);

                string[] _params = e.Uri.Query.Split('&');
                foreach(String param in _params)
                {
                    if(param.Contains("access_token"))
                    {
                        string[] temp = param.Split('=');
                        access_token = temp[temp.Length - 1];
                    }

                    if (param.Contains("refresh_token"))
                    {
                        string[] temp = param.Split('=');
                        refresh_token = temp[temp.Length - 1];
                    }
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
            IsIndeterminate = true,
            //Text = "Refreshing tasks"
        };

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            string tasks_query = "https://www.producteev.com/api/tasks/search?access_token=FvgUUxkUwUJB9JhBMKCDxw8tUhvYzAiA6GpPccaS7Ew&alias=responsible&sort=priority&order=asc";


            System.Uri myUri = new System.Uri(tasks_query);
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(myUri);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/json";
            
            //myRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), myRequest);

            //HttpWebRequest myRequest = (HttpWebRequest)callbackResult.AsyncState;
            // End the stream request operation
            //Stream postStream = myRequest.EndGetRequestStream(callbackResult);

            // Create the post data
            //string postData = "consumer_key=consumerkey&redirect_uri=http://www.google.com";
            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Add the post data to the web request
            //postStream.Write(byteArray, 0, byteArray.Length);
            //postStream.Close();

            // Start the web request
            myRequest.BeginGetResponse(new AsyncCallback(TasksCallback), myRequest);


            progress.IsVisible = true;
        }




        private void TasksCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
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
    }

    
    public class TaskData
    {
        public string title { get; set; }
        public int priority { get; set; }
        public int status { get; set; }

        public TaskData(string title, int priority, int status)
        {
            this.title = title;
            this.priority = priority;
            this.status = status;
        }

    }
    
}

