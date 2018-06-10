using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCombatPanel : MonoBehaviour {

	public onCounterInteraction onAdd;
	public onCounterInteraction onRemove;
	public int troopStr = 0;
	private List<UICounterBase> elemsLi = new List<UICounterBase>();

	public void awake(){
		clear();
	}

	public void addCounter(UICounterBase counterBase){
		if (elemsLi.Contains (counterBase)) return;
		UIHandlerHelper.Reparent (counterBase.transform, this.transform);
		elemsLi.Add (counterBase);

		if (onAdd != null){
			onAdd (counterBase);
		}
	}

	public void removeCounter(UICounterBase counterBase){
		if (!elemsLi.Contains (counterBase)) return;
		UIHandlerHelper.ParentRestore (counterBase.transform);
		elemsLi.Remove(counterBase);

		if (onRemove != null){
			onRemove(counterBase);
		}
	}

	public string[] getTroopsNames(){
		int len = elemsLi.Count;
		string[] trps = new string[len];
		for (int i = 0; i < len; i++) {

		}

		return trps;
	}

	public void clear ()
	{
		for (int i = 0; i < elemsLi.Count; i++) {
			elemsLi [i].gameObject.SetActive (false);
		}
	}
}
