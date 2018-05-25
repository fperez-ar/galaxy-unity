using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
	public enum GamePhase
	{
		explore,
		orbit,
		preparation,
		//pre-attack
		attack,
	};

	public GamePhase phase;
	private Planet planetSelectd;
	public AnimHandler anim;
	public PlanetIntelligence planetInt;
	public PlayerShip pShip;
	public Timer time;

	void Awake ()
	{
		anim.awake ();
		EvHandler.RegisterEv (GameEvent.MINE_PLT, minePlanet);
		EvHandler.RegisterEv (GameEvent.INVADE_PLT, invadePlanet);
		EvHandler.RegisterEv (GameEvent.RESOLVE_CBT_PHASE, resolveCombat);

		EvHandler.RegisterEv (UIEvent.ANIM_IDLE, IdleAnim);
		EvHandler.RegisterEv (UIEvent.ANIM_CON, ContinueAnim);
		EvHandler.RegisterEv (UIEvent.SHOW_PLANET_INFO, setCurrentPlanet);

	}


	void IdleAnim ()
	{
		anim.Suspend ();
	}

	void ContinueAnim ()
	{
	}

	void setCurrentPlanet (object oPlanet)
	{
		//THIS MIGHT NOT WORK IF THE LOGIC FOR DISPLAYING PLANET INFO CHANGES
		planetSelectd = (Planet)oPlanet;
		planetInt.setCurrentPlanet (planetSelectd);

	}

	void probePlanet (object oPlanet)
	{
		//THIS MIGHT NOT WORK IF THE LOGIC FOR DISPLAYING PLANET INFO CHANGES
		planetSelectd = (Planet)oPlanet;
		if (pShip.hasResources ("probe")) {
			pShip.removeResource ("probe");
		}
		//TODO: SHOW PLANET INFO
	}

	void minePlanet ()
	{
		var rs = planetSelectd.resources.getArray ();

		pShip.resources.addRange (rs);
		planetSelectd.resources.clear ();

		EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
		EvHandler.ExecuteEv (UIEvent.SHOW_PLANET_INFO, planetSelectd);

	}

	void invadePlanet ()
	{
		phase = GamePhase.preparation;
		int atkT = pShip.dominantSpecies.culture.technology;
		int defT = planetSelectd.dominantSpecies.culture.technology;

		EvHandler.ExecuteEv (UIEvent.SHOW_COMBAT_PANEL);
		EvHandler.ExecuteEv (GameEvent.PREPARATION_PHASE);
		EvHandler.ExecuteEv (GameEvent.PREPARATION_PHASE, Mathf.Abs (atkT - defT));

		UpdatePanel ();
	}

	void resolveCombat (object oTrps)
	{
		Species atkSp = pShip.dominantSpecies;			
		Species defSp = planetSelectd.dominantSpecies;

		object[] troopNames = (object[])oTrps;

		string[] attTrps = (string[])troopNames [0];
		if (attTrps.Length == 0)
			return; //TODO: Show tooltip

		string[] defTrps = (string[])troopNames [1];
		if (defTrps.Length == 0)
			EvHandler.ExecuteEv (GameEvent.AWON);

		Troopers finalAtk = Sum (atkSp, attTrps);
		Troopers finalDef = Sum (defSp, defTrps);

		Debug.Log (finalAtk);
		Debug.Log (finalDef);

		CombatResult ccbtResult = Combat.CCombat (atkSp, defSp, finalAtk, finalDef);
		if (ccbtResult.Equals (CombatResult.AWon)) {
			EvHandler.ExecuteEv (GameEvent.AWON);
		} else {
			EvHandler.ExecuteEv (GameEvent.ALOST);
		}
		//EvHandler.ExecuteEv (UIEvent.HIDE_COMBAT_PANEL);
	}

	void UpdatePanel ()
	{
		Species atk = pShip.dominantSpecies;
		Species def = planetSelectd.dominantSpecies;
		EvHandler.ExecuteEv (UIEvent.UPDATE_COMBAT_INFO, new object[2]{ atk, def });
	}

	private Troopers Sum (Species sp, string[] troopNames)
	{
		int len = troopNames.Length;
		Troopers[] troopArray = new Troopers[len];
		for (int i = 0; i < troopNames.Length; i++) {
			print ("A adding " + troopNames [i]);
			troopArray [i] = sp.getTroops (troopNames [i]);
		}

		return Troopers.sum (troopArray);
	}

	public void Save ()
	{
		SaveManager.Save (pShip);
	}

	public void Load ()
	{
		SaveManager.Load (pShip);
	}

	public void OnApplicationPause ()
	{
		Save ();
	}

	public void OnApplicationQuit ()
	{
		Application.CancelQuit ();
		SaveManager.Save (pShip, quit);
	}

	public void quit ()
	{
		Save ();
		Application.Quit ();
	}
}
