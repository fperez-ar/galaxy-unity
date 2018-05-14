using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlyCounter : CounterBase, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public bool draggable = false;
	private bool dragEnabled = true;


	public override void Awake ()
	{
		base.Awake ();

		if (draggable){
			EvHandler.RegisterEv (GameEvent.PREPARATION_PHASE, combatPhaseIn);
			EvHandler.RegisterEv (GameEvent.EXPLORE_PHASE, combatPhaseOut);
			//EvHandler.RegisterEv ("reset", combatPhaseOut);
		}
	}

	public void OnPointerClick(PointerEventData eventData) {
		Debug.Log ( "clicky" );
	}

	public void OnBeginDrag(PointerEventData eventData) {}

	public void OnDrag(PointerEventData eventData) {
		if (dragEnabled){
			transform.position = Input.mousePosition;
		}
	}

	public void OnEndDrag(PointerEventData eventData) {
		EvHandler.ExecuteEv (UIEvent.COUNTER_DROP, this );
	}

	/*
	void tooltipShow(ICounterable iC){
		if (iC.GetType () == typeof(Troopers)) {
			Troopers t = (Troopers)iC;
			EvHandler.ExecuteEv (EnumEvent.SHOW_TOOLTIP, t.getStringQuantity ());
		}else if (iC.GetType() == typeof(ResourceBase)){
			ResourceBase r = (ResourceBase)iC;
			EvHandler.ExecuteEv (EnumEvent.SHOW_TOOLTIP, r.getStringQuantity ());
		}else if (iC.GetType() == typeof(GeneticTrait)){
			GeneticTrait g = (GeneticTrait)iC;
			EvHandler.ExecuteEv (EnumEvent.SHOW_TOOLTIP, g.getStringQuantity ());
		}
	}
	*/

	void combatPhaseIn()  { 
		dragEnabled = true;
	}

	void combatPhaseOut() {
		draggable = true;
		dragEnabled = false;
		ForceResetPos ();
	}
}
