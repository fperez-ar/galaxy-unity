using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIGeneticInventory : UIInventory<GeneticTrait>
{

	public override void Awake ()
	{
		base.Awake ();
		clear ();
		uiElementPrefab = (UIGeneticElement)Resources.Load<UIGeneticElement> ("ui/GeneticUIElement");
		objUiMap = new Dictionary<GeneticTrait, UIListElement<GeneticTrait>> (list.Count);
		EvHandler.RegisterEv (UIEvent.UPDATE_INV, update);
		EvHandler.RegisterEv (GameEvent.ADD_GENE_MAT, removeFromInventory);
		EvHandler.RegisterEv (GameEvent.RM_GENE_MAT, returnToInventory);
	}

	public override void toggle ()
	{
		base.toggle ();
	}

	override protected void update ()
	{
		setRange (pShip.getAllGenes ());
	}

	void removeFromInventory (object oGene)
	{
		GeneticTrait g = (GeneticTrait)oGene;
		objUiMap [g].gameObject.SetActive (false);
	}

	void returnToInventory (object oCounter)
	{
		GeneticTrait g = (GeneticTrait)oCounter;
		objUiMap [g].gameObject.SetActive (true);
	}
}
