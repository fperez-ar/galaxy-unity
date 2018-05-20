using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInvDisplay : MonoBehaviour {

	public PlayerShip pShip;
	public Text playerRaceTxt;
	public Text playerPopText;
	void Awake() {
		
		EvHandler.RegisterEv (UIEvent.UPDATE_INV, updateInv);
	}

	void Start() {
		updateInv ();
	}

	void updateInv() {
		playerRaceTxt.text = pShip.dominantSpecies.name;

		DisplayText (pShip.dominantSpecies.name, playerRaceTxt);
		DisplayText (pShip.getPopulation (), playerPopText);
	}

	void DisplayText(int displayStr, Text displayTxt) {
		DisplayText (displayStr.ToString (), displayTxt);
	}
	void DisplayText(string displayStr, Text displayTxt) {
		if (displayStr == string.Empty) {
			displayTxt.enabled = false;
		} else {
			displayTxt.enabled = true;
			displayTxt.text = displayStr;
		}
	}

}
