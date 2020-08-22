using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Android;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Auth.Helper;
using Newtonsoft.Json;

namespace App5
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class histotr : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {

        private RecyclerView recycler;
        private RecyclerViewAdapter adapter;
        private RecyclerView.LayoutManager layoutManager;
        private List<Data> lstData = new List<Data>();
        private List<DataFromHistOtr> Dsql = new List<DataFromHistOtr>();
        private DataForLeftMenu Dsql1 = new DataForLeftMenu();
        private ImageView imageSrc;
        private TextView Fullname;
        private TextView id;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_mainHistotr);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            View hv = navigationView.GetHeaderView(0);
            Fullname = hv.FindViewById<TextView>(Resource.Id.Fullname);
            id = hv.FindViewById<TextView>(Resource.Id.ID);
            imageSrc = hv.FindViewById<ImageView>(Resource.Id.imageView);
            navigationView.SetNavigationItemSelectedListener(this);

            Dsql1 = req(Dsql1);
            
            string Full = Dsql1.Secondname + " " + Dsql1.Name;
            Fullname.Text = Full;
            id.Text = "ID - " + Sessions.GetSession();
            int resourceId = (int)typeof(Resource.Drawable).GetField(Dsql1.image).GetValue(null);
            imageSrc.SetImageResource(resourceId);
            InitData();
            recycler = FindViewById<RecyclerView>(Resource.Id.recycler);
            recycler.HasFixedSize = true;
            layoutManager = new LinearLayoutManager(this);
            recycler.SetLayoutManager(layoutManager);
            adapter = new RecyclerViewAdapter(lstData, this);
            adapter.ItemClick += OnItemClick;
            recycler.SetAdapter(adapter);
        }
        private void InitData()
        {
            WebRequest request = WebRequest.Create("http://192.168.56.1/WebApplication1/Home/Hist_otr");
            request.Method = "POST";
            string data = "";
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
            Dsql = JsonConvert.DeserializeObject<List<DataFromHistOtr>>(json_string);
            response.Close();
            int i = 0;
            while (i < Dsql.Count)
            {
                int resourceId = (int)typeof(Resource.Drawable).GetField(Dsql[i].path).GetValue(null);
                lstData.Add(new Data() { imageId = resourceId, description = Dsql[i].Name + " - " + Dsql[i].description });
                i++;
            }
        }
        void OnItemClick(object sender, int position)
        {
            Intent intent = new Intent(this, typeof(AboutOtr));
            intent.PutExtra("id", position);
            StartActivity(intent);
            Finish();
        }
        public DataForLeftMenu req(DataForLeftMenu Dsql1)
        {
            WebRequest request1 = WebRequest.Create("http://192.168.56.1/WebApplication1/Home/GetImageFullname");
            request1.Method = "POST";
            string idS = Sessions.GetSession();
            string data1 = "id=" + idS;
            byte[] byteArray1 = System.Text.Encoding.UTF8.GetBytes(data1);
            request1.ContentType = "application/x-www-form-urlencoded";
            request1.ContentLength = byteArray1.Length;
            using (System.IO.Stream dataStream = request1.GetRequestStream())
            {
                dataStream.Write(byteArray1, 0, byteArray1.Length);
            }
            WebResponse response1 = request1.GetResponse();
            string json_string1 = "";
            using (System.IO.Stream stream = response1.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    json_string1 = reader.ReadToEnd();
                }
            }
            Dsql1 = JsonConvert.DeserializeObject<DataForLeftMenu>(json_string1);
            response1.Close();
            return Dsql1;
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }


        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_profile)
            {
                StartActivity(new Android.Content.Intent(this, typeof(Profile)));
                Finish();
            }
            else if (id == Resource.Id.nav_message)
            {
                StartActivity(new Android.Content.Intent(this, typeof(MainActivity)));
                Finish();
            }
            else if (id == Resource.Id.nav_event)
            {
                StartActivity(new Android.Content.Intent(this, typeof(AllEvents)));
                Finish();
            }
            else if (id == Resource.Id.nav_infbase)
            {
                StartActivity(new Android.Content.Intent(this, typeof(Infomenu)));
                Finish();
            }
            else if (id == Resource.Id.nav_Logout)
            {
                Sessions.stopsession();
                StartActivity(new Android.Content.Intent(this, typeof(Login)));
                Finish();
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

