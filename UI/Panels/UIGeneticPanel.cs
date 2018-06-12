using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGeneticPanel : MonoBehaviour {

	public int maxGenes = 5;
	private int index = 0;
	private new Collider collider;
	private List<GeneticTrait> genes;
	private List<UICounterBase> counters;
	public SimpleDelegate onAdd;
	public SimpleDelegate onRemove;

	void Awake(){
		getComponents ();
		EvHandler.RegisterEv (GameEvent.ADD_GENE_MAT, add);
		EvHandler.RegisterEv (GameEvent.RM_GENE_MAT, remv);
	}

	void getComponents() {
		index = 0;
		counters = new List<UICounterBase> (GetComponentsInChildren <UICounterBase> ());
		genes = new List<GeneticTrait> (counters.Count);
		for (int i = 0; i < counters.Count; i++) { counters [i].position = i; }
	}

	void add(object oCounter) {
		GeneticTrait g = (GeneticTrait) oCounter;
		//print ("attempting to add " +g.name+" on "+index);
		if ( enabled && !genes.Contains (g) ) {
			set (g);
			if (onAdd != null) { onAdd(); }
		}
	}

	void remv(object oGene) {
		GeneticTrait g = (GeneticTrait) oGene;

		if (enabled && genes.Contains (g) ) {
			unset (g);
			if (onRemove != null) { onRemove(); }
		}
		
	}

	void set(GeneticTrait g) {
		//set counter base with gene object
		//set gene counter gameobject active
		//print ("Add " +g.name+" on "+index);
		if ( counters [index].getReference () != null) {
			GeneticTrait gt = (GeneticTrait)counters [index].getReference ();
			//print ("Removing" +gt.name+" on "+counters [index].position);
			genes.Remove (gt);
		}
		genes.Add (g);
		counters [index].set (g);
		index = ++index % maxGenes;
	}

	void unset(GeneticTrait g) {
		//set gene counter gameobject deactivate
		genes.Remove (g);
		index = findCounter (g);
		counters [index].gameObject.SetActive (false);
		//print ("Removing "+g.name+" on "+index);
	}

	public void Clear() {
		if (genes != null) genes.Clear ();
		for (int i = 0; i < counters.Count; i++) {
			if (counters [i].isActiveAndEnabled) {
				counters [i].reset ();
			}
		}
	}

	public int findCounter(GeneticTrait g) {
		for (int i = 0; i < counters.Count; i++) 
		{
			if (counters [i].getReference ().Equals (g)) {
				return counters [i].position;
			}
		}
		return index;
	}

	public GeneticTrait[] getGenes(){	
		return genes.ToArray ();
	}
}
