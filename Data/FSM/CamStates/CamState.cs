using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamState : State {

	protected Camera cam;

	public CamState ()
	{
		
	}

	public CamState (Camera _cam)
	{
		cam = _cam;
	}

}
