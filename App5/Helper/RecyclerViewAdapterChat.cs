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
using Com.Github.Library.Bubbleview;

namespace Auth.Helper
{

    class RecyclerViewHolderChat : RecyclerView.ViewHolder
    {


        public LinearLayout LL { get; set; }
        public TextView name { get; set; }
        public TextView Desc { get; set; }

        private ItemClickListener itemClickListener;
        public event EventHandler<int> ItemClick;

        public RecyclerViewHolderChat(View itemView):base(itemView)
        {

            LL = itemView.FindViewById<LinearLayout>(Resource.Id.LLBack);
            name = itemView.FindViewById<TextView>(Resource.Id.NameEventsRecycler);
            Desc = itemView.FindViewById<TextView>(Resource.Id.DescEventsRecycler);
        }

    }
    class RecyclerViewAdapterChat : RecyclerView.Adapter
    {
        private List<DataForChat> lstData = new List<DataForChat>();
        private Context context;
        public event EventHandler<int> ItemClick;


        public RecyclerViewAdapterChat(List<DataForChat> lstData,Context context)
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
            int type = GetItemViewType(position);
            if (type == 1)
            {
                int resourceId = (int)typeof(Resource.Drawable).GetField(lstData[position].image).GetValue(null);
                ViewHolderRecv viewHolder = holder as ViewHolderRecv;
                viewHolder.adressee_msg.SetImageResource(resourceId);
                viewHolder.text_message_recv.Text = lstData[position].message;
            }
            else
            {
                ViewHolderNotRecv viewHolder = holder as ViewHolderNotRecv;
                viewHolder.text_message_recv.Text = lstData[position].message;
            }
            //viewHolder.LL.SetBackgroundResource(resourceId);
            // viewHolder.name.Text = lstData[position].Name;
            //viewHolder.Desc.Text = lstData[position].Descriptions;

            // viewHolder.SetItemClickListener(this);
        }
        public void onClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
        //getviewtypecount
       
        public override int GetItemViewType(int position)
        {
            if (lstData[position].recv == "1")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View view;
            if (viewType == 1)
            {
                view = inflater.Inflate(Resource.Layout.ItemChatRecv, parent, false);
                return new ViewHolderRecv(view);
            }
            view = inflater.Inflate(Resource.Layout.ItemChat, parent, false);
            return new ViewHolderNotRecv(view);
        }


    }

}
class ViewHolderNotRecv : RecyclerView.ViewHolder
{
   public BubbleTextView text_message_recv { get; set; }
    public ViewHolderNotRecv(View itemView) : base(itemView)
    {
        text_message_recv = itemView.FindViewById<BubbleTextView>(Resource.Id.text_message);
    }
}

class ViewHolderRecv : RecyclerView.ViewHolder
{
    public ImageView adressee_msg { get; set; }
    public BubbleTextView text_message_recv { get; set; }
    public ViewHolderRecv(View itemView) : base(itemView)
    {
        adressee_msg = itemView.FindViewById<ImageView>(Resource.Id.adressee_msg);
        text_message_recv = itemView.FindViewById<BubbleTextView>(Resource.Id.text_message);
    }
}