using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class CounterBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	
	public ICounterable counter;
	public Color init = Color.green, end = Color.red;
	protected Text qText;
	protected Image img;

	public virtual void Awake () {
		getComponents ();
	}

	public void getComponents(){
		if ( ! img ) img = GetComponent<Image> ();
		if ( ! qText ) qText = GetComponentInChildren <Text> ();
	}

	public virtual void setCounter(ICounterable _counter) {
		counter = _counter;
		refresh (_counter);
	}

	public virtual void refresh(ICounterable c) {
		img.color = Color.Lerp (end, init, c.percQuantity);
		qText.text = c.counterQuantity.ToString ();
		qText.color = ColorUtil.getOpposite(img.color); //ColorUtil.getContrastBorW (img.color);
	}

	public virtual void OnPointerEnter(PointerEventData eventData) {
		//Debug.Log ( "OnPointerEnter" );
		if (counter == null) return;
		EvHandler.ExecuteEv (UIEvent.SHOW_TOOLTIP, counter.getStringQuantity ());
	}

	public void OnPointerExit(PointerEventData eventData) {
		//Debug.Log ( "OnPointerExit" );
		if (counter == null) return;
		EvHandler.ExecuteEv (UIEvent.HIDE_TOOLTIP);
	}

	public virtual void ForceResetPos(){
		gameObject.SetActive (false);
		gameObject.SetActive (true);
	}

}
