using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App5;
using Auth.Helper;
using Newtonsoft.Json;

namespace App5
{
    [Activity(Label = "AboutOtr")]
    class AboutOtr : Activity
    {
        TextView Name, napr, date, deviz, Count;
        ImageView Image { get; set; }
        DataAboutOtr Dsql = new DataAboutOtr();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            int id = Intent.GetIntExtra("id",0);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AboutOtr);
            Name = FindViewById<TextView>(Resource.Id.Name);
            napr = FindViewById<TextView>(Resource.Id.napr);
            date = FindViewById<TextView>(Resource.Id.Date);
            deviz = FindViewById<TextView>(Resource.Id.deviz);
            Count = FindViewById<TextView>(Resource.Id.Count);
            Image = FindViewById<ImageView>(Resource.Id.icon);

            WebRequest request = WebRequest.Create("http://192.168.56.1/WebApplication1/Home/About_otr");
            request.Method = "POST";
            string data = "id="+id;
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            using (System.IO.Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            WebResponse response = request.GetResponse();
            string json_string = "";
            using (System.IO.Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    json_string = reader.ReadToEnd();
                }
            }
            Dsql = JsonConvert.DeserializeObject<DataAboutOtr>(json_string);
            response.Close();
            int path1 = Choice(Dsql.Name);
            //Image.SetImageResource(path1);
            int resourceId = (int)typeof(Resource.Drawable).GetField("Granat").GetValue(null);
            Image.SetImageResource(resourceId);
            //Name.Text = Dsql.Name;
            napr.Text = Dsql.napr;
            date.Text = Dsql.date;
            deviz.Text = Dsql.deviz;
            Count.Text = Dsql.Count;



            // Create your application here
        }
        public int Choice(string s)
        {
            switch (s)
            {
                case "Spectr":
                    return Resource.Drawable.Spectr;
                case "Record":
                    return Resource.Drawable.Record;
                case "Kristal":
                    return Resource.Drawable.Kristal;
                case "Vertical":
                    return Resource.Drawable.Vertical;
                case "Muscat":
                    return Resource.Drawable.Muscat;
                case "Sirius":
                    return Resource.Drawable.Sirius;
                case "Nasledie":
                    return Resource.Drawable.Nasledie;
                case "Fenix":
                    return Resource.Drawable.Fenix;
                case "Granat":
                    return Resource.Drawable.Granat;

            }
            return Resource.Drawable.Spectr;
        }
    }
}