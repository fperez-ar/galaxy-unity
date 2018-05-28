using UnityEngine;
using System;
using System.Collections.Generic;

public class DiFSM : iFSM
{
	protected Dictionary<int, State> dictState;
	private State current;
	private State next;
	
	private bool busy;

	public bool BUSY {
		set{ busy = value; }
		get{ return busy; }
	}

	public DiFSM ()
	{
		dictState = new Dictionary<int, State> ();
		current = new State ();
		next = new State ();
	}

	public void Add (State e)
	{
		Debug.Log ("attempting to add "+ e +" with id: "+e.id);
		if (!dictState.ContainsKey (e.id)) {
			Debug.Log ("Adding state id: "+e.id);
			e.parentFSM = this;
			dictState.Add (e.id, e);
		}
	}

	public bool isCurrent (State e)
	{
		return (e == current);
	}

	public bool isCurrent (Type t)
	{
		return (t == current.GetType ());
	}

	public State GetCurrent ()
	{
		return current;
	}

	public void Set (int id)
	{
		if (!dictState.ContainsKey (id)) {
			Debug.Log ("Error, State id" + id + " not found!");
			return;
		}
		Set (dictState [id]);
	}

	public void Set (State state)
	{
		/*next = FindState (state);
		Debug.Log ( next + " encontrado");
		if (next == null) {
			Debug.Log ("Error, State " + state + " not found!");	
			return;
		}*/
		current.Off ();
		current = dictState [state.id];
		current.On ();
		Debug.Log ("set " + current);
	}

	public void Set (Type t)
	{
		next = FindState (t);
		if (next == null) {
			Debug.Log ("Error, State " + t + " not found!");
			return;
		}
		current.Off ();
		current = next;
		current.On ();
		Debug.Log("set " + current);
	}
	
	internal virtual State FindState (Type t)
	{
		foreach (var val in dictState.Values) {
			if (val.GetType ().Equals (t)) {
				return val;
			}
		}
		return null;
	}

	public void Update ()
	{
		current.Update ();
	}

	public void Next ()
	{
		Set (current.next);
	}
}
