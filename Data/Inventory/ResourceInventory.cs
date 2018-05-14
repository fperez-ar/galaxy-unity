using System.Collections.Generic;
using System.Text;

public class ResourceInventory : InventoryBase<ResourceBase> {
	public int resourceQuantity = 0;

	public override void add (ResourceBase t)
	{
		base.add (t);
		resourceQuantity++;
	}

	public override void add (string itemName, int itemQuantity)
	{
		base.add (itemName, itemQuantity);
		resourceQuantity++;
	}

	public override void addRange (List<ResourceBase> tList)
	{
		base.addRange (tList);
		resourceQuantity += tList.Count;
	}

	public override void addRange (ResourceBase[] tArray)
	{
		base.addRange (tArray);
		resourceQuantity += tArray.Length;
	}

	public override void clear ()
	{
		base.clear ();
		resourceQuantity = 0;
	}

	public override void remove (string itemName)
	{
		base.remove (itemName);
		resourceQuantity--;
	}

/*
	private Dictionary<string, Resource> resources 
		= new Dictionary<string, Resource>();

	public void add(string resourceName, int quantity){
		if (!resources.ContainsKey (resourceName)) {
			resources.Add (resourceName, new Resource (resourceName, 0));
		}

		resources [resourceName].quantity += quantity;
	}

	public void add(Resource r){
		if (!resources.ContainsKey (r.name)) {
			resources.Add (r.name, r);
		} else {
			resources [r.name].quantity += r.quantity;
		}
	}

	public int getQuantity(string resourceName){
		return resources [resourceName].quantity;
	}

	public override string ToString ()
	{
		//return string.Format ("[ResourceInventory]");
		StringBuilder sb = new StringBuilder();
		foreach (Resource r in resources.Values) {
			sb.AppendLine (r.getStringQuantity (':'));
		}

		return sb.ToString ();
	}
*/
}
