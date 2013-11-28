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
using System.Windows.Navigation;
using Microsoft.Phone.Shell;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace ProjectWork
{
    public partial class Page1 : PhoneApplicationPage
    {
        ProgressIndicator progress = new ProgressIndicator
        {
            IsVisible = false,
            IsIndeterminate = true
        };
        private string taskId;
        private string projectId;

        public Page1()
        {
            InitializeComponent();
            SystemTray.SetProgressIndicator(this, progress);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string title = "";
            string priority = "0";
            string status = "1";
            string id = "";
            string project = "";
            if (NavigationContext.QueryString.TryGetValue("title", out title)) titleTextBox.Text = title;
            if (NavigationContext.QueryString.TryGetValue("priority", out priority))
            {
                if (priority.Equals(L10N.priority + ": " + L10N.zeroPriority)) prioritySlider.Value = 0;
                else if (priority.Equals(L10N.priority + ": " + L10N.onePriority)) prioritySlider.Value = 1;
                else if (priority.Equals(L10N.priority + ": " + L10N.twoPriority)) prioritySlider.Value = 2;
                else if (priority.Equals(L10N.priority + ": " + L10N.threePriority)) prioritySlider.Value = 3;
                else if (priority.Equals(L10N.priority + ": " + L10N.fourPriority)) prioritySlider.Value = 4;
                else if (priority.Equals(L10N.priority + ": " + L10N.fivePriority)) prioritySlider.Value = 5;
            }

            if (NavigationContext.QueryString.TryGetValue("status", out status))
            {
                if (status.Equals(L10N.status + ": " + L10N.activeStatus)) completedCheckBox.IsChecked = false;
                else completedCheckBox.IsChecked = true;
                    
            }
            if (NavigationContext.QueryString.TryGetValue("id", out id))
            {
                taskId = id;
            }

            if (NavigationContext.QueryString.TryGetValue("project", out project))
            {
                projectId = project;
            }

            if (taskId == null)
            {
                completedCheckBox.IsEnabled = false;
                prioritySlider.IsEnabled = false;
                deleteButton.IsEnabled = false;
            }
            else
            {
                completedCheckBox.IsEnabled = true;
                prioritySlider.IsEnabled = true;
                deleteButton.IsEnabled = true;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            saveTask();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            deleteTask();
        }


        private void saveTask()
        {
            String at;
            if (!Helpers.settings.TryGetValue("access_token", out at))
            {
                Debug.WriteLine("Cannot get access token");
            }

            string tasks_query;
            if (taskId == null)
            {
                tasks_query = "https://www.producteev.com/api/tasks/?access_token=" + at;
            }
            else 
            {
                tasks_query = "https://www.producteev.com/api/tasks/" + taskId + "?access_token=" + at;
            }


            System.Uri myUri = new System.Uri(tasks_query);
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(myUri);
            myRequest.Method = taskId == null ? "POST" : "PUT";
            myRequest.ContentType = "application/json";
            myRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), myRequest);

            progress.IsVisible = true;
        }

        public void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
                Stream postStream = request.EndGetRequestStream(asynchronousResult);

                Debug.WriteLine("prioritySlider.Value: " + (int)prioritySlider.Value);

                

                string postData = taskId != null ?  "{\"task\":{\"status\":" + (completedCheckBox.IsChecked == true ? 0 : 1) + ",\"title\":\"" + titleTextBox.Text + "\",\"priority\":" + (int)prioritySlider.Value + "}}" :
                    "{\"task\":{\"title\":\"" + titleTextBox.Text + "\",\"project\":{\"id\":\"" + projectId + "\"}}}";

                Debug.WriteLine(postData);

                byte[] byteArray = Encoding.UTF8.GetBytes(postData.ToString());
                postStream.Write(byteArray, 0, postData.Length);
                postStream.Close();

                request.BeginGetResponse(new AsyncCallback(TasksCallback), request);
            });

        }

        private void deleteTask()
        {
            String at;
            if (!Helpers.settings.TryGetValue("access_token", out at))
            {
                Debug.WriteLine("Cannot get access token");
            }

            string tasks_query = "https://www.producteev.com/api/tasks/" + taskId + "?access_token=" + at;

            System.Uri myUri = new System.Uri(tasks_query);
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(myUri);
            myRequest.Method = "DELETE";
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
                    MessageBox.Show("Connection problem saving/deleting task");
                });
                return;
            }

            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                progress.IsVisible = false;
                NavigationService.Navigate(new Uri("/MainPage.xaml?from=taskPage", UriKind.Relative));
            });
            
        }

    }
}