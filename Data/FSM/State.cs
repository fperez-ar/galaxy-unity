using UnityEngine;
using System;
using System.Collections;

public class State
{
	public int id;
	public  SimpleDelegate onBegin;
	public  SimpleDelegate onEnd;
	
	public iFSM parentFSM;
	public State next;

	
	public virtual void On ()
	{
//		Debug.Log(this+" activado");
	}

	public virtual void Off ()
	{
//		Debug.Log(this+" desactivado");
	}

	public virtual void Update ()
	{		
	}

	public virtual void NextState ()
	{
//		Debug.Log("nextState " + next);
		parentFSM.BUSY = false;
		
		if (next != null)
			parentFSM.Set (next);
		else
			parentFSM.Next ();		
	}

	public virtual void NextState (Type t)
	{
//		Debug.Log("nextState " + next);
		parentFSM.BUSY = false;
		parentFSM.Set (t);
	}

	public virtual void NextState (State nxt)
	{
//		Debug.Log("nextState " + next);
		parentFSM.BUSY = false;
		
		if (next != null)
			parentFSM.Set (nxt);
	}
	
	
}
