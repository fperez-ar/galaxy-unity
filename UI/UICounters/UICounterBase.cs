using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof(Image))]
public class UICounterBase : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler
{
	[HideInInspector]
	public int position = 0;
	public Color init = Color.green, end = Color.red;

	public object getReference ()
	{
		return refObj;
	}

	protected object refObj;
	protected Text qText;
	protected Image img;

	public virtual void Awake ()
	{
		getComponents ();
	}

	public void getComponents ()
	{
		if (!img)
			img = GetComponent<Image> ();
		if (!qText)
			qText = GetComponentInChildren <Text> ();
	}


	public virtual void set (Troopers t)
	{
		refObj = t;
		img.color = Color.Lerp (end, init, (t.manpower / BaseVals.maxTroopersPerUnit));
		qText.color = ColorUtil.getOpposite (img.color); //ColorUtil.getContrastBorW (img.color);
		qText.text = t.manpower.ToString ();
		gameObject.SetActive (true);
	}

	public virtual void set (ResourceBase r)
	{
		refObj = r;
		img.color = Color.Lerp (end, init, r.normalizedQuantity);
		qText.color = ColorUtil.getOpposite (img.color); //ColorUtil.getContrastBorW (img.color);
		qText.text = r.quantity.ToString ();
		gameObject.SetActive (true);
	}

	public virtual void set (GeneticTrait g)
	{
		refObj = g;
		img.color = Color.Lerp (end, init, g.normalizedQuantity);
		qText.color = ColorUtil.getOpposite (img.color); //ColorUtil.getContrastBorW (img.color);
		qText.text = g.name [0].ToString ();
		gameObject.SetActive (true);
	}

	public virtual void reset ()
	{
		//refObj = null;
		img.color = Color.white;
		qText.color = Color.black;
		qText.text = string.Empty;
		gameObject.SetActive (false);
	}

	public virtual void OnPointerEnter (PointerEventData eventData)
	{
		EvHandler.ExecuteEv (UIEvent.SHOW_TOOLTIP, refObj.ToString ());
	}

	public virtual void OnPointerExit (PointerEventData eventData)
	{
		EvHandler.ExecuteEv (UIEvent.HIDE_TOOLTIP);
	}

	protected virtual void OnDisable ()
	{
		EvHandler.ExecuteEv (UIEvent.HIDE_TOOLTIP);
	}


}
