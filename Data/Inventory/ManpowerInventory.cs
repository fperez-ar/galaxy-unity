using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ManpowerInventory : InventoryBase<Troopers> {
	
	public Troopers get(string troopName){
		if (storage.ContainsKey (troopName)){
			return storage [troopName];
		}
		return null;
	}

	public float getMorale(string troopName){
		if (storage.ContainsKey (troopName)){
			return storage [troopName].morale;
		}
		return 0;
	}


	public override string ToString ()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder ();
		foreach (var troop in storage.Values) {
			sb.AppendLine (troop.ToString ());
		}
		return sb.ToString ();
	}

}
