using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{
	public Vector2 amplitude = Vector2.one;
	public float movementSpeed = 0.5f, orbitSpeed = 0.5f;
	public float cutoffPerc = 1;

	public Transform ship;
	private TypeDiFSM  fsm;

	private ShipState idleState;
	private ShipOrbitState orbitState;
	private ShipMoveToState moveToState;

	public void awake ()
	{
		EvHandler.RegisterEv (UIEvent.ENTER_PLT, MoveTo);
		SetFSM ();
	}

	public void SetFSM ()
	{
		fsm = new TypeDiFSM ();
		idleState = new ShipState ();
		moveToState = new ShipMoveToState (ship, movementSpeed, cutoffPerc);
		orbitState = new ShipOrbitState (ship, orbitSpeed, amplitude.x, amplitude.y);
		moveToState.next = orbitState;
		fsm.Add (idleState);
		fsm.Add (orbitState);
		fsm.Add (moveToState);
		fsm.Set (idleState);
	}

	public void update ()
	{
		fsm.Update ();
	}

	public void MoveTo (object moveTo)
	{
		moveToState.target = (Transform) moveTo;
		orbitState.target = (Transform) moveTo;
		fsm.Set (moveToState);
	}

	public void Orbit (object orbitAround)
	{
		orbitState.target = (Transform) orbitAround;
		fsm.Set (orbitState);
	}

	public void Suspend ()
	{
	}

	public void Resume ()
	{

	}


}
