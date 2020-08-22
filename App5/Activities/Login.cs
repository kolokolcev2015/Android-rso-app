using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using static Android.Views.View;
using Android.Views;
using System;
using Android.Support.Design.Widget;
using System.Data.SqlClient;
using System.Net;
using Android.Media;
using System.IO;
using Android.SE.Omapi;
using Auth.Helper;
using App5;
using Auth.Activities;

namespace App5
{
    [Activity(MainLauncher = true)]
    public class Login : AppCompatActivity, IOnClickListener
    {
        Button btnLogin;
        EditText input_login, input_password;
        TextView btnSignUp, btnForgotPassword;

        LinearLayout act_main;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main1);
            btnLogin = FindViewById<Button>(Resource.Id.login_btn_login);
            input_login = FindViewById<EditText>(Resource.Id.login_email);
            input_password = FindViewById<EditText>(Resource.Id.login_password);
            btnSignUp = FindViewById<TextView>(Resource.Id.login_btn_sign_up);
            btnForgotPassword = FindViewById<TextView>(Resource.Id.login_btn_forgot_password);
            act_main = FindViewById<LinearLayout>(Resource.Id.linearLayout1);

            btnSignUp.SetOnClickListener(this);
            btnForgotPassword.SetOnClickListener(this);
            btnLogin.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.login_btn_forgot_password)
            {
                //StartActivity(new Android.Content.Intent(this, typeof(ForgotPass)));
                //Finish();
            }
            else if (v.Id == Resource.Id.login_btn_sign_up)
            {

               StartActivity(new Android.Content.Intent(this, typeof(SignUp)));
               Finish();
            }
            else if (v.Id == Resource.Id.login_btn_login)
            {
                if (input_login.Text=="" || input_password.Text == "")
                {
                    Snackbar SnackBar = Snackbar.Make(act_main, "Input login and password", Snackbar.LengthShort);
                    SnackBar.Show();
                }
                else
                {
                    WebRequest request = WebRequest.Create("http://192.168.56.1/WebApplication1/Home/Auth");
                    request.Method = "POST";
                    string data = "input_login=" + input_login.Text + "&input_password=" + input_password.Text;
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
                            json_string += reader.ReadToEnd();
                        }
                    }
                    //my_Free_Tournament = JsonConvert.DeserializeObject<My_Free_Tournament>(json_string);
                    response.Close();
                    int result = Convert.ToInt32(json_string);
                    if (result > 0)
                    {
                        if (Sessions.startsession(json_string))
                        {
                            StartActivity(new Android.Content.Intent(this, typeof(MainActivity)));
                            Finish();
                        }
                    }
                    else
                    {
                        Snackbar SnackBar1 = Snackbar.Make(act_main, "Uncorrected login or password " + result, Snackbar.LengthShort);
                        SnackBar1.Show();
                    }
                }
            }
        }
    }
}