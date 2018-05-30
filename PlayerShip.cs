using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerShip : MonoBehaviour {

	public Species dominantSpecies = new Species();

	private int resourcePoints = -1;
	public ResourceInventory resources = new ResourceInventory();

	void Awake() {
		SaveManager.Load (this);
		EvHandler.RegisterEv (GameEvent.RM_CBT_FF, removeTrooper, false);
	}


	public int getResourcePoints() {
		return resourcePoints / BaseVals.maxResource;
	}


	public Troopers getTroop(string troopsName) {
		return dominantSpecies.getTroops (troopsName);
	}

	public Troopers[] getAllTroopers() {
		return dominantSpecies.man.getArray ();
	}

	public void addTroopers(Troopers[] ts) {
		dominantSpecies.man.addRange (ts);
		EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
	}

	public void removeTrooper(object refTrooper) {
		Troopers t = (Troopers)refTrooper;
		dominantSpecies.population += t.manpower;
		dominantSpecies.man.remove(t.name);
		EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
	}

	public GeneticTrait[] getAllGenes() {
		return dominantSpecies.gen.getArray ();
	}

	public void addGenes(GeneticTrait[] gs){
		dominantSpecies.gen.addRange (gs);
		EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
	}

	public ResourceBase[] getAllResources() {
		return resources.getArray ();
	}

	public ResourceBase getResource(string resourceName) {
		return resources.get (resourceName);
	}

	public void addResources(ResourceBase[] rs ) {
		resources.addRange (rs);
		EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
	}

	public void removeResource(string resourceName) {
		resources.remove (resourceName);
		EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
	}

	public void removeResource(ResourceBase rs ) {
		resources.remove (rs.name);
		EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
	}

	public bool hasResources(string resourceName) {
		return resources.contains (resourceName);
	}

	public void modifyResource(string resourceName, int byQuantity) {
		resources.modify(resourceName, byQuantity);
		EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
	}

	public int getPopulation() {
		return dominantSpecies.population;
	}

}
