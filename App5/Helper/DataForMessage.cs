﻿using System;
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
    class DataForMessage
    {
        public string image { get; set; }
        public string Fullname { get; set; }
        public string time { get; set; }
        public string message { get; set; }
    }
    class DataForMessageFromSql
    {
        public string image { get; set; }
        public string Secondname { get; set; }
        public string name { get; set; }
        public string time { get; set; }
        public string message { get; set; }
        public string Addressee { get; set; }
    }
}