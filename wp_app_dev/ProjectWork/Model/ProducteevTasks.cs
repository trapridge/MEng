using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace ProjectWork
{
    public class ProducteevTasks
    {
        public class Creator
        {
            public string id { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string email { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string timezone { get; set; }
            public int timezone_utc_offset { get; set; }
            public string avatar_path { get; set; }
        }

        public class Responsible
        {
            public string id { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string email { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string timezone { get; set; }
            public int timezone_utc_offset { get; set; }
            public string avatar_path { get; set; }
        }

        public class Follower
        {
            public string id { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string email { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string timezone { get; set; }
            public int timezone_utc_offset { get; set; }
            public string avatar_path { get; set; }
        }

        public class ProjectCreator
        {
            public string id { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string email { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string timezone { get; set; }
            public int timezone_utc_offset { get; set; }
            public string avatar_path { get; set; }
        }

        public class NetworkCreator
        {
            public string id { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string email { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string timezone { get; set; }
            public int timezone_utc_offset { get; set; }
            public string avatar_path { get; set; }
        }
 
        public class Network
        {
            public string id { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string title { get; set; }
            public NetworkCreator creator { get; set; }
            public string url { get; set; }
        }

        public class Project
        {
            public string id { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string title { get; set; }
            public ProjectCreator creator { get; set; }
            public Network network { get; set; }
            public bool locked { get; set; }
            public string description { get; set; }
            public bool restricted { get; set; }
            public string url { get; set; }
        }

        public class Task
        {
            public string id { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string title { get; set; }
            public int priority { get; set; }
            public int status { get; set; }
            public Creator creator { get; set; }
            public List<Responsible> responsibles { get; set; }
            public List<Follower> followers { get; set; }
            public Project project { get; set; }
            public List<object> labels { get; set; }
            public List<object> subtasks { get; set; }
            public int notes_count { get; set; }
            public bool allday { get; set; }
            public int reminder { get; set; }
            public int permissions { get; set; }
            public string deadline_status { get; set; }
            public string url { get; set; }
        }

        public class RootObject
        {
            public List<Task> tasks { get; set; }
            public int total_hits { get; set; }
        }
    }
}
