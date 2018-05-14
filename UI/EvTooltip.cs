using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvTooltip : MonoBehaviour {

	public Text text;
	public RectTransform rect;
	public Vector3 offset = Vector3.zero;
	private Vector3 smartOffset{
		get { 
				float mod = (Input.mousePosition.x > Screen.width / 2) ? -1 : 1;
				return offset * mod;
			}
	}

	void Awake () {
		//text = GetComponent<Text> ();
		rect = GetComponent<RectTransform> ();
		EvHandler.RegisterEv (UIEvent.SHOW_TOOLTIP, Show);
		EvHandler.RegisterEv (UIEvent.HIDE_TOOLTIP, Hide);
	}

	void Show (object strObject) {
		
		gameObject.SetActive (true);
		text.text = (string) strObject;
		adjustPivot ();
		transform.position = Input.mousePosition + smartOffset;
	}

	void Hide(){
		gameObject.SetActive (false);
	}


	void adjustPivot()
	{
		Vector2 piv = rect.pivot;

		bool rSide = (Input.mousePosition.x > Screen.width / 2);
		piv.x = rSide ? 1 : 0;
		//text.alignment = rSide ? TextAnchor.UpperRight : TextAnchor.UpperLeft;

		rect.pivot = piv;
		text.rectTransform.pivot = piv;
		//text.rectTransform.position -= smartOffset;
		//rect.position += smartOffset;
	}



	/*
	void Update(){
		if (show) {
			if (delayHide.check ()) {
				show = false;
			}
			transform.position = Input.mousePosition + offset;
		}
	}
	*/

}
