using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour {

	public Species dominantSpecies = new Species();

	private int resourcePoints = -1;
	public ResourceInventory resources = new ResourceInventory();

	void Start() {
		
		resources.add ( new ResourceBase("Water", 25) );

		dominantSpecies.addTroops (BaseVals.BaseTroops, 50);
		var t1 = new Troopers ("Marines", 10){morale = 50f,  offensiveCap = 42f, defensiveCap = 12f};
		dominantSpecies.addTroops (t1);
		dominantSpecies.addTroops ("Space robots", 25);
		dominantSpecies.addTroops ("Stellar vessels", 2);

		//EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
	}


	public int getResourcePoints() {
		return resourcePoints / BaseVals.maxResource;
	}

	public Troopers getTroop() {
		return dominantSpecies.getTroops ();
	}

	public Troopers getTroop(string troopsName) {
		return dominantSpecies.getTroops (troopsName);
	}

	public Troopers[] getAllTroopers() {
		return dominantSpecies.man.getArray ();
	}

}
