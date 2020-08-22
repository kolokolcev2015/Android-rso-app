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
    public class Profile : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, IOnClickListener
    {
        TextView Login, ID, Secondname, Name, Fathersname, date, otr, role;
        ImageView img;
        DataAllInfUser Dsql = new DataAllInfUser();
        private DataForLeftMenu Dsql1 = new DataForLeftMenu();
        private ImageView imageSrc;
        private TextView Fullname;
        private TextView id;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_mainProfile);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

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
            string idS = Sessions.GetSession();
            id.Text = "ID - " + idS; 
            int resourceId = (int)typeof(Resource.Drawable).GetField(Dsql1.image).GetValue(null);
            imageSrc.SetImageResource(resourceId);

            Dsql = Info(Dsql);
           //TextView ChangeFam = (TextView)FindViewById(Resource.Id.change_fam);
            //ChangeFam.SetOnClickListener(this);
            Login = FindViewById<TextView>(Resource.Id.Login);
            ID = FindViewById<TextView>(Resource.Id.ID);
            Secondname = FindViewById<TextView>(Resource.Id.Secondname);
            Name = FindViewById<TextView>(Resource.Id.Name);
            Fathersname = FindViewById<TextView>(Resource.Id.Fathersname);
            date = FindViewById<TextView>(Resource.Id.Date);
            otr = FindViewById<TextView>(Resource.Id.Otr);
            role = FindViewById<TextView>(Resource.Id.Role);
            img = FindViewById<ImageView>(Resource.Id.iconProfile);

            Login.Text = Dsql.Login;
            ID.Text = "ID - " + Sessions.GetSession();
            Secondname.Text = Dsql.Secondname;
            Name.Text = Dsql.Name;
            Fathersname.Text = Dsql.Fathersname;
            date.Text = Dsql.Date;
            otr.Text = Dsql.otr;
            role.Text = Dsql.role;
            int resourceId1 = (int)typeof(Resource.Drawable).GetField(Dsql.image).GetValue(null);
            img.SetImageResource(resourceId1);
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
        public void OnClick(View v)
        {
           // if (v.Id == Resource.Id.change_fam)
          //  { 
                
           // }
        }
                public DataAllInfUser Info(DataAllInfUser dsql)
        {
            WebRequest request = WebRequest.Create("http://192.168.56.1/WebApplication1/Home/GetFullInfo");
            request.Method = "POST";
            string idS = Sessions.GetSession();
            string data = "id=" + idS;
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
            dsql = JsonConvert.DeserializeObject<DataAllInfUser>(json_string);
            response.Close();
            return dsql;
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

