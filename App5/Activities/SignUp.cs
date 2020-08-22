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
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using App5;
using static Android.Views.View;

namespace App5
{
    [Activity(Label = "SignUp", Theme = "@style/AppTheme")]
    public class SignUp : AppCompatActivity, IOnClickListener
    {
        Button btnSignup;
        TextView btnLogin, btnForgotPass;
        EditText input_login, input_password, input_id;
        LinearLayout activity_sign_up;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SignUp);
            // view
            btnSignup = FindViewById<Button>(Resource.Id.signup_btn_register);
            btnLogin = FindViewById<TextView>(Resource.Id.signup_btn_login);
            btnForgotPass = FindViewById<TextView>(Resource.Id.signup_btn_forgot_password);
            input_login = FindViewById<EditText>(Resource.Id.signup_email);
            input_password = FindViewById<EditText>(Resource.Id.signup_password);
            activity_sign_up = FindViewById<LinearLayout>(Resource.Id.activity_sign_up);
            input_id = FindViewById<EditText>(Resource.Id.login_email);

            btnLogin.SetOnClickListener(this);
            btnForgotPass.SetOnClickListener(this);
            btnSignup.SetOnClickListener(this);

        }
        public void OnClick(View v)
        {
            if (v.Id == App5.Resource.Id.signup_btn_login)
            {
                StartActivity(new Intent(this, typeof(Login)));
                Finish();
            }
            else if (v.Id == App5.Resource.Id.signup_btn_forgot_password)
            {
               // StartActivity(new Intent(this, typeof(ForgotPass)));
              //  Finish();
            }
            else if (v.Id == Resource.Id.signup_btn_register)
            {
                WebRequest request = WebRequest.Create("http://192.168.56.1/WebApplication1/Home/Register");
                request.Method = "POST";
                string data = "input_login=" + input_login.Text + "&input_password=" + input_password.Text + "&input_id=" + input_id.Text;
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
                    Snackbar SnackBar = Snackbar.Make(activity_sign_up, "Новый пользователь был зарегистрирован!", Snackbar.LengthShort);
                    SnackBar.Show();

                }else
                {
                    Snackbar SnackBar = Snackbar.Make(activity_sign_up, "Ошибка регистрации, попробуйте позже!", Snackbar.LengthShort);
                    SnackBar.Show();
                }
            }
        }

      
    }
}