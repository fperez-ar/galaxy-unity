using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyState {
	idle,
	discovered, 
	probed,
	invaded
}
public class CelestialBody : MonoBehaviour {

	public BodyState state;
}
