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

namespace ProjectWork.Model
{
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
