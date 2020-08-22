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

    class RecyclerViewHolderEvents : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public LinearLayout LL { get; set; }
        public TextView name { get; set; }
        public TextView Desc { get; set; }

        private ItemClickListener itemClickListener;
        public event EventHandler<int> ItemClick;

        public RecyclerViewHolderEvents(View itemView, Action<int> listener):base(itemView)
        {

            LL = itemView.FindViewById<LinearLayout>(Resource.Id.LLBack);
            name = itemView.FindViewById<TextView>(Resource.Id.NameEventsRecycler);
            Desc = itemView.FindViewById<TextView>(Resource.Id.DescEventsRecycler);
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
    class RecyclerViewAdapterEvents : RecyclerView.Adapter
    {
        private List<DataEvents> lstData = new List<DataEvents>();
        private Context context;
        public event EventHandler<int> ItemClick;

        public RecyclerViewAdapterEvents(List<DataEvents> lstData,Context context)
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
            RecyclerViewHolderEvents viewHolder = holder as RecyclerViewHolderEvents;
            viewHolder.LL.SetBackgroundResource(resourceId);
            viewHolder.name.Text = lstData[position].Name;
            viewHolder.Desc.Text = lstData[position].Descriptions;

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
            View itemView = inflater.Inflate(Resource.Layout.ItemEvents,parent,false);
            return new RecyclerViewHolderEvents(itemView,onClick);
        }


    }
}