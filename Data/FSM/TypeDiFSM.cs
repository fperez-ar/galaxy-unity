using System;
using System.Collections.Generic;
using UnityEngine;

public class TypeDiFSM : iFSM
{
	protected Dictionary<Type, State> dictState;
	private State current;

	private bool busy;

	public bool BUSY {
		set{ busy = value; }
		get{ return busy; }
	}

	public TypeDiFSM ()
	{
		dictState = new Dictionary<Type, State> ();
		current = new State ();
	}

	public void Add (State e)
	{
		if (!dictState.ContainsKey (e.GetType ())) {
			e.parentFSM = this;
			dictState.Add (e.GetType (), e);
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
		throw new System.NotImplementedException ();
	}

	public void Set (State state)
	{
		current.Off ();
		current = dictState.ContainsValue (state) ? dictState [state.GetType ()] : null;
		current.On ();
		//Debug.Log ("set " + current);
	}

	public void Set (Type t)
	{
		current.Off ();
		current = dictState.ContainsKey (t) ? dictState [t] : null;
		current.On ();
		//Debug.Log("set " + current);
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