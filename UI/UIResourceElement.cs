using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIResourceElement : UIListElement, IPointerEnterHandler, IPointerExitHandler {
	
	private Troopers refRes;
	public Image img;
	public Text quantity;
	public Text resName;

	public override void set(ResourceBase r) {
		this.gameObject.SetActive (true);
		img.color = Color.Lerp(Color.red, Color.green, r.normalizedQuantity );
		quantity.color = ColorUtil.getOpposite (img.color);
		resName.text = r.name;
		quantity.text = r.quantity.ToString ();

	}

	public virtual void OnPointerEnter(PointerEventData eventData) {
	}

	public void OnPointerExit(PointerEventData eventData) {
	}

}
