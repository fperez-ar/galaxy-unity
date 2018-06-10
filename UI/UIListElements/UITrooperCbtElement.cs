using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UITrooperCbtElement : UIListElement<Troopers>, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	public Image img;
	public Text quantity;
	public Text troopName;
	public Text offensive;
	public Text defensive;
	public Text adapt;

	public override void set(Troopers troop)
	{
		this.gameObject.SetActive (true);
		img.color = Color.Lerp(BaseVals.maxMoraleColor, BaseVals.minMoraleColor, troop.morale/BaseVals.Morale);
		quantity.color = ColorUtil.getOpposite (img.color);
		quantity.text = troop.manpower.ToString ();
		troopName.text = troop.name;
		offensive.text = troop.offensiveCap.ToString ();
		defensive.text = troop.defensiveCap.ToString ();
		adapt.text	   = troop.adaptability.ToString ();
		refObj = troop;
	}

	public override void unset ()
	{
		base.unset ();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		//Debug.Log ("Clicked trooper element "+refObj);
		if (this.isActiveAndEnabled && GameMode.isMode(GameState.COMBAT))
		{
			EvHandler.ExecuteEv (GameEvent.RM_CBT_FF, refObj);
		}
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		//Debug.Log ( "OnPointerEnter" );
		//EvHandler.ExecuteEv (UIEvent.SHOW_TOOLTIP, refObj.ToString ());
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//Debug.Log ( "OnPointerExit" );
		//EvHandler.ExecuteEv (UIEvent.HIDE_TOOLTIP);
	}



}
