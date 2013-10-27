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

namespace NetworkingAndTombstoning
{
    /* Mapper for weather data */
    public class Weather
    {
        public class Item
        {
            public string description { get; set; }
        }

        public class Channel
        {
            public Item item { get; set; }
        }

        public class Results
        {
            public Channel channel { get; set; }
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
    }
}
