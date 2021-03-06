﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetIntelligence : MonoBehaviour {

	//TODO: Change to fsm
	public enum LogicState{
		idle, combat_deliberation
	}
	public LogicState currentState = LogicState.idle;

	public UICombatSubPanelPlanet pltCombatPanel;
	public PlanetInvDisplay pltInv;
	private Planet currentPlanet;
	private Species currSpecies { get {return currentPlanet.dominantSpecies;} }
	private Culture currCulture { get {return currentPlanet.dominantSpecies.culture;} }
	private Timer deliberationTimer;
	public float baseDeliberationTime = 10;

	public void awake()
	{
		EvHandler.RegisterEv (GameEvent.PREPARATION_PHASE, enterCombat);
	}

	public void setCurrentPlanet(Planet p)
	{
		currentPlanet = p;
	}

	void enterCombat()
	{
		float t = (currCulture.technology / BaseVals.Technology);
		deliberationTimer = new Timer ( baseDeliberationTime -t);
		print ("waiting for "+t);
		currentState = LogicState.combat_deliberation;
	}

	void chooseForCombat()
	{
		UnityEngine.Debug.LogWarning ("continue FIXing ME");
		//TODO: get available troopsn
		List<Troopers> availableTrps = currentPlanet.dominantSpecies.man.getList(); //?????

		if (availableTrps.Count == 0)
			throw new System.Exception ("NO TROOPS AVAILABLE IN "+currentPlanet);

		int len = availableTrps.Count;
		//based on species culture agressiveness will be max of troops selected
		int aggro = len - currCulture.agressiveness;
		int q = Random.Range (1, aggro);

		int indx = 0;
		//Debug.Log ("q: " + q + " a: " + aggro);
		aggro = (aggro <= 0) ? 1 : aggro;

		int[] indxs = RandomExt.rndNonRepeatingIndexes (q, aggro);

		Debug.Log ("q of troopers selected by p.i.: "+indxs.Length);
		for (int i = 0; i < indxs.Length; i++) {
			Debug.Log ("idx: "+indx);
			indx = indxs [i];
			pltCombatPanel.add ( availableTrps[indx] );
			UnityEngine.Debug.LogWarning ("FIX ME");
		}
	}

	public void update(){
		if (currentState.Equals (LogicState.combat_deliberation)) {
			if (deliberationTimer.check ()) {
				chooseForCombat ();
				currentState = LogicState.idle;
			}
		}
	}
}
