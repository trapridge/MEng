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
    public class TaskData
    {
        public string title { get; set; }
        public string priority { get; set; }
        public string status { get; set; }
        public string id { get; set; }

        public TaskData(string title, int priority, int status, string id)
        {
            this.title = title;

            switch (priority)
            {
                case 0: this.priority = L10N.priority + ": " + L10N.zeroPriority; break;
                case 1: this.priority = L10N.priority + ": " + L10N.onePriority; break;
                case 2: this.priority = L10N.priority + ": " + L10N.twoPriority; break;
                case 3: this.priority = L10N.priority + ": " + L10N.threePriority; break;
                case 4: this.priority = L10N.priority + ": " + L10N.fourPriority; break;
                case 5: this.priority = L10N.priority + ": " + L10N.fivePriority; break;
                default: this.priority = L10N.priority + ": " + L10N.zeroPriority; break;
            }

            this.status = status == 1 ? (L10N.status + ": " + L10N.activeStatus) : (L10N.status + ": " + L10N.completedStatus);
            this.id = id;
        }
    }
}
