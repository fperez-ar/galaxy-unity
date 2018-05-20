using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGeneticPanel : MonoBehaviour {

	public int maxGenes = 3, idx = 0;
	private List<GeneticTrait> genes;
	private List<UICounterBase> geneCounters;
	public SimpleDelegate onAdd;
	public SimpleDelegate onRemove;

	void Awake(){
		genes = new List<GeneticTrait> (maxGenes);
		geneCounters = new List<UICounterBase>(GetComponentsInChildren <UICounterBase> ());

		EvHandler.RegisterEv (GameEvent.ADD_GENE_MAT, add);
		EvHandler.RegisterEv (GameEvent.RM_GENE_MAT, remv);
	}

	void add(object oGene) {
		GeneticTrait g = (GeneticTrait) oGene;
		if (enabled && !genes.Contains (g) ) {
			genes.Add (g);
			set (g);
			if (onAdd != null) { onAdd(); }
		}
	}

	void remv(object oGene) {
		GeneticTrait g = (GeneticTrait) oGene;
		print ("removing "+g); 
		if (enabled && !genes.Contains (g) ) {
			genes.Remove(g);
			unset (g);
			if (onRemove != null) { onRemove(); }
		}
		
	}

	void set(GeneticTrait g) {
		geneCounters [idx].set (g);
		geneCounters [idx].gameObject.SetActive (true);
		idx = ++idx % maxGenes;
	}

	void unset(GeneticTrait g) {
		geneCounters [idx].gameObject.SetActive (false);
		idx--;
	}

	public void Clear() {
		if (genes != null) genes.Clear ();
		geneCounters.ForEach (delegate(UICounterBase obj) { obj.gameObject.SetActive (false); });
	}

	public GeneticTrait[] getGenes(){	
		return genes.ToArray ();
	}
}
