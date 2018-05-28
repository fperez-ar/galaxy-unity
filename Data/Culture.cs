using System.Collections;
using System.Collections.Generic;
using System.Text;

[System.Serializable]
public class Culture {

	//Stats
	public int agressiveness = 0;
	public int technology = 0;
	public int ideology;

	public string description = null;

	// Use this for initialization
	public Culture () {
		
	}

	public Culture (int cmax = 10) {
		var rnd = new System.Random ();
		rollCulture ( rnd.Next (0, cmax) );
	}
	private void rollCulture(int points)
	{
		var rnd = new System.Random ();
		agressiveness = rnd.Next (0, points);
		technology = rnd.Next (0, points);
		ideology = rnd.Next (0, ideologyDes.Length);
	}

	public override string ToString ()
	{
		StringBuilder res = new StringBuilder ();

		res.Append (ideology);
		res.Append (" Ideology: ");
		res.AppendLine( getIdeologyDescription () );

		res.Append (agressiveness);
		res.Append (" Agressiveness: ");
		res.AppendLine( getAggroDescription () + " race");

		res.Append (technology);
		res.Append (" Technology: ");
		res.AppendLine( getTechDescription () );

		return res.ToString ();
	}

	public static implicit operator string(Culture culture){

		return culture.description;
	}


	// TODO: WORD FORMULA
	// One noun (work, mind, commune) one qualifier -ism (supremacist, purism, isolationism)

	//TODO: 
	//* 1 separate this from culture class
	//* 2 create template file
	//* 3 pick up nouns, qualifiers etc from file and combine them on start



	static string[] ideologyDes = {
		"Individualist supremacy",
		"Theocracy",
		"Agrarian purists",
		"Technocracy",
		"Genetic Supremacist",
		"Intelectual meritocracy",
		"Absolute consumerism ",
		"Mental isolationism",
		"unknown",
		"Communnal"
	};
	static string[] aggDes = {
		"weak", 	//0
		"impotent", //1
		"languid", 	//2
		"tame",		//3
		"feeble",	//4
		"barbaric",	//5
		"belicose",	//6
		"dangerous",//7
		"very hostile",//8 
		"destructive"};//9

	static string[] techDes = {
		"cave dwellers",
		"primitive nomads",
		"minimal understanding of physics",
		"primary grasp of chemistry",
		"underdeveloped explotation of its resources",
		"industrial surge",
		"rudimentary electronic",
		"basic information manipulation",
		"elementary understanding of atomic energy",
		"Capable of exoplanetary flight"
	};


	public string getIdeologyDescription(){
		if ( ideology > 0 && ideology < ideologyDes.Length  )
			return ideologyDes [this.ideology];
		else
			return string.Empty;
	}

	public string getTechDescription(){
		if ( technology > 0 && technology < techDes.Length  )
			return techDes [this.technology];
		else
			return string.Empty;
	}

	public string getAggroDescription(){
		if ( agressiveness > 0 && agressiveness < aggDes.Length )
			return aggDes [this.agressiveness];
		else
			return string.Empty;
	}

	private void genDescription(){
		if (description == null) return;

		StringBuilder res = new StringBuilder ();

		res.Append (ideology);
		res.Append (" Ideology: ");
		res.AppendLine ( getIdeologyDescription () );

		res.Append (agressiveness);
		res.Append (" Agressiveness: ");
		res.AppendLine ( aggDes [this.agressiveness] + " race");

		res.Append (technology);
		res.Append (" Technology: ");
		res.AppendLine ( techDes [this.technology] );

		description = res.ToString ();
	}
}
