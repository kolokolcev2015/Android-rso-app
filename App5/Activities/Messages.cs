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
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using App5;
using Auth.Helper;
using Newtonsoft.Json;

namespace Auth.Activities
{
    [Activity(Label = "Messages")]
    public class Messages : Activity
    {
        private RecyclerView recycler;
        private RecViewAdapterMessage adapter;
        private RecyclerView.LayoutManager layoutManager;
        private List<DataForMessage> lstData = new List<DataForMessage>();
        private List<DataForMessageFromSql> Dsql = new List<DataForMessageFromSql>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Messages);
            InitData();

            recycler = FindViewById<RecyclerView>(Resource.Id.recycler);
            recycler.HasFixedSize = true;
            layoutManager = new LinearLayoutManager(this);
            recycler.SetLayoutManager(layoutManager);
            adapter = new RecViewAdapterMessage(lstData, this);
            adapter.ItemClick += OnItemClick;
            recycler.SetAdapter(adapter);

            // Create your application here
        }

        private void InitData()
        {
            WebRequest request = WebRequest.Create("http://192.168.56.1/WebApplication1/Home/Message");
            request.Method = "POST";
            string idSS = Sessions.GetSession();
            string login = "kolokolcev";
            string data = "id="+"1234567";
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

            Dsql = JsonConvert.DeserializeObject<List<DataForMessageFromSql>>(json_string);
            response.Close();
            int i = 0;
            
            while (i < Dsql.Count)
            {
                lstData.Add(new DataForMessage()
                {
                    image = Dsql[i].image,
                    Fullname = Dsql[i].Secondname + " " + Dsql[i].name,
                    time = Dsql[i].time,
                    message = Dsql[i].message
                });
            }



        }
        void OnItemClick(object sender, int position)
        {
            //Intent intent = new Intent(this, typeof(AboutOtr));
           // intent.PutExtra("id", position);
           // StartActivity(intent);
            //Finish();
        }
    }
}