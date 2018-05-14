using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour {
	
	//TODO: Implement state machine

	public enum AnimState{
		idle,
		movingTo,
		orbit
	}
	static AnimState astate = AnimState.idle, previous;

	static float cutoffPerc;
	static Vector3 point;
	static Transform move, tow;
	static float angle = 0, speed = 1, xAmplitude = 1, yAmplitude = 1;
	private static float startTime = 0, elapsed = 0, distCovered = 0, totalDistance = 0;

    void Update () 
	{
		if (astate == AnimState.movingTo) {
			
			distCovered = (Time.time - startTime) * speed;
			elapsed  = distCovered / totalDistance ;

			move.forward = Vector3.Slerp (move.forward, tow.position-move.position, elapsed);

			//if ( Vector3.Distance (move.position, tow.position) <= cutThreshold ) {
			if ( elapsed >= cutoffPerc){
				Orbit (move, tow, speed, 1, 1);
			} else {
				move.position = Vector3.Lerp (move.position, tow.position, elapsed);
			}
		} 
	}

	void FixedUpdate()
	{
		if (astate == AnimState.orbit){

			//TODO: Eliminate jerking motion at beginning of movement
			angle = speed * Mathf.Deg2Rad * Time.time;

			point.x = Mathf.Cos(angle * speed) * xAmplitude ;
			point.z = Mathf.Sin(angle * speed) * yAmplitude ;


			//move.position = (tow.position + point) * Time.deltaTime;
			move.position  = Vector3.Lerp(move.position, tow.position+point, 0.25f );
			move.forward =  tow.position - move.position ;
		}
	}

	public static void MoveTo(Transform _move, Transform _to, float _cutOff, float _speed){
		
		if (astate == AnimState.movingTo) {
			astate = AnimState.idle;
			return;
		} else if (astate == AnimState.orbit && _to == tow) {
			astate = AnimState.idle;
			return;
		} else if (astate == AnimState.idle && _to == tow) {
			Orbit (move, tow, speed, 1, 1);
			return;
		}

		startTime = Time.time;
		elapsed = 0;

		move = _move;
		tow = _to.transform;
		point = _to.transform.position;

		totalDistance = Vector3.Distance (move.position, tow.position);
		cutoffPerc = (_cutOff * _to.transform.lossyScale.magnitude) / totalDistance ;

		/*
		print ("_cutoff "+_cutOff); 
		print ("transform scale "+ _col.transform.lossyScale.magnitude); 
		print ("totalDistance "+totalDistance );
		print ("result "+cutoffPerc ); 
		*/
		speed = _speed;

		if (totalDistance <= cutoffPerc){
			astate = AnimState.orbit;
		}else{
			astate = AnimState.movingTo;
		}
	}

	public static void Orbit(Transform _center, Transform _around, float _speed, float _xAmplitude, float _yAmplitude){

		if (astate == AnimState.orbit) {
			astate = AnimState.idle;
			return;
		}

		move = _center;
		tow = _around;
		speed = _speed;
		point = Vector3.zero;
		angle = Vector3.Angle (move.position, tow.position) * Mathf.Deg2Rad;


		xAmplitude = (tow.lossyScale.magnitude * _xAmplitude) ;
		yAmplitude = (tow.lossyScale.magnitude * _yAmplitude) ;
		/*
		print ("Objective scale "+ tow.lossyScale.magnitude);
		print ("Objective mod (_amplitude) "+ _xAmplitude);
		print ("Result "+xAmplitude);
		*/

		astate = AnimState.orbit;
	}

	public static void Suspend(){
		previous = astate;
		astate = AnimState.idle;
	}

	public static void Resume(){
		astate = previous;
	}


}
