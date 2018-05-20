using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class UICounterBase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	private object refObj;
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

	public virtual void set(Troopers t) {
		refObj = t;
		img.color = Color.Lerp (end, init, (t.manpower/BaseVals.maxTroopersPerUnit) );
		qText.color = ColorUtil.getOpposite(img.color); //ColorUtil.getContrastBorW (img.color);
		qText.text = t.manpower.ToString ();
	}

	public virtual void set(ResourceBase r) {
		refObj = r;
		img.color = Color.Lerp (end, init, r.normalizedQuantity );
		qText.color = ColorUtil.getOpposite(img.color); //ColorUtil.getContrastBorW (img.color);
		qText.text = r.quantity.ToString ();
	}

	public virtual void set(GeneticTrait g) {
		refObj = g;
		img.color = Color.Lerp (end, init, g.normalizedQuantity );
		qText.color = ColorUtil.getOpposite(img.color); //ColorUtil.getContrastBorW (img.color);
		qText.text = g.name[0].ToString ();
	}

	public virtual void OnPointerEnter(PointerEventData eventData) {
	}


	public virtual void OnPointerExit(PointerEventData eventData) {
	}

	public virtual void OnPointerClick(PointerEventData eventData) {
		print ("\tRemove Gene element"+refObj);
		EvHandler.ExecuteEv (GameEvent.RM_GENE_MAT, refObj);
	}
}
