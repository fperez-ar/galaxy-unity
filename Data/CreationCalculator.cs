using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CreationCalculator{

	static float offModifier;
	static float defModifier;
	static float adaModifier = 0.075f;

	static float getBaseStat(int basePop){
		
		//return Mathf.Log (basePop, 3f);
		//return Mathf.Log10 (basePop);
		// * 0.24f + Mathf.Log (basePop*0.5f);
		return basePop * 0.5f;
	}

	public static float calculateOffensive(int pop, GeneticTrait[] gs) {
		if (gs == null)	return 0f;

		float accum = getBaseStat (pop);
		for (int i = 0; i < gs.Length; i++) {
			accum += pop * gs [i].offensiveCapModificator;
		}
		return accum;
	}

	public static float calculateDefensive(int pop, GeneticTrait[] gs) {
		if (gs == null)	return 0f;

		float accum = getBaseStat (pop);
		for (int i = 0; i < gs.Length; i++) {
			accum += pop * gs [i].defensiveCapModificator;
		}
		return accum;
	}

	public static float calculateAdaptability(int pop, GeneticTrait[] gs) {
		if (gs == null)	return 0f;

		float accum = 0;
		for (int i = 0; i < gs.Length; i++) {
			accum += gs [i].adaptability * adaModifier;
		}
		return accum;
	}
}
