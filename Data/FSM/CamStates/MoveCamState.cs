using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCamState : CamState {

	private Vector3 point;
	private Collider lastCol;
	private Transform transform;
	private Vector3 baseMousePos = Vector3.zero;
	private float rotSpeed = 0.8f, camRotSpeed;

	public MoveCamState (Camera _cam, Transform _transform, float _rotSpeed, float _camRotSpeed)
	{
		cam = _cam;
		transform = _transform;
		rotSpeed = _rotSpeed;
		camRotSpeed = _camRotSpeed;
	}

	public override void Update ()
	{
		if (Input.GetKeyDown (KeyCode.F)) {
			if ( lastCol ) EvHandler.ExecuteEv (UIEvent.ENTER_PLT, lastCol.transform);
		}

		/* Zoom
		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			transform.position += transform.forward * Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
		}
		*/
		//Rotation
		if (Input.GetMouseButtonDown (0)) {

			if (EventSystem.current.IsPointerOverGameObject ()) {
				return;
			}

			baseMousePos = Input.mousePosition;
			RaycastHit rinfo;

			if (Physics.Raycast (cam.ScreenPointToRay (Input.mousePosition), out rinfo, Mathf.Infinity, (Layers.PLANET | Layers.SUN))) {
				point = rinfo.collider.transform.position;
				lastCol = rinfo.collider;

				var p = lastCol.GetComponent <Planet> ();
				if (p) {
					EvHandler.ExecuteEv (GameEvent.SELECT_BODY, p);
				} else {
					var s = lastCol.GetComponent <Sun> ();
					EvHandler.ExecuteEv (GameEvent.SELECT_BODY, s);
				}
				EvHandler.ExecuteEv (UIEvent.ANIM_IDLE);
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			EvHandler.ExecuteEv (UIEvent.ANIM_CON);
			Cursor.visible = true;
		}

		if (Input.GetMouseButton (0)) {
			if (EventSystem.current.IsPointerOverGameObject ()) {
				return;
			}

			var deltaMouse = (baseMousePos - Input.mousePosition);
			Cursor.visible = false;
			if (lastCol) {
				transform.RotateAround (lastCol.transform.position, transform.right, deltaMouse.y * rotSpeed * Time.deltaTime);
				transform.RotateAround (lastCol.transform.position, transform.up, -deltaMouse.x * rotSpeed * Time.deltaTime);
			} else {
				transform.RotateAround (point, transform.right, deltaMouse.y * rotSpeed * Time.deltaTime);
				transform.RotateAround (point, transform.up, -deltaMouse.x * rotSpeed * Time.deltaTime);
			}
		}

		//TODO: Move movement to ASDF + Q&E/Q&C
		//Movement
		if (Input.GetMouseButtonDown (1)) {
			baseMousePos = Input.mousePosition;
		}
		if (Input.GetMouseButton (1)) {

			//Vector3 deltaMouse = (baseMousePos - Input.mousePosition);

			//Debug.Log (deltaMouse);
			//cam.transform.RotateAround (cam.transform.position, transform.right, deltaMouse.y * Time.deltaTime);
			float speed = camRotSpeed * Input.GetAxis ("Mouse X") * Time.deltaTime;

			cam.transform.RotateAround (cam.transform.position, transform.up, speed);
		}
	}


}
