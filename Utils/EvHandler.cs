using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SimpleDelegate ();
public delegate void ComplexDelegate (object args);

public static class EvHandler
{
	
	private static Dictionary<string, SimpleDelegate> simpleEvs
	= new Dictionary<string, SimpleDelegate> ();

	private static Dictionary<string, ComplexDelegate> complexEvs
	= new Dictionary<string, ComplexDelegate> ();


	//TODO: work out how to add events dependent on the gamestate

	public static void RegisterEv (UIEvent ev, SimpleDelegate del, bool additive = true)
	{
		RegisterEv (ev.ToString (), del, additive);
	}


	public static void RegisterEv (UIEvent ev, ComplexDelegate del, bool additive = true)
	{
		RegisterEv (ev.ToString (), del, additive);
	}


	public static void RegisterEv (GameEvent ev, SimpleDelegate del, bool additive = true)
	{
		RegisterEv (ev.ToString (), del, additive);
	}

	public static void RegisterEv (GameEvent ev, ComplexDelegate del, bool additive = true)
	{
		RegisterEv (ev.ToString (), del, additive);
	}

	public static void RegisterEv (string ev, SimpleDelegate del, bool additive = true)
	{
		if (simpleEvs.ContainsKey (ev)) {
			if (additive) {
				//Debug.Log (ev+" registered.");
				simpleEvs [ev] += del;
			} else {
				simpleEvs [ev] = del;
			}
		} else {
			simpleEvs.Add (ev, del);
		}
	}

	public static void RegisterEv (string ev, ComplexDelegate del, bool additive = true)
	{
		if (complexEvs.ContainsKey (ev)) {
			if (additive) {
				complexEvs [ev] += del;
			} else {
				complexEvs [ev] = del;
			}
		} else {
			complexEvs.Add (ev, del);
		}
	}

	public static void ExecuteEv (UIEvent ev)
	{
		ExecuteEv (ev.ToString ());
	}

	public static void ExecuteEv (GameEvent ev)
	{
		ExecuteEv (ev.ToString ());
	}

	public static void ExecuteEv (string ev)
	{
		if (simpleEvs.ContainsKey (ev)) {
			//Debug.Log ("Executing Simple event "+ev);
			simpleEvs [ev] ();
		} else {
			Debug.LogWarning (ev + " attempted execution but was not registered.");
		}
	}

	public static void ExecuteEv (UIEvent ev, object args)
	{
		ExecuteEv (ev.ToString (), args);
	}

	public static void ExecuteEv (GameEvent ev, object args)
	{
		ExecuteEv (ev.ToString (), args);
	}

	public static void ExecuteEv (string ev, object args)
	{
		if (complexEvs.ContainsKey (ev)) {
			//Debug.Log ("Executing Complex event "+ev);
			complexEvs [ev] (args);
		} else {
			Debug.LogWarning (ev + " attempted execution but was not registered.");
		}
	}


	public static void UnregisterEv (UIEvent ev)
	{
		complexEvs.Remove (ev.ToString ());
	}

	public static void UnregisterEv (GameEvent ev)
	{
		complexEvs.Remove (ev.ToString ());
	}

	public static void UnregisterEv (string ev)
	{
		complexEvs.Remove (ev);
	}

	public static void Clear ()
	{
		complexEvs.Clear ();
	}

}

//UI Events
public enum UIEvent
{
	ENTER_PLT,

	SHOW_PLANET_INFO,
	SHOW_SUN_INFO,
	SHOW_INV_INFO,
	SHOW_COMBAT_PANEL,
	HIDE_COMBAT_PANEL,
	UPDATE_COMBAT_INFO,
	UPDATE_INV,
	HIDE_TOOLTIP,
	SHOW_TOOLTIP,
	SHOW_AUTOFADE_TOOLTIP,
	ANIM_IDLE,
	ANIM_CON,
	COUNTER_DROP,
}

//Game Logic events
public enum GameEvent
{
	ORBIT_PLT,
	//planet interaction
	PROBE_PLT, MINE_PLT, ATTACK_PLT, INVADE_PLT,
	//combat
	PREPARATION_PHASE, COMBAT_CALCULATION, RESOLVE_CBT_PHASE,
	EXPLORE_PHASE, UI_PHASE,
	//troop creation
	ADD_GENE_MAT, RM_GENE_MAT,
	ADD_CBT_FF,	RM_CBT_FF,
	//combat resolution
	AWON, ALOST,

}

