using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomText {

	//TODO: Move to other class for generating text
	const string num = "0123456789";
	const string vowels = "aeiou";
	const string consonants = "bcdfghjklmñpqrstvwxyz";
	const string alfLower = "abcdefghijklmnopqrstuvwyxz";
	const string alf = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

	public static string getText(int len = 6){
		System.Text.StringBuilder sb = new System.Text.StringBuilder ();

		var rnd = new System.Random ();
		int r = -1;

		for (int i = 0; i < len; i++) {
			r = rnd.Next (0, alf.Length);
			sb.Append ( alf [ r ] );
		}
		return sb.ToString ();
	}

	public static string getNiceText(int len = 6, int vowelInterval = 2){
		System.Text.StringBuilder sb = new System.Text.StringBuilder ();

		var rnd = new System.Random ();
		int r = -1;

		r = rnd.Next (0, alfLower.Length);
		sb.Append ( alfLower[r].ToString ().ToUpper () );

		for (int i = 1; i < len-1; i++) {

			if ( i % vowelInterval == 0 ){
				r = rnd.Next (0, vowels.Length);
				sb.Append ( vowels [ r ] );
			}else{
				r = rnd.Next (0, alfLower.Length);
				sb.Append ( alfLower [ r ] );
			}
		}

		return sb.ToString ();
	}


	private static T rouletteWheelSelection<T>(float[] fitnessChance, T[] elements){
		float sum = 0;
		for (int i = 0; i < fitnessChance.Length; i++) { sum += fitnessChance[i]; }

		float fitness = Random.Range (0f, sum);
		float current = 0;

		for (int i = fitnessChance.Length - 1; i >= 0 ; i--) {
			current += fitnessChance [i];
			if ( current > fitness) {
				return elements [i];
			}
		}

		return default(T);
	}


	public static float[] esperantoChance = {0.1155f, 0.1040f, 0.905f, 0.886f, 0.775f, 0.615f, 0.597f, 0.588f, 0.537f, 0.408f, 0.331f, 0.301f, 0.299f, 0.291f, 0.291f, 0.181f, 0.132f, 0.107f, 0.103f, 0.100f, 0.069f, 0.060f, 0.054f, 0.050f, 0.042f, 0.036f, 0.009f, 0.01f};
	private static char[] esperantoAlphaLower 	= {'a', 'i',  'e',  'o',  'n',  'l',  's',  'r',  't',  'k',  'j',  'u',  'm',  'd',  'p',  'v',  'g',  'f',  'b',  'c',  'ĝ',  'ĉ',  'z',  'ŭ',  'h',  'ŝ',  'ĵ',  'ĥ'};

	private static char rouletteWheelEsperanto(bool lower = true){
		if ( esperantoAlphaLower.Length != esperantoChance.Length) Debug.LogWarning ("Esperanto alphabet length does not match chance length");
		float sum = 7.996499f;	//for (int i = 0; i < esperantoChance.Length; i++) { sum += esperantoChance [i]; }
		float fitness = Random.Range (0f, sum);
		float current = 0;

		for (int i = esperantoChance.Length - 1; i >= 0 ; i--) {
			current += esperantoChance [i];
			if ( current > fitness) {
				return esperantoAlphaLower [i];
			}
		}

		return ' ';
	}

	public static string getEsperanto(int minLen, int maxLen){
		int len = Random.Range (minLen, maxLen);
		char[] espName = new char[len];

		espName[0] =  char.ToUpper ( rouletteWheelEsperanto (false) );

		for (int i = 1; i < len; i++) {
			espName[i] = rouletteWheelEsperanto ();
		}

		return new string(espName);
	}

	public static float[] englishChance = {0.12702f, 0.9056f, 0.8167f, 0.7507f, 0.6966f, 0.6749f, 0.6327f, 0.6094f, 0.5987f, 0.4253f, 0.4025f, 0.2782f, 0.2758f, 0.2406f, 0.2360f, 0.2228f, 0.2015f, 0.1974f, 0.1929f, 0.1492f, 0.0978f, 0.0772f, 0.0153f, 0.0150f, 0.0095f, 0.0074f};
	public static char[] englishAlphaLower = {'e', 't', 'a', 'o', 'i', 'n', 's', 'h', 'r', 'd', 'l', 'c', 'u', 'm', 'w', 'f', 'g', 'y', 'p', 'b', 'v', 'k', 'j', 'x', 'q', 'z'};

	private static char rouletteWheelEnglish(bool lower = true){
		if ( englishAlphaLower.Length != englishChance .Length) Debug.LogWarning ("English alphabet length does not match chance length");

		float sum = 8.85672f; //for (int i = 0; i < englishChance.Length; i++) { sum += englishChance [i]; }
		float fitness = Random.Range (0f, sum);
		float current = 0;

		for (int i = englishChance.Length - 1; i >= 0 ; i--) {
			current += englishChance  [i];
			if ( current > fitness) {
				return englishAlphaLower [i];
			}
		}

		return ' ';
	}

	public static string getEnglish(int minLen, int maxLen){
		int len = Random.Range (minLen, maxLen);
		char[] engName = new char[len];

		engName[0] = char.ToUpper ( rouletteWheelEnglish (false) );

		for (int i = 1; i < len; i++) {
			engName[i] = rouletteWheelEnglish ();
		}

		return new string(engName);
	}

	public static string getBisilableText(int minLen = 2, int maxLen = 8){

		minLen = (minLen % 2 == 0)? minLen : minLen+1;
		int len = Random.Range (minLen, maxLen);
		StringBuilder sb = new StringBuilder ();

		char v = vowels[ Random.Range (0, vowels.Length) ];
		char c = consonants [Random.Range (0, consonants.Length)];

		if (Random.value < 0.5f){
			sb.Append ( char.ToUpper (v) );
		}else{
			sb.Append ( char.ToUpper (c) );
		}

		for (int i = 1; i < len/2; i++) {
			v = vowels[ Random.Range (0, vowels.Length) ];
			c = consonants [Random.Range (0, consonants.Length)];

			if (Random.value < 0.5f){
				sb.Append (v);
				sb.Append (c);
			}else{
				sb.Append (c);
				sb.Append (v);
			}
		}

		return sb.ToString ();
	}

	private static string[] tokipona_words;
	public static string getTokiPonaWord(){
		
		if (tokipona_words == null) {
			tokipona_words = File.ReadAllLines (Filenames.resourcesDir + Filenames.TokiPonaDict, Encoding.UTF8);
		}

		int l = Random.Range (0, tokipona_words.Length);
		return  tokipona_words[l];
	}

	public static string reallyRandomText(int minLen, int maxLen){
		float r = Random.value;
		if (r < 0.25f) { 		//0 - .24
			//Debug.Log ("getEnglish ");
			return getTokiPonaWord ();
		} else if (r < 0.5) { 	//.25 - .49
			//Debug.Log ("getEsperanto ");
			return getEsperanto (minLen, maxLen);
		} else if (r < 0.75) { 	//.5 - .74
			//Debug.Log ("getNiceText ");
			return getNiceText (minLen, 2);
		} else if (r < 0.9) { 	//.75 - .89
			return getEnglish (minLen, maxLen);
		}

		return getText ( Random.Range (minLen, maxLen));
	}
}
