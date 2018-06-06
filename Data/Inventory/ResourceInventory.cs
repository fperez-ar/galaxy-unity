using System.Collections.Generic;
using System.Text;

public class ResourceInventory : InventoryBase<ResourceBase>
{
	protected int resourceQuantity = 0;
	public int getResourcesQuantity { get {return resourceQuantity;} }

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

	public virtual void modify (string resourceName, int q, bool additive = true)
	{
		if (storage.ContainsKey (resourceName)) {
			storage [resourceName].quantity += q;
		}
	}
}
