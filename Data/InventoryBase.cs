using System;
using System.Text;
using System.Collections.Generic;

[System.Serializable]
public abstract class InventoryBase<T> where T : ResourceBase  {

	protected Dictionary<string, T> storage;

	public InventoryBase ()
	{
		storage = new Dictionary<string, T>();
	}

	public virtual void add(string itemName, int itemQuantity){
		if (!storage.ContainsKey (itemName)) {
			
			T t = Activator.CreateInstance <T>();
			t.name = itemName;
			t.quantity = itemQuantity;
			storage.Add (itemName, t);
		}

		storage [itemName].quantity += itemQuantity;
	}

	public virtual void add(T t){
		if (!storage.ContainsKey (t.name)) {
			storage.Add (t.name, t);
		} else {
			storage [t.name].quantity += t.quantity;
		}
	}

	public virtual void addRange(T[] tArray){
		for (int i = 0; i < tArray.Length; i++) {
			T t = tArray [i];
			if (!storage.ContainsKey (t.name)) {
				storage.Add (t.name, t);
			} else {
				storage [t.name].quantity += t.quantity;
			}
			
		}
	}

	public virtual void addRange(List<T> tList){
		for (int i = 0; i < tList.Count; i++) {
			T t = tList [i];
			if (!storage.ContainsKey (t.name)) {
				storage.Add (t.name, t);
			} else {
				storage [t.name].quantity += t.quantity;
			}
		}

	}

	public virtual int getQuantity(string itemName){
		return storage [itemName].quantity;
	}

	public virtual T[] getArray(){
		return  new List<T> (storage.Values).ToArray ();
	}

	public virtual List<T> getList(){
		return new List<T> (storage.Values);
	}

	public override string ToString ()
	{
		//return string.Format ("[GeneticTraitInventory]");
		StringBuilder sb = new StringBuilder();
		foreach (T g in storage.Values) {
			sb.AppendLine ( g.getStringQuantity (':') );
		}

		return sb.ToString ();
	}


	public virtual void remove(string itemName){
		if (storage.ContainsKey (itemName)) {
			storage.Remove (itemName);
		}
	}

	public virtual void clear(){
		storage.Clear ();
	}
}
