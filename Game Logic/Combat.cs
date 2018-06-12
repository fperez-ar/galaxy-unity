using System;
using UnityEngine;
public enum CombatResult{
	AWon,
	ALost
}

public class Combat {
	
	public static float defLostModifier = 0.3f;
	public static float defWinModifier = 0.1f;

	public static float attLostModifier = 0.2f;
	public static float attWinModifier = 0.1f;

	public static float moraleDmgModifier = 0.05f;
	//public static float attLostMoraleMod = 0.05f;
	//public static float defLostMoraleMod = 0.05f;



	public static CombatResult CCombat(Species attackingSp, Species defendingSp, Troopers attTroops, Troopers defTroops){
		//Troopers A = attacking.getTroops (attTroops);
		//Troopers B = defending.getTroops (defTroops);

		Culture cA = attackingSp.culture;
		Culture cB = defendingSp.culture;

		float offAdaptMod = 1f + attTroops.adaptability;
		float defAdaptMod = 1f + defTroops.adaptability;
		float off = attTroops.offensiveCap * offAdaptMod;
		float def = defTroops.defensiveCap * defAdaptMod;

		int dTech = cA.technology - cB.technology;
		int dAggr = cA.agressiveness - cB.agressiveness;

		float dmg = (off - def) + dTech + dAggr;

		Debug.Log ("Offensive: "+off);
		Debug.Log ("Defensive:" +def);
		Debug.Log ("delta Tech: "+dTech);
		Debug.Log ("delta Aggr: "+dAggr);
		Debug.Log ("damage: "+dmg);

		CombatResult res;
		if (dmg > 0) {		//Attackers WON
			attTroops.applyDamage (dmg * defLostModifier );
			defTroops.applyDamage (dmg * attWinModifier );
			//attackingSp.applyDamage ( dmg * defLostModifier);
			//defendingSp.applyDamage ( dmg * attWinModifier );
			res = CombatResult.AWon;
		} else { //dmg <= 0 //Attackers LOST

			float attDmg = Math.Abs (dmg) * attLostModifier + (100 - attTroops.morale) * moraleDmgModifier;
			attTroops.applyDamage ( attDmg );
			defTroops.applyDamage (  Math.Abs(dmg) * defWinModifier );
			//attackingSp.applyDamage ( Math.Abs (dmg) * attLostModifier + (100 - attTroops.morale) * attLostMoraleMod );
			//defendingSp.applyDamage ( Math.Abs(dmg) * defWinModifier );
			res = CombatResult.ALost;
		}
		//TODO: Resolve damage to morale
		MoraleDamage (attackingSp, defendingSp, attTroops, defTroops);

		//TODO: enable invasion if defenders lost
		return res;
	}

	private static void MoraleDamage(Species attackingSp, Species defendingSp, Troopers attTroops, Troopers defTroops) {
		
		float deltaT = defendingSp.culture.technology - attackingSp.culture.technology;
		float deltaA = defendingSp.culture.agressiveness - attackingSp.culture.agressiveness ;
		float moraleDmg = (deltaT + deltaA) * moraleDmgModifier;
		attTroops.morale += moraleDmg;
		Debug.Log (string.Format ("Morale damage {0} to {1}", moraleDmg, attTroops ));

		deltaT = attackingSp.culture.technology - defendingSp.culture.technology;
		deltaA = attackingSp.culture.agressiveness - defendingSp.culture.agressiveness;
		moraleDmg = (deltaT + deltaA) * moraleDmgModifier;

		defTroops.morale += moraleDmg;
		Debug.Log (string.Format ("Morale damage {0} to {1}", moraleDmg, defTroops ));
	}

	public static float Delta(float a, float b) {
		return Mathf.Abs (a - b);
	}

}
