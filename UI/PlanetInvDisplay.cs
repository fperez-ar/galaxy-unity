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
	public GameObject mineActionBtn;
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
		mineActionBtn.SetActive (false);
	}


	void updatePlanetText (object oPlanetInfo)
	{
		Planet p = (Planet) ((object[])oPlanetInfo)[0];
		int discoLvl = (int)((object[])oPlanetInfo)[1];
		clear ();
		//Depending on planet state, show different info
		evalDiscoveryLevel (p, discoLvl);
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

	void evalDiscoveryLevel (Planet planet, int discoLvl)
	{
		//<>
		print (discoLvl);
		DisplayText ("?????", planetNameTxt);
		if (discoLvl >= DiscoveryProgress.Discovered ) {
			print ("Discovered");
			DisplayText (planet.name, planetNameTxt);
		} 
		if (discoLvl >= DiscoveryProgress.Probed ) {
			print ("Probed");
			//show resources tab
			mineActionBtn.SetActive (true);
			//show invade/combat btn if it's civilized
			if (planet.hasCivilization) 
			{
				invadeActionBtn.SetActive (true);
			}
		} 
		if (discoLvl  <= DiscoveryProgress.Invaded) {
			probeActionBtn.SetActive (true);
		}
	}
}
