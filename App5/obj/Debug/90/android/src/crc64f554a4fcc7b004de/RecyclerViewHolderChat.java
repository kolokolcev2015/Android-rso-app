package crc64f554a4fcc7b004de;


public class RecyclerViewHolderChat
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Auth.Helper.RecyclerViewHolderChat, App5", RecyclerViewHolderChat.class, __md_methods);
	}


	public RecyclerViewHolderChat (android.view.View p0)
	{
		super (p0);
		if (getClass () == RecyclerViewHolderChat.class)
			mono.android.TypeManager.Activate ("Auth.Helper.RecyclerViewHolderChat, App5", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
