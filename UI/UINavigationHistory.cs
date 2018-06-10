using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINavigationHistory : UIInventory<CelestialBody>
{
	private HistoryQueueBase<CelestialBody> navigationHistory;

	override public void Awake ()
	{
		base.Awake ();
		uiElementPrefab = (UICelestialBodyElement)Resources.Load<UICelestialBodyElement> ("ui/UICelestialBodyElement");
		navigationHistory = new HistoryQueueBase<CelestialBody> ();
		clear();
		EvHandler.RegisterEv (GameEvent.ORBIT_PLT, addPlanet);
		EvHandler.RegisterEv (UIEvent.UPDATE_INV, update);
	}

	public void addPlanet (object oBody)
	{
		navigationHistory.add ((CelestialBody)oBody);
		EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
	}

	public bool isPlanetDiscovered (CelestialBody body)
	{
		return navigationHistory.contains (body);
	}

	protected override void update ()
	{
		setRange (navigationHistory.getArray ());
	}

	protected override void setRange (CelestialBody[] elems)
	{
		if (elems.Length > list.Count) {
			increase (elems.Length - list.Count);
		}
		for (int i = 0; i < list.Count; i++) {
			if (i < elems.Length) {
				list [i].set (elems [i]);
			} else {
				list [i].unset ();
			}
		}
	}

}
