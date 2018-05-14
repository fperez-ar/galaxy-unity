using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrooperUIElement : MonoBehaviour {

	private Troopers refTroopers;
	public Image img;
	public Text quantity;
	public Text troopName;
	public Text offensive;
	public Text defensive;

	public void set(Troopers t) {
		this.gameObject.SetActive (true);
		img.color = Color.Lerp(BaseVals.maxMoraleColor, BaseVals.minMoraleColor, t.morale/BaseVals.Morale);
		quantity.color = ColorUtil.getOpposite (img.color);
		quantity.text = t.manpower.ToString ();
		troopName.text = t.name;
		offensive.text = "Offense " + t.offensiveCap;
		defensive.text = "Defense " + t.defensiveCap;
	}

}
