using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIGeneticInventory : UIInventory 
{
	public override void Awake () {
		base.Awake ();
		//uiElementPrefab = (UIGeneticElement)  Resources.Load<UIGeneticElement> ("ui/GeneticUIElement");
		clear ();
		EvHandler.RegisterEv (UIEvent.UPDATE_INV, update);
	}

	override protected void update () {
		setRange( pShip.getAllGenes () );
	}


}
