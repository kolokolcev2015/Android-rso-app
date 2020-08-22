using System;
using System.Collections.Generic;
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
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using App5.Helper;
using Auth.Helper;
using Cenkgun.ChatBar;
using Newtonsoft.Json;

namespace App5
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class Chat : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {

        private RecyclerView recycler;
        private RecyclerViewAdapterChat adapter;
        private RecyclerView.LayoutManager layoutManager;

        private DataForLeftMenu Dsql1 = new DataForLeftMenu();
        private List<DataForChat> Sqlchat = new List<DataForChat>();
        private ImageView imageSrc;
        private TextView Fullname;
        private TextView id;
        string image, Name, Secondname;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            image = Intent.GetStringExtra("image");
            Name = Intent.GetStringExtra("Name");
            Secondname = Intent.GetStringExtra("Secondname");
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_mainChat);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += FabOnClick;

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
            ChatBarView chb = FindViewById<ChatBarView>(Resource.Id.chatbar);
            chb.SetMessageBoxHint("Введите сообщение..");
            string Full = Dsql1.Secondname + " " + Dsql1.Name;
            Fullname.Text = Full;
            id.Text = "ID - " + Sessions.GetSession();
            int resourceId = (int)typeof(Resource.Drawable).GetField(Dsql1.image).GetValue(null);
            imageSrc.SetImageResource(resourceId);
            ImageView img_top = (ImageView)FindViewById(Resource.Id.image_chat);
            TextView txt = (TextView)FindViewById(Resource.Id.Fname);
            int resourceId1 = (int)typeof(Resource.Drawable).GetField(image).GetValue(null);
            img_top.SetImageResource(resourceId1);
            txt.Text = Secondname + " " + Name;
            


            Sqlchat = GetMessages(Sqlchat);

            recycler = FindViewById<RecyclerView>(Resource.Id.recycler);
            recycler.HasFixedSize = true;
            layoutManager = new LinearLayoutManager(this);
            recycler.SetLayoutManager(layoutManager);
            adapter = new RecyclerViewAdapterChat(Sqlchat, this);
            recycler.SetAdapter(adapter);
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
        public List<DataForChat> GetMessages(List<DataForChat> Dsql1)
        {
            WebRequest request1 = WebRequest.Create("http://192.168.56.1/WebApplication1/Home/Chat");
            request1.Method = "POST";
            string idS = Sessions.GetSession();
            string data1 = "id=" + idS+"&image="+image;
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
            Dsql1 = JsonConvert.DeserializeObject<List<DataForChat>>(json_string1);
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
    }
}

