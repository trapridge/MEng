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
        private string currentProject = "";
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
                getTasks();
                //App.ViewModel.LoadData();
            }
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            // login to producteev here
            my_popup_xaml.IsOpen = true;
            // connect
            connectToProducteev();
        }

        private Uri producteevLoginUri = new Uri("https://www.producteev.com/oauth/v2/auth_login");
        private HttpWebRequest _webRequest;
        private CookieContainer _cookieContainer = new CookieContainer();

        private void connectToProducteev()
        {
            string auth_url = "http://goo.gl/b3WUJI";

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                _webRequest = (HttpWebRequest)HttpWebRequest.Create(producteevLoginUri);
                _webRequest.CookieContainer = _cookieContainer;

                webBrowser1.Navigate(new Uri(auth_url));
                webBrowser1.Visibility = Visibility.Visible;
            });

        }
        

        private void btn_continue_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            my_popup_xaml.IsOpen = false;
        }

        
        public void ClearCookies(Uri uri)
        {
            var cookies = _cookieContainer.GetCookies(uri);

            foreach (Cookie cookie in cookies)
            {
                cookie.Discard = true;
                cookie.Expired = true;
            }
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

                }

                ClearCookies(producteevLoginUri);
                my_popup_xaml.IsOpen = false;
                webBrowser1.Visibility = Visibility.Collapsed;

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
            getTasks();
        }

        private void getTasks()
        {
            string tasks_query = "https://www.producteev.com/api/tasks/search?&alias=all&sort=" + sort_mode + "&order=asc&access_token=" + access_token;

            System.Uri myUri = new System.Uri(tasks_query);
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(myUri);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/json";

            /*
            string postData = "{metaData:{appVersion:1.40},....}";
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postData);
            postStream.Write(byteArray, 0, postData.Length);
            postStream.Close();
            */

            //myRequest.BeginGetResponse(new AsyncCallback(TasksCallback), myRequest);
            myRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), myRequest);
    

            progress.IsVisible = true;
        }

        public void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            Stream postStream = request.EndGetRequestStream(asynchronousResult);
            //string postData = currentProject.Length > 0 ? "{\"projects\":[\"" + currentProject + "\"]}" : "";
            string postData = "";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData.ToString());
            postStream.Write(byteArray, 0, postData.Length);
            postStream.Close();

            request.BeginGetResponse(new AsyncCallback(TasksCallback), request);
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
                List<ProjectData> projects = new List<ProjectData>();

                List<string> projectsIds = new List<string>();
                foreach(ProjectWork.ProducteevTasks.Task task in tasksModel.tasks)
                {
                    if (currentProject.Length == 0 || currentProject.Equals("all") || currentProject.Equals(task.project.id))
                    {
                        tasks.Add(new TaskData(task.title, task.priority, task.status));
                    }

                    if (!projectsIds.Contains(task.project.id))
                    {
                        projects.Add(new ProjectData(task.project.title, task.project.id));
                        projectsIds.Add(task.project.id);
                    }
                }

                projects.Add(new ProjectData("All projects", "all"));

                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    App.ViewModel.LoadData(tasks, projects);
                    progress.IsVisible = false;
                    pivotControl.SelectedIndex = 0;
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

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Debug.WriteLine("tapped: " + ((ItemViewModel)ThirdListBox.SelectedItem).LineTwo);
            currentProject = ((ItemViewModel)ThirdListBox.SelectedItem).LineTwo;
            getTasks();
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

    public class ProjectData
    {
        public string title { get; set; }
        public string id { get; set; }
        
        public ProjectData(string title, string id)
        {
            this.title = title;
            this.id = id;
        }

    }
}

