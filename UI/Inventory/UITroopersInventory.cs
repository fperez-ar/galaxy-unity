using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITroopersInventory : UIInventory<Troopers> {
	
	override public void Awake() {
		base.Awake ();
		uiElementPrefab = (UITrooperElement)  Resources.Load<UITrooperElement> ("ui/TrooperUIElement");

		EvHandler.RegisterEv (UIEvent.UPDATE_INV, update);
		update ();
	}

	protected override void update () {
		setRange( pShip.getAllTroopers () );
	}

	protected override void OnBeforeShow () {
		base.OnBeforeShow ();
	}

}
