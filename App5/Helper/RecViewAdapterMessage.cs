using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using App5;

namespace Auth.Helper
{
    class RecViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public ImageView imageView { get; set; }
        public TextView login { get; set; }
        public TextView Message { get; set; }
        public TextView time { get; set; }

        private ItemClickListener itemClickListener;
        public event EventHandler<int> ItemClick;

        public RecViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            imageView = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            login = itemView.FindViewById<TextView>(Resource.Id.login);
            Message = itemView.FindViewById<TextView>(Resource.Id.Message);
            time = itemView.FindViewById<TextView>(Resource.Id.time);
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
            //itemView.SetOnClickListener(this);
            //itemView.SetOnLongClickListener(this);

        }

        public void SetItemClickListener(ItemClickListener itemClickListener)
        {
            this.itemClickListener = itemClickListener;
        }
        public void OnClick(View v)
        {
            itemClickListener.onClick(v, AdapterPosition, false);
        }

        public bool OnLongClick(View v)
        {
            itemClickListener.onClick(v, AdapterPosition, true);
            return true;
        }
    }
    class RecViewAdapterMessage : RecyclerView.Adapter
    {
        private List<DataForMessage> lstData = new List<DataForMessage>();
        private Context context;
        public event EventHandler<int> ItemClick;

        public RecViewAdapterMessage(List<DataForMessage> lstData, Context context)
        {
            this.lstData = lstData;
            this.context = context;
        }

        public override int ItemCount
        {
            get
            {
                return lstData.Count();
            }

        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            int resourceId = (int)typeof(Resource.Drawable).GetField(lstData[position].image).GetValue(null);
            RecViewHolder viewHolder = holder as RecViewHolder;
            viewHolder.imageView.SetImageResource(resourceId);
            viewHolder.login.Text = lstData[position].Fullname;
            viewHolder.Message.Text = lstData[position].message;
            viewHolder.time.Text = lstData[position].time;


            // viewHolder.SetItemClickListener(this);
        }

        public void onClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.ItemMessage, parent, false);
            return new RecViewHolder(itemView, onClick);
        }


    }
}