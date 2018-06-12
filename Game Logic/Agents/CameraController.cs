using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

	public Camera cam;
	public TypeDiFSM fsm = new TypeDiFSM ();
	public float rotSpeed = 10f, camRotationSpeed = 100;

	void Start(){
		//cam = GetComponent <Camera> ();
		fsm.Add (new MoveCamState (cam, transform, rotSpeed, camRotationSpeed));
		fsm.Add (new IdleCamState() );
		fsm.Set (typeof(MoveCamState));
		EvHandler.RegisterEv ( GameEvent.UI_PHASE, idleCam );
		EvHandler.RegisterEv ( GameEvent.EXPLORE_PHASE, moveCam );
	}

	void idleCam(){
		fsm.Set (typeof(IdleCamState));
	}

	void moveCam(){
		fsm.Set (typeof(MoveCamState));
	}

	void Update () {
		fsm.Update ();
	}


}
