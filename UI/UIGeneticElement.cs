using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIGeneticElement : UIListElement<GeneticTrait>, IPointerClickHandler {
	
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
		refObj = g;
	}


	public void OnPointerClick(PointerEventData eventData) {
		if (GameMode.isMode (GameState.TROOP_CREATION)) {
			//the troop creation panel is shown...
			EvHandler.ExecuteEv (GameEvent.ADD_GENE_MAT, refObj);
		}
	}

}
