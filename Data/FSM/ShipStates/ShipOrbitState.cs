using UnityEngine;
public class ShipOrbitState : State {
	
	private Vector3 point;
	private Transform parent;
	public Transform target;
	private float angle = 0, speed = 1;
	private Vector2 baseAmplitude = Vector2.one, amplitude;

	public ShipOrbitState (Transform ship, ref float orbitSpeed, ref float amplitudeX, ref float amplitudeY)
	{
		parent = ship;
		speed = orbitSpeed;
		baseAmplitude.x = amplitudeX;
		baseAmplitude.y = amplitudeY;
	}

	public override void On ()
	{
		angle = Vector3.Angle (parent.position, target.position) * Mathf.Deg2Rad;

		amplitude.x =  baseAmplitude.x * target.lossyScale.magnitude;
		amplitude.y =  baseAmplitude.y * target.lossyScale.magnitude;

		EvHandler.ExecuteEv (GameEvent.ORBIT_PLT, target.GetComponent<CelestialBody> ());
	}

	public override void Update ()
	{
		//TODO: Eliminate jerking motion at beginning of movement
		angle = speed * Mathf.Deg2Rad * Time.time;

		point.x = Mathf.Cos (angle * speed) * amplitude.x;
		point.z = Mathf.Sin (angle * speed) * amplitude.y;

		parent.position = Vector3.Lerp (parent.position, target.position + point, 0.25f);
		parent.forward = target.position - parent.position;
	}
}
