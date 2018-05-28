using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

	public Camera cam;
	public TypeDiFSM fsm = new TypeDiFSM ();
	public float zoomSpeed = 10, rotSpeed = 0.8f, moveSpeed = 0.2f;

	void Start(){
		//cam = GetComponent <Camera> ();
		fsm.Add (new MoveCamState (cam, transform, moveSpeed, rotSpeed, zoomSpeed));
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
