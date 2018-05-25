using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEvTooltip : MonoBehaviour
{

	public RectTransform rect;
	public Image bgBox;
	public Text text;
	public Vector3 offset = Vector3.zero;
	public bool trackMouse = false;
	private bool fadeTooltip = false;
	public float fadeRate = 0.1f, delayFade = 3f;

	private Vector3 smartOffset {
		get { 
			float mod = (Input.mousePosition.x > Screen.width / 2) ? -1 : 1;
			return offset * mod;
		}
	}

	void Awake ()
	{
		if (bgBox != null) bgBox = GetComponent<Image> ();
		if (rect  != null) rect = GetComponent<RectTransform> ();
		EvHandler.RegisterEv (UIEvent.SHOW_TOOLTIP, Show);
		EvHandler.RegisterEv (UIEvent.HIDE_TOOLTIP, Hide);
		EvHandler.RegisterEv (UIEvent.SHOW_AUTOFADE_TOOLTIP, AutoFade);
		Hide ();
	}

	void Show (object strObject)
	{
		gameObject.SetActive (true);
		text.text = (string)strObject;
		if (trackMouse) {
			adjustPivot ();
			transform.position = Input.mousePosition + smartOffset;
		}
	}

	void Hide ()
	{
		gameObject.SetActive (false);
	}

	void AutoFade (object strObject)
	{
		gameObject.SetActive (true);
		text.text = (string)strObject;
		StartCoroutine (fade ());
	}

	void adjustPivot ()
	{
		Vector2 piv = rect.pivot;
		bool rSide = (Input.mousePosition.x > Screen.width / 2);
		piv.x = rSide ? 1 : 0;
		//text.alignment = rSide ? TextAnchor.UpperRight : TextAnchor.UpperLeft;
		rect.pivot = piv;
		text.rectTransform.pivot = piv;
	}

	IEnumerator fade ()
	{
		Color bgColor= bgBox.color;
		Color txtColor= text.color;

		if (fadeTooltip) {
			Color resetAlpha = text.color;
			resetAlpha.a = 1;
			text.color = resetAlpha;
		}
		fadeTooltip = true;

		yield return new WaitForSeconds (delayFade);

		while (fadeTooltip) {

			Color newAlpha = bgBox.color;
			newAlpha.a -= fadeRate;
			bgBox.color = newAlpha;

			newAlpha = text.color;
			newAlpha.a -= fadeRate;
			text.color = newAlpha;

			if (text.color.a <= 0.15f) {
				Hide ();
				text.color = txtColor;
				bgBox.color = bgColor;
			}

			yield return new WaitForSeconds (0.1f);
		}
	}


}
