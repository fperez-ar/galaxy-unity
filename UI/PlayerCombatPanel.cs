using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void onCounterInteraction (UICounterBase c);
public class PlayerCombatPanel : MonoBehaviour {

	public int troopStr = 0;
	public Transform contentParent;
	protected List< UIListElement<UITrooperElement> > list = new List<UIListElement<UITrooperElement>> ();

	void Awake(){
		list.AddRange (contentParent.GetComponentsInChildren<UIListElement<UITrooperElement>> ());
		clear();
	}

	void add(object o) {
		//set next element in the list
		//move index backwards
	}

	void remove(object o) {
		//unset last (current) element in the list
		//move index backwards
	}

	/*
	private BoxCollider2D container;
	private List<PlyCounter> elemsLi = new List<PlyCounter>();
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
		}

	}

	public void addCounter(UICounterBase counterBase){

		PlyCounter bCounter = (PlyCounter) counterBase;
		bCounter.draggable = false;
		UIHandlerHelper.Reparent (bCounter.transform, this.transform);
		elemsLi.Add (bCounter);

		if (onAdd != null){
			onAdd (bCounter);
		}
	}

	public void removeCounter(UICounterBase counterBase){
		PlyCounter bCounter = (PlyCounter) counterBase;
		bCounter.draggable = true;
		elemsLi.Remove(bCounter);
		UIHandlerHelper.ParentRestore (bCounter.transform);

		if (onRemove != null){
			onRemove(bCounter);
		}
	}

	void Round( Vector3 v, float roundFactor = 10) {
		v.x = v.x % roundFactor;
		v.y = v.y % roundFactor;
	}
	*/

	public string[] getTroopsNames(){
		int len = list.Count;
		string[] trps = new string[len];
		for (int i = 0; i < len; i++) {
			UnityEngine.Debug.LogWarning ("FIX ME");//trps [i] = elemsLi [i].counter.getName ();
			if ( list[i].activeSelf ) {
				trps [i] = list[i].getRefObj.name;
			}
		}

		return trps;
	}

	protected virtual void clear ()
	{
		for (int i = 0; i < list.Count; i++) {
			list [i].gameObject.SetActive (false);
		}
	}
}
