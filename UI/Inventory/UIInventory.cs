using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIInventory<T> : MonoBehaviour, IToggleable where T : class
{
	//TODO: Add states animating, ready to avoid activating buttons when animating
	public PlayerShip pShip;
	public Transform contentParent;
	public event SimpleDelegate BeforeShow;
	public bool shown {
		get { return mShown; }
		set { mShown = value; }
	}
	protected bool mShown = false;
	protected Animation anim;
	protected UIListElement<T> uiElementPrefab;
	protected List<UIListElement<T>> list = new List<UIListElement<T>> ();
	protected Dictionary<T, UIListElement<T>> objUiMap;

	public virtual void Awake ()
	{
		anim = GetComponent<Animation> ();
		list.AddRange (contentParent.GetComponentsInChildren<UIListElement<T>> ());
	}

	public virtual void toggle ()
	{
		if (mShown) {
			hide ();
		} else {
			OnBeforeShow ();
			show ();
		}
	}

	protected virtual void show ()
	{
		mShown = true;
		update ();
		anim.CrossFade ("in");
	}

	protected virtual void hide ()
	{
		mShown = false;
		anim.CrossFade ("out");
		//GameMode.setMode(GameState.NAVEGATION);
	}

	protected virtual void update ()
	{
		clear ();
	}

	protected virtual void clear ()
	{
		for (int i = 0; i < list.Count; i++) {
			list [i].gameObject.SetActive (false);
		}
	}

	protected virtual void setRange (T[] elems)
	{
		if (elems.Length > list.Count) {
			increase (elems.Length - list.Count);
		}
		objUiMap.Clear ();
		for (int i = 0; i < list.Count; i++) {
			if (i < elems.Length) {
				print("Setting #["+i+"]:"+elems [i]);
				list [i].set (elems [i]);
				objUiMap.Add (elems [i], list [i]);
			} else {
				list [i].unset ();
			}
		}
	}

	protected virtual void increase (int increase = 10)
	{
		UIListElement<T>[] ts = new UIListElement<T>[increase];
		for (int i = 0; i < increase; i++) {
			ts [i] = (UIListElement<T>)GameObject.Instantiate (uiElementPrefab, contentParent);
			ts [i].name = uiElementPrefab.name + " " + i;
			ts [i].gameObject.SetActive (false);
		}
		list.AddRange (ts);
	}

	protected virtual void remove (UIListElement<T> uiElemList)
	{
		if (list.Contains (uiElemList)) {
			list.Remove (uiElemList);
		}
		//also remove from obj-ui map
		foreach (KeyValuePair<T, UIListElement<T>> pair in objUiMap)
		{
		    if (pair.Value.Equals(uiElemList)) {
				objUiMap.Remove(pair.Key);
		    }
		}
	}

	protected virtual void OnBeforeShow ()
	{
		if (BeforeShow != null) {
			BeforeShow ();
		}
	}
}
