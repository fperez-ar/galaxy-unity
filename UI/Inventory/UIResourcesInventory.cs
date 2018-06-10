using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResourcesInventory : UIInventory<ResourceBase>, IToggleable {


	public override void Awake () {
		base.Awake ();
		clear();
		uiElementPrefab = (UIResourceElement)  Resources.Load<UIResourceElement> ("ui/GeneticUIElement");
		objUiMap = new Dictionary<ResourceBase, UIListElement<ResourceBase>> (list.Count);
		EvHandler.RegisterEv (UIEvent.UPDATE_INV, update);
	}

	override protected void update () {
		setRange( pShip.getAllResources () );
	}

	protected override void setRange (ResourceBase[] rs) {
		base.setRange (rs);
	}


	protected override void OnBeforeShow () {
		base.OnBeforeShow ();
	}
}
