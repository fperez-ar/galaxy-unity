
using UnityEngine;
using UnityEngine.UI;

public class UICombatPanel : MonoBehaviour {

	public Text plyRace;
	public Text plyCombatStrength;
	public Text pltRace;
	public Text pltCombatStrength;
	private Animation anim;
	public UICombatSubPanelPlayer plyCombatPanel;
	public UICombatSubPanelPlanet pltCombatPanel;

	void Awake () {
		if (!anim) anim = GetComponent<Animation> ();
		if (!plyCombatPanel) plyCombatPanel = GetComponentInChildren <UICombatSubPanelPlayer> ();
		if (!pltCombatPanel) pltCombatPanel = GetComponentInChildren <UICombatSubPanelPlanet> ();
		plyCombatPanel.awake();
		pltCombatPanel.awake();

		EvHandler.RegisterEv (UIEvent.SHOW_COMBAT_PANEL, showCombatPanel);
		//EvHandler.RegisterEv (UIEvent.HIDE_COMBAT_PANEL, hideCombatPanel);
		EvHandler.RegisterEv (UIEvent.UPDATE_COMBAT_INFO, updatePanel);

		EvHandler.RegisterEv (GameEvent.COMBAT_CALCULATION, combatCalc);
		EvHandler.RegisterEv (GameEvent.AWON, animWon);
		EvHandler.RegisterEv (GameEvent.ALOST, animLost);
	}

	void showCombatPanel(){
		//plyCombatPanel.clear();
		//pltCombatPanel.clear();
		anim.CrossFade("combat_panel_in", 0.2f);
	}

	void hideCombatPanel(){
		anim.CrossFade ("combat_panel_out", 0.2f);
	}

	void animWon(){
		plyCombatStrength.text = "WON";
		pltCombatStrength.text = "LOST";
		//anim.Play ("combat_panel_awon");
	}

	void animLost(){
		plyCombatStrength.text = "LOST";
		pltCombatStrength.text = "WON";
		//anim.Play ("combat_panel_alost");
	}

	public void HideViaButton(){
		EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
		hideCombatPanel ();
		GameMode.setMode(GameState.NAVEGATION);
	}

	void updatePlayerSide()
	{
	}


	void updatePlanetSide()
	{
	}

	void combatCalc()
	{
		string[] plyTrpNames = plyCombatPanel.getTroopsNames();
		if (plyTrpNames.Length == 0)
			return;

		string[] pltTrpNames = pltCombatPanel.getTroopsNames ();
		if (pltTrpNames.Length == 0)
			EvHandler.ExecuteEv (GameEvent.AWON);

		object[] objArray = new object[]{plyTrpNames, pltTrpNames};
		EvHandler.ExecuteEv (GameEvent.RESOLVE_CBT_PHASE, objArray); //Relay info on troops names
	}

	void updatePanel(object objParams)
	{
		Species atking = (Species) ((object[])objParams) [0];

		if (atking) {
			plyRace.text = atking.name;
			plyCombatStrength.text = "placeholder";
		}

		Species defing = (Species) ((object[])objParams) [1];

		if (defing) {
			//TODO: actually determine which troops go into combat for the planet
			pltRace.text = defing.name;
			pltCombatStrength.text = "placeholder";
		}
	}


}
