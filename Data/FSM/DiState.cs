using UnityEngine;
using System;
using System.Collections;

public class DiState : State {

	//TODO: Implement actual random number that work or find out why GetHashCode returns the same for all objects
	public new int id{ get { return -1; } }

}
