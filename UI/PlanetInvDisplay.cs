using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInvDisplay : MonoBehaviour
{

	public UnityEngine.UI.Text planetNameTxt;
	public UnityEngine.UI.Text planetRaceTxt;
	//public UnityEngine.UI.Text planetCultureTxt;

	public GameObject probeActionBtn;
	public GameObject invadeActionBtn;

	void Awake ()
	{
		getComponents ();
		EvHandler.RegisterEv (UIEvent.SHOW_SUN_INFO, updateSunText);
		EvHandler.RegisterEv (UIEvent.SHOW_PLANET_INFO, updatePlanetText);
	}

	void Start ()
	{
		clear ();
	}

	void getComponents ()
	{
	}

	void clear ()
	{
		planetNameTxt.text = string.Empty;
		planetRaceTxt.text = string.Empty;
		//planetCultureTxt.text = string.Empty;
		probeActionBtn.SetActive (false);
		invadeActionBtn.SetActive (false);

	}


	void updatePlanetText (object oPlanetInfo)
	{
		Planet p = (Planet) oPlanetInfo;
		clear ();
		//Depending on planet state, show different info
		evalDiscoveryLevel (p);
	}

	void updateSunText (object oSun)
	{
		Sun s = ((Sun)oSun);
		clear ();
		planetNameTxt.text = s.name;
	}

	void DisplayText (string displayStr, UnityEngine.UI.Text displayTxt)
	{
		if (displayStr == string.Empty) {
			displayTxt.enabled = false;
		} else {
			displayTxt.enabled = true;
			displayTxt.text = displayStr;
		}
	}

	void clearIcons (List<UICounterBase> li)
	{
		for (int l = 0; l < li.Count; l++) {
			li [l].gameObject.SetActive (false);
		}
	}

	void evalDiscoveryLevel (Planet planet)
	{
		//<>
		probeActionBtn.SetActive (true);
		ExplotationState discoThrs = planet.getExplotationState ();

		switch (discoThrs) {

		case ExplotationState.undiscovered:
			DisplayText ("?????", planetNameTxt);
			break;

		case ExplotationState.discovered:
			print ("Discovered");
			DisplayText (planet.name, planetNameTxt);
			break;

		case ExplotationState.probed:
			print ("Probed");
			probeActionBtn.SetActive (false);
			if (planet.hasCivilization) {
				invadeActionBtn.SetActive (true);
			}
			break;
		}

	}
}
