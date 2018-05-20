using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInvDisplay : MonoBehaviour {

	public UnityEngine.UI.Text planetNameTxt;
	public UnityEngine.UI.Text planetRaceTxt;
	//public UnityEngine.UI.Text planetCultureTxt;

	public GameObject mineActionBtn;
	public GameObject invadeActionBtn;

	void Awake(){
		getComponents ();
		EvHandler.RegisterEv (UIEvent.SHOW_SUN_INFO, updateSunText);
		EvHandler.RegisterEv (UIEvent.SHOW_PLANET_INFO, updatePlanetText);
	}

	void Start(){
		clear ();
	}

	void getComponents(){
	}

	void clear() {
		planetNameTxt.text = string.Empty;
		planetRaceTxt.text = string.Empty;
		//planetCultureTxt.text = string.Empty;
		invadeActionBtn.SetActive ( false );
		mineActionBtn.SetActive ( false );
	}


	void updatePlanetText(object oPlanet){
		Planet p = (Planet) oPlanet;
		clear ();

		DisplayText (p.name, planetNameTxt);
	}

	void updateSunText(object oSun){
		Sun s = ((Sun)oSun);
		clear ();
		planetNameTxt.text = s.name;
	}

	void DisplayText(string displayStr, UnityEngine.UI.Text displayTxt){
		if (displayStr == string.Empty) {
			displayTxt.enabled = false;
		} else {
			displayTxt.enabled = true;
			displayTxt.text = displayStr;
		}
	}

	void clearIcons(List<UICounterBase> li) {
		for (int l = 0; l < li.Count; l++) {
			li [l].gameObject.SetActive (false);
		}
	}


}
