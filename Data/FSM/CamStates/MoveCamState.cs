using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCamState : CamState {

	private Vector3 point;
	private Collider lastCol;
	private Transform transform;
	private Vector3 baseMousePos = Vector3.zero;
	private float moveSpeed = 0.2f, rotSpeed = 0.8f, zoomSpeed = 10;

	public MoveCamState (Camera _cam, Transform _transform, float _moveSpeed, float _rotSpeed, float _zoomSpeed)
	{
		cam = _cam;
		transform = _transform;
		moveSpeed = _moveSpeed;
		zoomSpeed = _zoomSpeed;
		rotSpeed = _rotSpeed;
	}

	public override void Update ()
	{
		if (Input.GetKeyDown (KeyCode.F)) {
			if ( lastCol ) EvHandler.ExecuteEv (UIEvent.ENTER_PLT, lastCol.transform);
		}

		// Zoom
		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			transform.position += transform.forward * Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
		}

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
					EvHandler.ExecuteEv (UIEvent.SHOW_PLANET_INFO, p);
				} else {
					var s = lastCol.GetComponent <Sun> ();
					EvHandler.ExecuteEv (UIEvent.SHOW_SUN_INFO, s);
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
			var deltaMouse = (baseMousePos - Input.mousePosition);
			transform.RotateAround (cam.transform.position, transform.up, deltaMouse.x * rotSpeed * Time.deltaTime);
			//transform.RotateAround (cam.transform.position, transform.right, deltaMouse.y * rotSpeed * Time.deltaTime);
			//transform.position += transform.right * deltaMouse.x * moveSpeed * Time.deltaTime;
			//transform.position += transform.up * deltaMouse.y * moveSpeed * Time.deltaTime;
		}
	}


}
