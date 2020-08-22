package crc6481974efe9a97b2b8;


public class ViewHolderNotRecv
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ViewHolderNotRecv, App5", ViewHolderNotRecv.class, __md_methods);
	}


	public ViewHolderNotRecv (android.view.View p0)
	{
		super (p0);
		if (getClass () == ViewHolderNotRecv.class)
			mono.android.TypeManager.Activate ("ViewHolderNotRecv, App5", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
