using System;
using System.IO;
using System.Net;
using Android;
using Android.App;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Auth.Helper;
using Newtonsoft.Json;
using static Android.Views.View;

namespace App5
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class InfoEvents : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, IOnClickListener
    {
        string image;
        private DataForLeftMenu Dsql1 = new DataForLeftMenu();
        private DataEventsUsers DsqlEv = new DataEventsUsers();
        private ImageView imageSrc;
        private TextView Fullname;
        private TextView id;
        LinearLayout LLBack;
        TextView Visit,org;
        TextView Name, Descriptions, Date, Time, Place, Count;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            image = Intent.GetStringExtra("image");
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_mainInfoEvents);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

           // FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
           // fab.Click += FabOnClick;

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
            DsqlEv = GetEventsInfo(DsqlEv,image);

            LLBack = (LinearLayout)FindViewById(Resource.Id.LLBackInfoEvent);
            Name = (TextView)FindViewById(Resource.Id.NameEventsInfo);
            Descriptions = (TextView)FindViewById(Resource.Id.DescEventsInfo);
            Date = (TextView)FindViewById(Resource.Id.DataEvent);
            Time = (TextView)FindViewById(Resource.Id.Timestart);
            Place = (TextView)FindViewById(Resource.Id.PlaceEvent);
            //Count = (TextView)FindViewById(Resource.Id.CountEvent);

            int resourceId1 = (int)typeof(Resource.Drawable).GetField(image).GetValue(null);
            LLBack.SetBackgroundResource(resourceId1);
            Name.Text = DsqlEv.Name;
            Descriptions.Text = DsqlEv.Descriptions;
            Date.Text = DsqlEv.Date;
            Time.Text = DsqlEv.Time;
            Place.Text = DsqlEv.Place;
           // Count.Text = DsqlEv.Count;

            Visit = (TextView)FindViewById(Resource.Id.Visits);
            org = (TextView)FindViewById(Resource.Id.org);
            if (DsqlEv.Status == "0")
            {
                Visit.Text = "Стать участником";
                org.Text = "Стать организатором";
            }
            else if (DsqlEv.Status == "1")
            {
                Visit.Text = "Покинуть мероприятие";
                org.Text = "Стать организатором";
            }
            else if (DsqlEv.Status == "2")
            {
                Visit.Text = "Стать участником";
                org.Text = "Покинуть статус организатора";
            }
            Visit.SetOnClickListener(this);

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
        public DataEventsUsers GetEventsInfo(DataEventsUsers Dsql1,string img)
        {
            WebRequest request1 = WebRequest.Create("http://192.168.56.1/WebApplication1/Home/UsersEvents");
            request1.Method = "POST";
            string idS = Sessions.GetSession();
            string data1 = "image=" + img + "&users_id="+idS;
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
            Dsql1 = JsonConvert.DeserializeObject<DataEventsUsers>(json_string1);
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

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
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

            }
            else if (id == Resource.Id.nav_infbase)
            {
                StartActivity(new Android.Content.Intent(this, typeof(Infomenu)));
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

        public void OnClick(View v)
        {//Кликает на кнопку только один раз, дальше не слушает

            if (v.Id == Resource.Id.Visits)
            {
                if (Visit.Text == "Стать участником")
                {
                    WebRequest request1 = WebRequest.Create("http://192.168.56.1/WebApplication1/Home/SetVisitStatus");
                    request1.Method = "POST";
                    string idS = Sessions.GetSession();
                    string data1 = "image=" + image + "&users_id=" + idS;
                    byte[] byteArray1 = System.Text.Encoding.UTF8.GetBytes(data1);
                    request1.ContentType = "application/x-www-form-urlencoded";
                    request1.ContentLength = byteArray1.Length;
                    using (System.IO.Stream dataStream = request1.GetRequestStream())
                    {
                        dataStream.Write(byteArray1, 0, byteArray1.Length);
                    }
                    
                    Visit = (TextView)FindViewById(Resource.Id.Visits);
                    Visit.Text = "Покинуть мероприятие";
                    Count.Text = Convert.ToString(Convert.ToInt32(Count.Text) + 1);
                }
                if (Visit.Text == "Покинуть мероприятие")
                {
               

                    Visit = (TextView)FindViewById(Resource.Id.Visits);
                    Visit.Text = "Стать участником";
                    Count.Text = Convert.ToString(Convert.ToInt32(Count.Text) - 1);
                }
            }
            
        }
    }
}

