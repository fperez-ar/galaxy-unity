using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInvDisplay : MonoBehaviour {

	public UnityEngine.UI.Text planetNameTxt;
	public UnityEngine.UI.Text planetRaceTxt;
	//public UnityEngine.UI.Text planetCultureTxt;
	//public UnityEngine.UI.Text planetResourcesTxt;


	private int gi = 0;
	public GameObject genesPanel;
	private List<CounterBase> genesIconsLi = new List<CounterBase>();

	private int ri = 0; 
	public GameObject resourcesPanel;
	private List<CounterBase> resourceIconsLi = new List<CounterBase>();

	private int ti = 0; 
	public GameObject troopersPanel;
	private List<CounterBase> troopersCounterLi = new List<CounterBase>();

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
		genesIconsLi.AddRange ( genesPanel.GetComponentsInChildren<CounterBase> ());
		resourceIconsLi.AddRange ( resourcesPanel.GetComponentsInChildren<CounterBase> ());
		troopersCounterLi.AddRange ( troopersPanel.GetComponentsInChildren<CounterBase> ());
	}

	void clear() {
		planetNameTxt.text = string.Empty;
		planetRaceTxt.text = string.Empty;
		//planetCultureTxt.text = string.Empty;
		invadeActionBtn.SetActive ( false );
		mineActionBtn.SetActive ( false );
		clearIcons (genesIconsLi);
		clearIcons (resourceIconsLi);
		clearIcons (troopersCounterLi);
	}


	void updatePlanetText(object oPlanet){
		Planet p = (Planet) oPlanet;
		clear ();

		DisplayText (p.name, planetNameTxt);

		if (p.resources.resourceQuantity > 0) {
			mineActionBtn.SetActive ( true );
			RefreshIcons (p.resources.getArray ());
		}

		if (p.civilization || p.dominantSpecies != null) {
			invadeActionBtn.SetActive (true);
			DisplayText (p.dominantSpecies.name, planetRaceTxt);
			RefreshIcons (p.dominantSpecies.gen.getArray ());
			RefreshIcons (p.dominantSpecies.man.getArray ());
		}
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

	void RefreshIcons(GeneticTrait[] traits){
		if (traits.Length > genesIconsLi.Count) Debug.LogError ("More traits than icons available to display");
		for (int i = 0; i < traits.Length; i++) {
			genesIconsLi [i].gameObject.SetActive (true);
			genesIconsLi [i].setCounter ( (ICounterable) traits[i] );
		}

		gi = traits.Length;
		for (int l = gi; l < genesIconsLi.Count; l++) {
			genesIconsLi [l].gameObject.SetActive (false);
		}
	}

	void RefreshIcons(ResourceBase[] res){
		if (res.Length > resourceIconsLi.Count) Debug.LogError ("More resources than icons available to display");
		for (int i = 0; i < res.Length; i++) {
			resourceIconsLi [i].gameObject.SetActive (true);
			resourceIconsLi[i].setCounter ( (ICounterable) res[i] );
		}
		ri = res.Length;
		for (int l = ri; l < resourceIconsLi.Count; l++) {
			resourceIconsLi [l].gameObject.SetActive (false);
		}
	}

	void RefreshIcons(Troopers[] ts){
		if (ts.Length > troopersCounterLi.Count) Debug.LogError ("More troopers than icons available to display");
		for (int i = 0; i < ts.Length; i++) {
			troopersCounterLi [i].gameObject.SetActive (true);
			troopersCounterLi [i].setCounter ( (ICounterable) ts[i] );
		}

		ti = ts.Length;
		for (int l = ti; l < troopersCounterLi.Count; l++) {
			troopersCounterLi [l].gameObject.SetActive (false);
		}
	}

	void RefreshIcon(ICounterable c, List<CounterBase> li, int liIndex){
		li [liIndex].setCounter (c);
	}

	void ForceResetPos(List<CounterBase> counters) {
		for (int i = 0; i < counters.Count; i++) {
			counters [i].transform.SetParent (transform);
			counters [i].ForceResetPos ();
		}
	}

	void clearIcons(List<CounterBase> li) {
		for (int l = 0; l < li.Count; l++) {
			li [l].gameObject.SetActive (false);
		}
	}

	public List<CounterBase> getAvailableTroopCounters(){		
		return troopersCounterLi.FindAll (el => el.gameObject.activeSelf);
	}

}
