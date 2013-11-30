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
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;
using ProjectWork.Model;

namespace ProjectWork
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string currentProject = "all";
        private string access_token;
        private string sort_mode = "title";
        private Uri producteevLoginUri = new Uri("https://www.producteev.com/oauth/v2/auth_login");
        private HttpWebRequest _webRequest;
        private CookieContainer _cookieContainer = new CookieContainer();
        ProgressIndicator progress = new ProgressIndicator
        {
            IsVisible = false,
            IsIndeterminate = true
        };

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            SystemTray.SetProgressIndicator(this, progress);
        }

        // event handlers ----------------------------------------------------------------------------->
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            string at = "";
            if (Helpers.settings.TryGetValue("access_token", out at))
            {
                Debug.WriteLine("Found access_token from storage");
                access_token = at;
            }

            if (!App.ViewModel.IsDataLoaded)
            {
                getTasks();
            }

            if (currentProject.Equals("all"))
            {
                ApplicationBarIconButton btn = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
                btn.IsEnabled = false;
            }
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            my_popup_xaml.IsOpen = true;
            connectToProducteev();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            my_popup_xaml.IsOpen = false;
        }

        private void webBrowser1_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

            if (Helpers.strCmp(e.Uri.AbsoluteUri, "https://www.google.") == true)
            {
                Debug.WriteLine(e.Uri.AbsoluteUri);

                foreach (KeyValuePair<string, string> keys in Helpers.ParseQueryString(e.Uri))
                {
                    if (keys.Key == "access_token")
                    {
                        access_token = keys.Value;

                        // If the key exists
                        if (Helpers.settings.Contains("access_token"))
                        {
                            // Store the new value
                            Helpers.settings["access_token"] = access_token;
                        }
                        else
                        {
                            Helpers.settings["access_token"] = access_token;

                        }
                        Helpers.settings.Save();

                        Debug.WriteLine("Found access token: " + access_token);
                    }

                }

                ClearCookies(producteevLoginUri);
                my_popup_xaml.IsOpen = false;
                webBrowser1.Visibility = Visibility.Collapsed;

            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            getTasks();
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

            ApplicationBarIconButton btn = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            if (currentProject.Equals("all"))
            {
                btn.IsEnabled = false;
            }

            getTasks();
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            ApplicationBarIconButton btn = (ApplicationBarIconButton)ApplicationBar.Buttons[0];

            if (sort_mode.Equals("title"))
            {
                sort_mode = "priority";
                btn.Text = "Sort by title";
            }
            else
            {
                sort_mode = "title";
                btn.Text = "Sort by priority";
            }
            getTasks();
        }

        private void TextBlock_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ItemViewModel t = ((ItemViewModel)FirstListBox.SelectedItem);

            Debug.WriteLine("tapped: " + t.LineOne);
            Debug.WriteLine("Navigating to: " + "/TaskPage.xaml?title=" + t.LineOne + "&priority=" + t.LineTwo + "&status=" + t.LineThree + "&id=" + t.LineFour + "&project=" + currentProject);
            NavigationService.Navigate(new Uri("/TaskPage.xaml?title=" + t.LineOne + "&priority=" + t.LineTwo + "&status=" + t.LineThree + "&id=" + t.LineFour + "&project=" + currentProject, UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/TaskPage.xaml?project=" + currentProject, UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            ShellTile tile = ShellTile.ActiveTiles.First();
            if (null != tile)
            {
                StandardTileData data = new StandardTileData();
                data.Title = "Pasks";
                data.BackTitle = "Producteev tasks";
                tile.Update(data);
            }
        }

        // -----------------------------------------------------------------------------> event handlers

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

        private void getTasks()
        {
            string tasks_query = "https://www.producteev.com/api/tasks/search?&alias=all&sort=" + sort_mode + "&order=asc&access_token=" + access_token;

            System.Uri myUri = new System.Uri(tasks_query);
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(myUri);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/json";
            myRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), myRequest);
    
            progress.IsVisible = true;
        }

        public void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            Stream postStream = request.EndGetRequestStream(asynchronousResult);
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
                    MessageBox.Show(L10N.connectionProblem);
                });
                return;
            }

            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
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
                        tasks.Add(new TaskData(task.title, task.priority, task.status, task.id));
                    }

                    if (!projectsIds.Contains(task.project.id))
                    {
                        projects.Add(new ProjectData(task.project.title, task.project.id));
                        projectsIds.Add(task.project.id);
                    }
                }

                projects.Add(new ProjectData(L10N.allProjects, "all"));

                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    App.ViewModel.LoadData(tasks, projects);
                    progress.IsVisible = false;
                    pivotControl.SelectedIndex = 0;
                });

                
            }
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

    }

}

