using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITroopersInventory : UIInventory 
{

	override public void Awake() {
		base.Awake ();
		//list.AddRange (contentParent.GetComponentsInChildren<UITrooperElement>());
		uiElementPrefab = (UITrooperElement)  Resources.Load<UITrooperElement> ("ui/TrooperUIElement");
		//EvHandler.RegisterEv (UIEvent.SHOW_ARMY_INV, show); 		//EvHandler.RegisterEv (UIEvent.HIDE_ARMY_INV, hide);
		EvHandler.RegisterEv (UIEvent.UPDATE_INV, update);
		update ();
	}

	protected override void update () {
		setRange( pShip.getAllTroopers () );
	}

	protected override void setRange (Troopers[] ts)
	{
		base.setRange (ts);
	}

}
