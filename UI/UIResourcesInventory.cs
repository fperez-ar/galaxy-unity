﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResourcesInventory : UIInventory {


	public override void Awake () {
		base.Awake ();
		uiElementPrefab = (UIResourceElement)  Resources.Load<UIResourceElement> ("ui/GeneticUIElement");
		EvHandler.RegisterEv (UIEvent.UPDATE_INV, update);
	}

	override protected void update () {
		setRange( pShip.getAllResources () );
	}

	protected override void setRange (ResourceBase[] rs)
	{
		base.setRange (rs);
	}

}
