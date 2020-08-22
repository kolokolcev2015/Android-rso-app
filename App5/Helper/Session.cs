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
    class Sessions
    {
        static string SESSION_ID = null;

        static public bool startsession(string id)
        {
            SESSION_ID = id;
            if (SESSION_ID != null) return true;
            else return false;
        }
        static public bool stopsession()
        {
            SESSION_ID = null;
            if (SESSION_ID == null) return true;
            else return false;
        }
        static public string GetSession()
        {
            return SESSION_ID;
        }
    }
}