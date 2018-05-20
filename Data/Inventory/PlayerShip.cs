using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour {

	public Species dominantSpecies = new Species();

	private int resourcePoints = -1;
	public ResourceInventory resources = new ResourceInventory();

	void Awake() {
		SaveManager.Load (this);
		/*
		resources.add ( new ResourceBase("Water", 25) );
		dominantSpecies.addTroops (BaseVals.BaseTroops, 50);
		dominantSpecies.addTroops (new Troopers ("Marines", 10){morale = 50f,  offensiveCap = 42f, defensiveCap = 12f} );
		dominantSpecies.addTroops ("Space robots", 25);
		dominantSpecies.addTroops ("Stellar vessels", 2);
		*/
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

	public void addTroopers(Troopers[] ts) {
		dominantSpecies.man.addRange (ts);
	}


	public GeneticTrait[] getAllGenes() {
		return dominantSpecies.gen.getArray ();
	}

	public void addGenes(GeneticTrait[] gs){
		dominantSpecies.gen.addRange (gs);
	}

	public ResourceBase[] getAllResources() {
		return resources.getArray ();
	}

	public void addResources(ResourceBase[] rs ) {
		resources.addRange (rs);
	}

	public int getPopulation() {
		return dominantSpecies.population;
	}

}
