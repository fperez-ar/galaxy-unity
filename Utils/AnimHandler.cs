using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{
	
	//TODO: Implement state machine

	public enum AnimState
	{
		idle,
		movingTo,
		orbit
	}

	private AnimState astate = AnimState.idle, previous;

	private float cutoffPerc;
	private Vector3 point;
	public float angle = 0, speed = 1, xAmplitude = 1, yAmplitude = 1;
	private float startTime = 0, elapsed = 0, distCovered = 0, totalDistance = 0;
	public Transform ship, tow;

	public void awake() {
		EvHandler.RegisterEv (UIEvent.ENTER_PLT, MoveTo);
	}

	void Update ()
	{
		if (astate == AnimState.movingTo) {
			
			distCovered = (Time.time - startTime) * speed;
			elapsed = distCovered / totalDistance;

			ship.forward = Vector3.Slerp (ship.forward, tow.position - ship.position, elapsed);

			if (elapsed >= cutoffPerc) {
				Orbit (tow);
			} else {
				ship.position = Vector3.Lerp (ship.position, tow.position, elapsed);
			}
		} 
	}

	void FixedUpdate ()
	{
		if (astate == AnimState.orbit) {

			//TODO: Eliminate jerking motion at beginning of movement
			angle = speed * Mathf.Deg2Rad * Time.time;

			point.x = Mathf.Cos (angle * speed) * xAmplitude;
			point.z = Mathf.Sin (angle * speed) * yAmplitude;

			ship.position = Vector3.Lerp (ship.position, tow.position + point, 0.25f);
			ship.forward = tow.position - ship.position;
		}
	}

	public void MoveTo (object oTo)
	{
		Debug.Log ("move to");
		Transform _to = (Transform) oTo;
		if (astate == AnimState.movingTo) {
			//Toggle
			astate = AnimState.idle;
			return; 
		} else if (astate == AnimState.orbit && _to == tow) {
			//Toggle ?
			astate = AnimState.idle;
			return; 
		} else if (astate == AnimState.idle && _to == tow) {
			//if it's the same, just orbit
			Orbit (tow);
			return;
		}

		startTime = Time.time;
		elapsed = 0;

		tow = _to.transform;
		point = _to.transform.position;

		totalDistance = Vector3.Distance (ship.position, tow.position);
		cutoffPerc = (_to.transform.lossyScale.magnitude) / totalDistance;

		if (totalDistance <= cutoffPerc) {
			astate = AnimState.orbit;
		} else {
			astate = AnimState.movingTo;
		}
	}

	public void Orbit (Transform _around)
	{

		if (astate == AnimState.orbit) {
			astate = AnimState.idle;
			return;
		}

		tow = _around;
		point = Vector3.zero;
		angle = Vector3.Angle (ship.position, tow.position) * Mathf.Deg2Rad;

		xAmplitude *= tow.lossyScale.magnitude;
		yAmplitude *= tow.lossyScale.magnitude;

		astate = AnimState.orbit;
		EvHandler.ExecuteEv (GameEvent.ORBIT_PLT, _around.GetComponent<CelestialBody> ());
	}

	public void Suspend ()
	{
		previous = astate;
		astate = AnimState.idle;
	}

	public void Resume ()
	{
		astate = previous;
	}


}
