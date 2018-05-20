using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIGeneticElement : UIListElement, IPointerClickHandler {

	private GeneticTrait refGene;
	public Text geneName;
	public Text offensiveMod;
	public Text defensiveMod;
	public Text adaptabilityMod;

	public override void set(GeneticTrait g) {
		this.gameObject.SetActive (true);
		geneName.text = g.name;
		offensiveMod.text = g.offensiveCapModificator.ToString ();
		defensiveMod.text = g.defensiveCapModificator.ToString ();
		adaptabilityMod.text = g.adaptability.ToString ();
		refGene = g;
	}


	public void OnPointerClick(PointerEventData eventData) {
		Debug.Log ("\tAdd Gene element "+refGene.name);
		EvHandler.ExecuteEv (GameEvent.ADD_GENE_MAT, refGene);
	}

	public virtual void OnPointerEnter(PointerEventData eventData) {
	}

	public void OnPointerExit(PointerEventData eventData) {
	}
}
