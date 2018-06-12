using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerShip : MonoBehaviour {

	public Species dominantSpecies = new Species();

	private int resourcePoints = -1;
	public ResourceInventory resources = new ResourceInventory();

	public int getResourcePoints() {
		return resourcePoints / BaseVals.maxResource;
	}

	public Troopers getTroop(string troopsName) {
		return dominantSpecies.getTroops (troopsName);
	}

	public Troopers[] getAllTroopers() {
		return dominantSpecies.man.getArray ();
	}

	public void addTrooper(Troopers t) {
		dominantSpecies.man.add (t);
	}

	public void addTroopers(Troopers[] ts) {
		dominantSpecies.man.addRange (ts);
	}

	public void removeTrooper(object refTrooper) {
		Troopers t = (Troopers)refTrooper;
		dominantSpecies.population += t.manpower;
		dominantSpecies.man.remove(t.name);
	}

	public GeneticTrait[] getAllGenes() {
		return dominantSpecies.gen.getArray ();
	}

	public void addGenes(GeneticTrait[] gs){
		dominantSpecies.gen.addRange (gs);
	}


	public ResourceBase getResource(string resourceName) {
		return resources.get (resourceName);
	}

	public void addResource(ResourceBase r ) {
		resources.add (r);
	}
	public void addResources(ResourceBase[] rs ) {
		resources.addRange (rs);
	}

	public void removeResource(string resourceName) {
		resources.remove (resourceName);
	}

	public void removeResource(ResourceBase rs ) {
		resources.remove (rs.name);
	}

	public bool hasResources(ResourceBase res) {
		return resources.contains (res);
	}

	public bool hasResources(string resourceName) {
		return resources.contains (resourceName);
	}

	public bool hasResources(string resourceName, int quantity) {
		return resources.getQuantity (resourceName) >= quantity ? true : false;
	}

	public void modifyResource(string resourceName, int byQuantity) {
		resources.modify(resourceName, byQuantity);
	}

	public ResourceBase[] getAllResources() {
		return resources.getArray ();
	}

	public int getPopulation() {
		return dominantSpecies.population;
	}

}
