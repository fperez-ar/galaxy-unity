using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

	public Camera cam;
	public FSM fsm = new FSM();
	public float zoomSpeed = 10, rotSpeed = 0.8f, moveSpeed = 0.2f;

	void Start(){
		//cam = GetComponent <Camera> ();
		fsm.Add (new OrbitCamState (cam, transform, moveSpeed, rotSpeed, zoomSpeed));
		fsm.Add (new CombatCamState() );
		fsm.Set (typeof(OrbitCamState));
		EvHandler.RegisterEv ( UIEvent.SHOW_COMBAT_PANEL, enterCombatPhase );
		EvHandler.RegisterEv ( UIEvent.HIDE_COMBAT_PANEL, exitCombatPhase );
	}

	void enterCombatPhase(){
		fsm.Set (typeof(CombatCamState));
	}

	void exitCombatPhase(){
		fsm.Set (typeof(OrbitCamState));
	}

	void Update () {
		fsm.Update ();
	}


}
