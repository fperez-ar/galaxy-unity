using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIGeneticInventory : UIInventory<GeneticTrait>
{
	
	public override void Awake () {
		base.Awake ();
		uiElementPrefab = (UIGeneticElement)  Resources.Load<UIGeneticElement> ("ui/GeneticUIElement");
		clear ();
		EvHandler.RegisterEv (UIEvent.UPDATE_INV, update);
		EvHandler.RegisterEv (GameEvent.ADD_GENE_MAT, TurnOff);
		EvHandler.RegisterEv (GameEvent.RM_GENE_MAT, TurnOn);
	}

	public override void toggle () {
		base.toggle ();
	}

	override protected void update () {
		setRange( pShip.getAllGenes () );
	}

	void TurnOff(object oGene) {
		GeneticTrait g = (GeneticTrait)oGene;
		objUiMap [g].gameObject.SetActive (false);
	}

	void TurnOn(object oCounter) {
		GeneticTrait g = (GeneticTrait)oCounter;
		objUiMap [g].gameObject.SetActive (true);
	}
}
