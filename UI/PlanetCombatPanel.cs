using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCombatPanel : MonoBehaviour, ICounterablePanel {
	
	public onCounterInteraction onAdd;
	public onCounterInteraction onRemove;
	public int troopStr = 0;
	private List<CounterBase> elemsLi = new List<CounterBase>();

	void Awake(){
		
	}

	public void addCounter(CounterBase counterBase){
		if (elemsLi.Contains (counterBase)) return;
		UIHandlerHelper.Reparent (counterBase.transform, this.transform);
		elemsLi.Add (counterBase);
		troopStr += counterBase.counter.counterQuantity;

		if (onAdd != null){
			onAdd (counterBase);
		}
	}

	public void removeCounter(CounterBase counterBase){
		if (!elemsLi.Contains (counterBase)) return;
		UIHandlerHelper.ParentRestore (counterBase.transform);
		elemsLi.Remove(counterBase);
		troopStr -= counterBase.counter.counterQuantity;

		if (onRemove != null){
			onRemove(counterBase);
		}
	}

	public string[] getTroopsNames(){
		int len = elemsLi.Count;
		string[] trps = new string[len];
		for (int i = 0; i < len; i++) {
			trps[i] = elemsLi[i].counter.getName ();
		}

		return trps;
	}
}
