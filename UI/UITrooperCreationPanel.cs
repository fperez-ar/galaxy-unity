using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITrooperCreationPanel : MonoBehaviour {
	
	public InputField nameInput;
	public Text offensiveNum;
	public Text defensiveNum;
	public Text adaptabilityNum;
	public Text manpowerNum;
	public InputField manpowerInput;
	public PlayerShip pShip;
	public UIGeneticPanel geneticPanel;
	protected bool shown = false;
	private Animation anim;


	void Awake() {
		anim = GetComponent <Animation> ();
		if (!geneticPanel)  GetComponentInChildren <UIGeneticPanel> ();
		geneticPanel.onAdd += updateStats;
		geneticPanel.onRemove += updateStats;
		manpowerInput.onValueChanged.AddListener ( delegate{ updateStats(); } );

	}


	public void toggle() {
		if (!shown) {
			show ();
		} else {
			hide ();
		}
	}

	void show() {
		shown = true;
		geneticPanel.enabled = true;
		anim.Play ("in");
		clearFields ();
	}

	void hide() {
		shown = false;
		geneticPanel.enabled = false;
		anim.Play ("out");
	}

	public void hideViaButton() {
		hide ();
	}
		
	void clearFields() {
		nameInput.text = string.Empty;
		offensiveNum.text = "0";
		defensiveNum.text = "0";
		adaptabilityNum.text = "0";
		manpowerNum.text = "0";
		manpowerInput.text = "0";
		geneticPanel.Clear ();
	}


	public void Create() {
		//if (nameInput.text == string.Empty) 
		int population 	= int.Parse(manpowerInput.text);
		if (population <= 0) return;

		float off 		= float.Parse (offensiveNum.text);
		float def 		= float.Parse (defensiveNum.text);
		float adapt 	= float.Parse (adaptabilityNum.text);

		//remove used manpower, genetic and resources from player inventory
		pShip.dominantSpecies.population -= population;

		Troopers newTrup = new Troopers (nameInput.text, population)
		{ offensiveCap = off, defensiveCap = def, adaptability = adapt, morale = 100f, };

		//send troop to player inventory
		pShip.dominantSpecies.addTroops (newTrup);
		print (newTrup);
		EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
		clearFields ();
	}


	void updateStats() {
		print ("Updating stats...");
		manpowerInput.text = getPopulationValidated ();
		int popu = int.Parse (manpowerInput.text);
		offensiveNum.text 	 = getOffValidated  (popu, geneticPanel.getGenes ());
		defensiveNum.text 	 = getDefValidated  (popu, geneticPanel.getGenes ());
		adaptabilityNum.text = getAdaptValidated(popu, geneticPanel.getGenes ());
	}


	string getPopulationValidated() {
		if (manpowerInput.text == string.Empty)	return "0";

		int popCap = Mathf.Min (pShip.getPopulation(), BaseVals.maxTroopersPerUnit);
		int popInput = int.Parse (manpowerInput.text);

		return Mathf.Min (popInput, popCap ).ToString ();
	}

	string getOffValidated(int pop, GeneticTrait[] gs) {
		if (manpowerInput.text == string.Empty)	return "0";
		if (gs == null)	return "0";
		return CreationCalculator.calculateOffensive(pop, gs).ToString (BaseVals.StatsFormat);
	}

	string getDefValidated(int pop, GeneticTrait[] gs) {
		if (manpowerInput.text == string.Empty)	return "0";
		if (gs == null)	return "0";

		return CreationCalculator.calculateAdaptability(pop, gs).ToString (BaseVals.StatsFormat);
	}

	string getAdaptValidated(int pop, GeneticTrait[] gs) {
		if (manpowerInput.text == string.Empty)	return "0";
		if (gs == null)	return "0";

		return CreationCalculator.calculateAdaptability (pop, gs).ToString (BaseVals.StatsFormat);
	}

}
