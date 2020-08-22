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
using Auth.Helper;
using Com.Github.Library.Bubbleview;

namespace App5.Helper
{
    class CustomAdapter : BaseAdapter
    {
        private List<DataForChat> Lst = new List<DataForChat>();
        Context context;
        LayoutInflater inflater;

        public CustomAdapter(List<DataForChat> Lst,Context context)
        {
            this.Lst = Lst;
            this.context = context;
            inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
        }
        public override int Count
        {
            get
            {
                return Lst.Count;   
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                if (Lst[position].recv == "1")
                {
                    view = inflater.Inflate(Resource.Layout.ItemChatRecv, null);
                    BubbleTextView text_message = view.FindViewById<BubbleTextView>(Resource.Id.text_message);
                    ImageView img = view.FindViewById<ImageView>(Resource.Id.adressee_msg);
                    int resourceId = (int)typeof(Resource.Drawable).GetField(Lst[position].image).GetValue(null);
                    img.SetImageResource(resourceId);
                    text_message.Text = Lst[position].message;
                }
                else
                {
                    view = inflater.Inflate(Resource.Layout.ItemChat, null);
                    BubbleTextView text_message = view.FindViewById<BubbleTextView>(Resource.Id.text_message);
                    text_message.Text = Lst[position].message;
                }
            }
            return view;
        }
    }
}