using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITroopersInventory : UIInventory<Troopers>, IToggleable {

	public PlayerShip pShip;
	override public void Awake()
	{
		base.Awake ();
		uiElementPrefab = (UITrooperElement)  Resources.Load<UITrooperElement> ("ui/TrooperUIElement");
		objUiMap = new Dictionary<Troopers, UIListElement<Troopers>> (list.Count);
		EvHandler.RegisterEv (UIEvent.SHOW_COMBAT_PANEL, show);
		EvHandler.RegisterEv (UIEvent.UPDATE_INV, update);
		EvHandler.RegisterEv (GameEvent.DISBAND_CBT_FF, disbanding);
		EvHandler.RegisterEv (GameEvent.ADD_CBT_FF, removeFromInventory);
		EvHandler.RegisterEv (GameEvent.RM_CBT_FF, returnToInventory);
		#if UNITY_EDITOR
		 EvHandler.RegisterEv("debuginv", debug);
		#endif
		update ();
	}

	protected override void update ()
	{
		setRange(pShip.getAllTroopers ());
	}

	#if UNITY_EDITOR
	void debug()
	{
		Troopers[] ts = pShip.getAllTroopers ();
		print("Current inventory");
		for (int i = 0; i < ts.Length; i++) {
			print("* ["+i+"]"+ts[i].name);
		}
	}
	#endif

	protected override void OnBeforeShow ()
	{
		base.OnBeforeShow ();
	}

	//when you add to the combat panel you remove it from the inventory
	void disbanding(object oTroopers)
	{
		Troopers t = (Troopers) oTroopers;
		print("Disbanding from inv "+t);
		pShip.removeTrooper(t);
		update();
	}

	void removeFromInventory(object oTroopers)
	{
		Troopers t = (Troopers) oTroopers;
		print("Removing from inv "+t.name);
		pShip.removeTrooper(t);
		update();
	}

	void returnToInventory(object oTroopers)
	{
		Troopers t = (Troopers) oTroopers;
		print("adding to inv "+t.name);
		pShip.addTrooper(t);
		update();
	}
}
