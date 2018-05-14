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

	public static float attLostMoraleMod = 0.05f;
	public static float defLostMoraleMod = 0.05f;

	public static bool SimpleCombat(int atkQ, int defQ){
		return atkQ > defQ;
	}

	public static CombatResult CCombat(Species attacking, Species defending, Troopers attTroops, Troopers defTroops){
		//Troopers A = attacking.getTroops (attTroops);
		//Troopers B = defending.getTroops (defTroops);

		Culture cA = attacking.culture;
		Culture cB = defending.culture;

		float off = (int) (attTroops.offensiveCap * attTroops.adaptability);
		float def = (int) (defTroops.defensiveCap * defTroops.adaptability);

		int dTech = cA.technology - cB.technology;
		int dAggr = cA.agressiveness - cB.agressiveness;

		float dmg = (off - def) + dTech + dAggr;

		Debug.Log ("Offensive: "+off);
		Debug.Log ("Defensive:" +def);
		Debug.Log ("delta Tech: "+dTech);
		Debug.Log ("delta Aggr: "+dAggr);
		Debug.Log ("damage: "+dmg);

		if (dmg > 0) {		//Attackers WON
			attacking.applyDamage ( dmg * defLostModifier);
			defending.applyDamage ( dmg * attWinModifier );
			return CombatResult.AWon;
		} else { //dmg <= 0 //Attackers LOST
			attacking.applyDamage ( Math.Abs (dmg) * attLostModifier + (100 - attTroops.morale) * attLostMoraleMod );
			defending.applyDamage (  Math.Abs(dmg) * defWinModifier );
			return CombatResult.ALost;
		}

		//TODO: enable invasion if defenders lost

		//TODO: Resolve damage to morale
	}

}
