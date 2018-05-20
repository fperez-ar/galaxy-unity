using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIInventory : MonoBehaviour {
	
	public PlayerShip pShip;
	public Transform contentParent;
	
	protected Animation anim;
	protected bool shown = false;
	protected UIListElement uiElementPrefab;
	protected List<UIListElement> list = new List<UIListElement>();

	public virtual void Awake() {
		anim = GetComponent<Animation>();
		list.AddRange (contentParent.GetComponentsInChildren<UIListElement>());
		clear ();
	}

	public virtual void toggle() {
		if ( shown ){
			hide ();
		}else{
			show ();
		}
	}

	protected virtual void show() {
		shown = true;
		update ();
		anim.Play ("in");
	}

	protected virtual void hide() {
		shown = false;
		anim.Play ("out");
	}

	protected virtual void update () {
		clear ();

	}

	protected virtual void clear(){
		for (int i = 0; i < list.Count; i++) {
			list [i].gameObject.SetActive (false);
		}
	}


	protected virtual void set(ResourceBase r, int index) {
		list [index].set (r);
	}

	protected virtual void set(GeneticTrait g, int index) {
		list [index].set (g);
	}

	protected virtual void set(Troopers t, int index) {
		list [index].set (t);
	}

	protected virtual void setRange(ResourceBase[] elems) {
		if (elems.Length > list.Count) { increase (elems.Length - list.Count); }
		for (int i = 0; i < list.Count; i++) {
			if ( i < elems.Length )
				list [i].set (elems [i]);
			else
				list [i].gameObject.SetActive (false);
		}
	}

	protected virtual void setRange(Troopers[] elems) {
		if (elems.Length > list.Count) { increase (elems.Length - list.Count); }
		for (int i = 0; i < list.Count; i++) {
			if ( i < elems.Length )
				list [i].set (elems [i]);
			else
				list [i].gameObject.SetActive (false);
		}
	}

	protected virtual void setRange(GeneticTrait[] elems) {
		if (elems.Length > list.Count) { increase (elems.Length - list.Count); }
		for (int i = 0; i < list.Count; i++) {
			if ( i < elems.Length )
				list [i].set (elems [i]);
			else
				list [i].gameObject.SetActive (false);
		}
	}

	protected virtual void increase(int increase = 10) {
		UIListElement[] ts = new UIListElement[increase];
		for (int i = 0; i < increase; i++) {
			ts [i] = (UIListElement) GameObject.Instantiate (uiElementPrefab, contentParent);
			ts [i].name = uiElementPrefab.name + " " + i;
			ts [i].gameObject.SetActive (false);
		}
		list.AddRange (ts);
	}

}
