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

    class RecyclerViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public ImageView imageView { get; set; }
        public TextView txtDescription { get; set; }

        private ItemClickListener itemClickListener;
        public event EventHandler<int> ItemClick;

        public RecyclerViewHolder(View itemView, Action<int> listener):base(itemView)
        {
            imageView = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            txtDescription = itemView.FindViewById<TextView>(Resource.Id.txtDescription);
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
            //itemView.SetOnClickListener(this);
           // itemView.SetOnLongClickListener(this);

        }

        public void SetItemClickListener(ItemClickListener itemClickListener)
        {
            this.itemClickListener = itemClickListener;
        }
        public void OnClick(View v)
        {
                itemClickListener.onClick(v,AdapterPosition,false);
        }

        public bool OnLongClick(View v)
        {
            itemClickListener.onClick(v, AdapterPosition, true);
            return true;
        }
    }
    class RecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<Data> lstData = new List<Data>();
        private Context context;
        public event EventHandler<int> ItemClick;

        public RecyclerViewAdapter(List<Data> lstData,Context context)
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
            RecyclerViewHolder viewHolder = holder as RecyclerViewHolder;
            viewHolder.imageView.SetImageResource(lstData[position].imageId);
            viewHolder.txtDescription.Text = lstData[position].description;

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
            View itemView = inflater.Inflate(Resource.Layout.item,parent,false);
            return new RecyclerViewHolder(itemView,onClick);
        }


    }
}