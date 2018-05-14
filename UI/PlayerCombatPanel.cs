using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void onCounterInteraction (CounterBase c);
public class PlayerCombatPanel : MonoBehaviour, ICounterablePanel {

	public onCounterInteraction onAdd;
	public onCounterInteraction onRemove;

	public int troopStr = 0;
	private BoxCollider2D container;
	private List<PlyCounter> elemsLi = new List<PlyCounter>();

	void Awake(){
		container = GetComponent<BoxCollider2D> ();
		EvHandler.RegisterEv (UIEvent.COUNTER_DROP, add);
	}

	void add(object oCounter){
		PlyCounter bCounter = (PlyCounter)oCounter;

		if (container.bounds.Contains (bCounter.transform.position)) {
			if (!elemsLi.Contains (bCounter)) {
				addCounter (bCounter);
			}
		} else {
			if (elemsLi.Contains (bCounter)) {
				removeCounter (bCounter);
			}

			bCounter.ForceResetPos ();
		}

	}


	public void addCounter(CounterBase counterBase){
		
		PlyCounter bCounter = (PlyCounter) counterBase;
		bCounter.draggable = false;
		UIHandlerHelper.Reparent (bCounter.transform, this.transform);
		elemsLi.Add (bCounter);
		troopStr += bCounter.counter.counterQuantity;

		if (onAdd != null){
			onAdd (bCounter);
		}
	}

	public void removeCounter(CounterBase counterBase){
		PlyCounter bCounter = (PlyCounter) counterBase;
		bCounter.draggable = true;
		elemsLi.Remove(bCounter);
		UIHandlerHelper.ParentRestore (bCounter.transform);
		troopStr -= bCounter.counter.counterQuantity;

		if (onRemove != null){
			onRemove(bCounter);
		}
	}

	public string[] getTroopsNames(){
		int len = elemsLi.Count;
		string[] trps = new string[len];
		for (int i = 0; i < len; i++) {
			trps [i] = elemsLi [i].counter.getName ();
		}

		return trps;
	}

	void Round( Vector3 v, float roundFactor = 10) {
		v.x = v.x % roundFactor;
		v.y = v.y % roundFactor;
	}
}
