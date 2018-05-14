using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopersInventory : MonoBehaviour 
{
	
	public Transform contentParent;
	public PlayerShip pShip;
	private bool shown = false;
	private Animator anim;
	private int index = 0;
	private int lenIncrease = 10;
	private TrooperUIElement trooperUIElemPrefab;
	private List<TrooperUIElement> list = new List<TrooperUIElement>();

	void Awake() {
		anim = GetComponent<Animator>();
		list.AddRange (contentParent.GetComponentsInChildren<TrooperUIElement>());
		trooperUIElemPrefab = (TrooperUIElement)  Resources.Load<TrooperUIElement> ("ui/TrooperUIElement");
		EvHandler.RegisterEv (UIEvent.SHOW_ARMY_INV, show);
		EvHandler.RegisterEv (UIEvent.HIDE_ARMY_INV, hide);
		EvHandler.RegisterEv (UIEvent.UPD_ARMY_INV, update);
		update ();
	}

	public void toggle() {
		if ( shown ){
			hide ();
		}else{
			show ();
		}
		shown = !shown;
	}

	void show() {
		update ();
		anim.Play ("in");
	}

	void hide() {
		anim.Play ("out");
	}

	void update() {
		index = 0;
		addRange( pShip.getAllTroopers () );
	}

	void add(Troopers t) {
		if (index > list.Count-1) insert ();

		list [index].set (t);
		index++;
	}

	void addRange(Troopers[] ts) {
		for (int i = 0; i < ts.Length; i++) {
			add (ts [i]);
		}
	}

	void insert() {
		TrooperUIElement[] ts = new TrooperUIElement[10];
		for (int i = 0; i < lenIncrease; i++) {
			ts[i] = (TrooperUIElement) GameObject.Instantiate (trooperUIElemPrefab, contentParent);
			ts [i].gameObject.SetActive (false);
		}
		list.AddRange (ts);
	}

}
