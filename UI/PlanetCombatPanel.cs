using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCombatPanel : UIInventory<Troopers> {

	public int troopStr = 0;

	public void awake(){
		base.Awake ();
		uiElementPrefab = (UITrooperCbtElement)  Resources.Load<UITrooperCbtElement> ("ui/UITrooperCbtElement");
		objUiMap = new Dictionary<Troopers, UIListElement<Troopers>> (list.Count);
		//EvHandler.RegisterEv(GameEvent.ADD_CBT_FF, add);
		//EvHandler.RegisterEv(GameEvent.RM_CBT_FF, remove);
		clear();
	}

	void add(object o)
	{
		if ( GameMode.isMode(GameState.COMBAT)) {
			set((Troopers) o);
		}
	}

	void remove(object o)
	{
		if ( GameMode.isMode(GameState.COMBAT)) {
			unset((Troopers) o);
		}
	}

	void set(Troopers t)
	{
		UIListElement<Troopers> le = findFirstDisabled();
		le.set(t);
		objUiMap.Add(t, le);
		print("adding to CBT panel "+t.name);
	}

	void unset(Troopers t)
	{
		UIListElement<Troopers> le = find(t);
		le.unset();
		objUiMap.Remove(t);
		print("removing from CBT panel "+t.name);
	}

	protected override void update () {

	}

	public string[] getTroopsNames(){
		int len = list.Count;
		string[] trps = new string[len];
		for (int i = 0; i < len; i++) {
			UnityEngine.Debug.LogWarning ("FIX ME");//trps [i] = elemsLi [i].counter.getName ();
			if ( list[i].isActiveAndEnabled ) {
				trps [i] = list[i].getRefObj.name;
			}
		}

		return trps;
	}

	private UIListElement<Troopers> find(Troopers t)
	{
		for (int i = 0; i < list.Count; i++) {
			if (list[i].getRefObj != null && list[i].getRefObj.Equals(t)) {
				return list[i];
			}
		}
		Debug.LogError("trooper "+t+" not found in player combat panel.");
		return null;
	}

	private UIListElement<Troopers> findFirstDisabled()
	{
		for (int i = 0; i < list.Count; i++) {
			if (!list[i].gameObject.activeSelf )//&& list[i].getRefObj == null
			{
				return list[i];
			}
		}
		int newIndx = list.Count;
		increase();
		return list[newIndx];
	}

	protected override void clear ()
	{
		for (int i = 0; i < list.Count; i++) {
			list [i].gameObject.SetActive (false);
		}
		objUiMap.Clear();
		index = 0;
	}

	protected override void OnBeforeShow () {
		base.OnBeforeShow ();
	}

}
