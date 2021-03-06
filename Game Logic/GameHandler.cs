﻿using System.Collections;
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
	public PlanetIntelligence pI;
	public PlayerShip pShip;

	#if UNITY_EDITOR
	public Transform debug_planet;
	#endif

	void Awake ()
	{
		//Application.wantsToQuit += ;
		anim.awake ();
		pI.awake ();
		Load();

		EvHandler.RegisterEv (GameEvent.ORBIT_PLT, selectBody);
		EvHandler.RegisterEv (GameEvent.SELECT_BODY, selectBody);
		EvHandler.RegisterEv (GameEvent.MOD_RES, modResource);

		EvHandler.RegisterEv (UIEvent.SHOW_PROBE_PANEL, setUseProbePanel);
		EvHandler.RegisterEv (GameEvent.PROBE_PLT, probePlanet);
		//EvHandler.RegisterEv (GameEvent.MINE_PLT, minePlanet);
		EvHandler.RegisterEv (GameEvent.INVADE_PLT, invadePlanet);
		EvHandler.RegisterEv (GameEvent.RESOLVE_CBT_PHASE, resolveCombat);
		EvHandler.RegisterEv (UIEvent.ANIM_IDLE, IdleAnim);
		EvHandler.RegisterEv (UIEvent.ANIM_CON, ContinueAnim);
		//EvHandler.RegisterEv (UIEvent.SHOW_PLANET_INFO, setCurrentPlanet);
		GameMode.setMode (GameState.NAVEGATION);
		#if UNITY_EDITOR
			debug();
		#endif
	}
	#if UNITY_EDITOR
	void debug()
	{
		Invoke ("debugMoveTo", 0.5f);
		Invoke ("debugInvade", 1f);
	}
	void debugMoveTo () { anim.MoveTo (debug_planet); }
	void debugInvade () { EvHandler.ExecuteEv(GameEvent.INVADE_PLT); }
	#endif
	void Update ()
	{
		pI.update ();
	}

	void FixedUpdate ()
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

	#region planet-interaction
	void selectBody (object oBody)
	{
		if (oBody is Planet) {
			planetSelectd = (Planet)oBody;
			pI.setCurrentPlanet (planetSelectd);

			EvHandler.ExecuteEv (UIEvent.SHOW_PLANET_INFO, planetSelectd);
		} else if (oBody is Sun) {
			EvHandler.ExecuteEv (UIEvent.SHOW_SUN_INFO, oBody);
		}
	}

	// TODO: Move all setting up ui elems code to a composite class
	public void setUseProbePanel ()
	{
		if (pShip.hasResources (BaseVals.ResProbes))
		{
			EvHandler.ExecuteEv (UIEvent.SHOW_PROBE_PANEL, pShip.getResource (BaseVals.ResProbes));
		} else {
			EvHandler.ExecuteEv (UIEvent.SHOW_AUTOFADE_TOOLTIP, "No probes available");
		}
	}

	void probePlanet (object oQuantityProbes)
	{
		//when probing, you already orbit the planet and thus have it selected...
		int probeQ = (int) oQuantityProbes;//pShip.getResource (BaseVals.ResProbes).quantity;
		if (!pShip.hasResources (BaseVals.ResProbes, probeQ))
			throw new System.Exception ("Attempting to use more probes than actually available");

		if (planetSelectd.getExplotationState() > ExplotationState.researched) {
			print ("Probing level: "+(int)planetSelectd.getExplotationState());
			EvHandler.ExecuteEv (UIEvent.SHOW_AUTOFADE_TOOLTIP, "No more information available from probes.");
			return;
		}

		pShip.modifyResource(BaseVals.ResProbes, -probeQ);
		planetSelectd.probe (probeQ);
		EvHandler.ExecuteEv (UIEvent.SHOW_PLANET_INFO, planetSelectd);
		EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
	}

	void invadePlanet ()
	{
		if ( GameMode.isMode(GameState.COMBAT) ) return;
		phase = GamePhase.preparation;
		int atkT = pShip.dominantSpecies.culture.technology;
		int defT = planetSelectd.dominantSpecies.culture.technology;

		GameMode.setMode(GameState.COMBAT);
		EvHandler.ExecuteEv (UIEvent.SHOW_COMBAT_PANEL);
		EvHandler.ExecuteEv (GameEvent.PREPARATION_PHASE);
		EvHandler.ExecuteEv (GameEvent.PREPARATION_PHASE, Mathf.Abs (atkT - defT));
		UpdateCombatPanel ();
	}
	#endregion

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
		for (int i = 0; i < troopNames.Length; i++)
		{
			print ("A adding " + troopNames [i]);
			troopArray [i] = sp.getTroops (troopNames [i]);
		}

		return Troopers.sum (troopArray);
	}

	void modResource(object oRes)
	{
		ResourceBase r = (ResourceBase) oRes;
		print("adding "+r);
		//TODO: Queue the popups or show a list of all resoources gained...
		EvHandler.ExecuteEv (UIEvent.SHOW_AUTOFADE_TOOLTIP, "Obtained "+r);
		pShip.addResource(r);
		EvHandler.ExecuteEv(UIEvent.UPDATE_INV);
	}

	#region save/load management
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
		print("Saving...");
		SaveManager.Save (pShip, quit);
	}

	public void quit ()
	{
		Save ();
		Application.Quit ();
	}

	#endregion
}
