using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
	public enum GamePhase
	{
		explore,
		orbit,
		preparation, //pre-attack
		attack,
	};

	public GamePhase phase;
	private DiscoveryHistory discHistory;
	private Planet planetSelectd;
	public AnimHandler anim;
	public PlanetIntelligence pI;
	public PlayerShip pShip;
	private int initialDiscoveryLvl = 1;

	#if UNITY_EDITOR
	public Transform debug_planet;
	#endif

	void Awake ()
	{
		//Application.wantsToQuit += ;
		anim.awake ();
		pI.awake ();
		discHistory = new DiscoveryHistory ();
		EvHandler.RegisterEv (GameEvent.ORBIT_PLT, selectBody);
		EvHandler.RegisterEv (GameEvent.SELECT_BODY, selectBody);
		EvHandler.RegisterEv (GameEvent.PROBE_PLT, probePlanet);
		EvHandler.RegisterEv (GameEvent.MINE_PLT, minePlanet);
		EvHandler.RegisterEv (GameEvent.INVADE_PLT, invadePlanet);
		EvHandler.RegisterEv (GameEvent.RESOLVE_CBT_PHASE, resolveCombat);
		EvHandler.RegisterEv (UIEvent.ANIM_IDLE, IdleAnim);
		EvHandler.RegisterEv (UIEvent.ANIM_CON, ContinueAnim);
		//EvHandler.RegisterEv (UIEvent.SHOW_PLANET_INFO, setCurrentPlanet);
		GameMode.setMode (GameState.NAVEGATION);
		#if UNITY_EDITOR
		Invoke ("debug", 0.5f);
		#endif
	}

	void debug(){
		anim.MoveTo (debug_planet);
	}

	void Update()
	{
		pI.update ();
	}

	void FixedUpdate()
	{
		anim.update ();//camera work should go to lateupdate if it jitters
	}

	void IdleAnim ()
	{
		anim.Suspend ();
	}

	void ContinueAnim ()
	{
	}


	void selectBody(object oBody)
	{
		if (oBody is Planet) {
			planetSelectd = (Planet)oBody;
			pI.setCurrentPlanet (planetSelectd);
			discHistory.add (planetSelectd.name, initialDiscoveryLvl);
			EvHandler.ExecuteEv (UIEvent.SHOW_PLANET_INFO, planetSelectd);
		} else if (oBody is Sun) {
			EvHandler.ExecuteEv (UIEvent.SHOW_SUN_INFO, oBody);
		}
	}

	void probePlanet ()
	{
		print("Attempting to probe");
		//when probing, you already orbit the planet and thus have it selected...

		planetSelectd.probe ();
		//move following to exploitation probe method, determine how to act on it
		//TODO: Mining now depends on: probe q + discovery lvl / plt eff 
		if (pShip.hasResources (BaseVals.ResProbes, probeDiff)) {
			pShip.modifyResource (BaseVals.ResProbes, -probeDiff);

			EvHandler.ExecuteEv (UIEvent.SHOW_PLANET_INFO, planetSelectd);
		} else {
			print("No probes");
			EvHandler.ExecuteEv (UIEvent.SHOW_AUTOFADE_TOOLTIP, "No probes available.");
		}

	}

	void minePlanet ()
	{
		//consume probes to get resources modified (-/+|*) by mine efficiency
		//int mineEff = planetSelectd.getMineEfficiency ();

		var rs = planetSelectd.resources.getArray ();
		pShip.resources.addRange (rs);
		planetSelectd.resources.clear ();
		EvHandler.ExecuteEv (UIEvent.UPDATE_INV);

	}

	void invadePlanet ()
	{
		phase = GamePhase.preparation;
		int atkT = pShip.dominantSpecies.culture.technology;
		int defT = planetSelectd.dominantSpecies.culture.technology;

		EvHandler.ExecuteEv (UIEvent.SHOW_COMBAT_PANEL);
		EvHandler.ExecuteEv (GameEvent.PREPARATION_PHASE);
		EvHandler.ExecuteEv (GameEvent.PREPARATION_PHASE, Mathf.Abs (atkT - defT));

		UpdateCombatPanel ();
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

	void UpdateCombatPanel ()
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
		SaveManager.Save (pShip, quit);
	}

	public void quit ()
	{
		Save ();
		Application.Quit ();
	}
}
