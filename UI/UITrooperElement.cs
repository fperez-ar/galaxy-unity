using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UITrooperElement : UIListElement, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	private Troopers refTroopers;
	public Image img;
	public Text quantity;
	public Text troopName;
	public Text offensive;
	public Text defensive;

	public override void set(Troopers trup)
	{
		this.gameObject.SetActive (true);
		img.color = Color.Lerp(BaseVals.maxMoraleColor, BaseVals.minMoraleColor, trup.morale/BaseVals.Morale);
		quantity.color = ColorUtil.getOpposite (img.color);
		quantity.text = trup.manpower.ToString ();
		troopName.text = trup.name;
		offensive.text = trup.offensiveCap.ToString ();
		defensive.text = trup.defensiveCap.ToString ();
		refTroopers = trup;
	}


	public void OnPointerClick(PointerEventData eventData) {
		Debug.Log ("Clicked trooper element "+refTroopers.name);
	}

	public virtual void OnPointerEnter(PointerEventData eventData) {
		//Debug.Log ( "OnPointerEnter" );
		//EvHandler.ExecuteEv (UIEvent.SHOW_TOOLTIP, refTroopers.ToString ());
	}

	public void OnPointerExit(PointerEventData eventData) {
		//Debug.Log ( "OnPointerExit" );
		//EvHandler.ExecuteEv (UIEvent.HIDE_TOOLTIP);
	}


}
