using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInvDisplay : MonoBehaviour {

	public PlayerShip pShip;
	public Text playerRaceTxt;

	public GameObject genesPanel;
	private int gi = 0; 
	private List<CounterBase> genesIconsLi = new List<CounterBase>();

	public GameObject resourcesPanel;
	private int ri = 0; 
	private List<CounterBase> resourceIconsLi = new List<CounterBase>();

	public GameObject troopersPanel;
	private int ti = 0; 
	private List<CounterBase> troopersCounterLi = new List<CounterBase>();



	void Awake() {
		getComponents ();
		EvHandler.RegisterEv (UIEvent.UPDATE_INV, updateInv);
	}


	void updateInv() {
		playerRaceTxt.text = pShip.dominantSpecies.name;

		DisplayText (pShip.dominantSpecies.name, playerRaceTxt);
		RefreshIcons (pShip.dominantSpecies.gen.getArray ());
		RefreshIcons (pShip.resources.getArray ());
		RefreshIcons (pShip.dominantSpecies.man.getArray ());
	}

	void DisplayText(string displayStr, Text displayTxt) {
		if (displayStr == string.Empty) {
			displayTxt.enabled = false;
		} else {
			displayTxt.enabled = true;
			displayTxt.text = displayStr;
		}
	}

	void RefreshIcons(GeneticTrait[] traits) {
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

	void RefreshIcons(Troopers[] ts) {
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
		
	void add(ICounterable c, List<PlyCounter> li, int liIndex) {
		li [liIndex].setCounter (c);
		liIndex++;
	}

	void getComponents() {
		//geneticInventoryTxt = genesPanel.GetComponentInChildren<Text> ();
		genesIconsLi.AddRange ( genesPanel.GetComponentsInChildren<CounterBase> ());

		//resourcesInventoryTxt = resourcesPanel.GetComponentInChildren<Text> ();
		resourceIconsLi.AddRange ( resourcesPanel.GetComponentsInChildren<CounterBase> ());

		//troopersInventoryTxt = troopersPanel.GetComponentInChildren<Text> ();
		troopersCounterLi.AddRange ( troopersPanel.GetComponentsInChildren<CounterBase> ());
		//.GetComponentInChildren<> ();
	}

}
