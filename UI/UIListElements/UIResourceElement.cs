using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIResourceElement : UIListElement<ResourceBase>, IPointerClickHandler {
	
	public Image img;
	public Text quantity;
	public Text resName;

	public override void set(ResourceBase r) {
		this.gameObject.SetActive (true);
		img.color = Color.Lerp(Color.red, Color.green, r.normalizedQuantity );
		quantity.color = ColorUtil.getOpposite (img.color);
		resName.text = r.name;
		quantity.text = r.quantity.ToString ();
		refObj = r;
	}

	public void OnPointerClick(PointerEventData eventData) {
		//EvHandler.ExecuteEv (GameEvent.ADD_RES, refObj);
	}


}
