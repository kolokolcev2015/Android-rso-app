using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Auth.Helper
{
    public class Data
    {
        public int imageId { get; set; }
        public string description { get; set; }
    }
    public class DataForLeftMenu
    {
        public string image { get; set; }
        public string Name { get; set; }

        public string Secondname { get; set; }

    }
    public class DataAllInfUser
    {
        public string image { get; set; }
        public string Login { get; set; }
        public string Secondname { get; set; }
        public string Name { get; set; }
        public string Fathersname { get; set; }
        public string Date { get; set; }
        public string otr { get; set; }
        public string role { get; set; }

    }
    public class DataEvents
    {
        public string image { get; set; }
        public string Name { get; set; }

        public string Descriptions { get; set; }

    }
    public class DataEventsUsers
    {
        public string Name { get; set; }
        public string Descriptions { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Place { get; set; }
        public string Count { get; set; }
        public string Status { get; set; }

    }
    public class DataForChat
    {
        public string image { get; set; }
        public string Secondname { get; set; }
        public string name { get; set; }
        public string time { get; set; }
        public string message { get; set; }
        public string Addressee { get; set; }
        public string recv { get; set; }

    }
}