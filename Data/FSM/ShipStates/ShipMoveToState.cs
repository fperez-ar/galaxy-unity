using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMoveToState : State
{

	private Transform parent;
	private Vector3 vel = Vector3.zero;
	public Transform target;
	private float distCovered, startTime, totalDistance;
	private float speed, cutOffPerc, cutOffDistance;
	private Quaternion finalRotation;

	public ShipMoveToState (Transform ship, float movementSpeed, float cuttOffPercentage)
	{
		parent = ship;
		speed = movementSpeed;
		cutOffPerc = cuttOffPercentage;
	}

	public override void On ()
	{
		finalRotation = Quaternion.LookRotation (target.position - parent.position);
		cutOffDistance = cutOffPerc * Vector3.Distance (parent.position, target.position);

	}

	public override void Update ()
	{
		float curSpeed = (1 / speed);
		float distance = (target.position - parent.position).sqrMagnitude;
		if ( distance <= cutOffDistance ) {
			NextState ();
		} else {
			//snappier >> //parent.forward = Vector3.Lerp(parent.forward, objective.position - parent.position, speed * Time.deltaTime);
			//smoother >>
			parent.rotation = Quaternion.Slerp (parent.rotation, finalRotation, speed * Time.deltaTime);
			//move to //parent.position = Vector3.Lerp (parent.position, objective.position, elapsedPerc);
			parent.position = Vector3.SmoothDamp(parent.position, target.position, ref vel, curSpeed);
		}
	}


}
