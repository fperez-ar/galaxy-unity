using System;
using System.Text;
using UnityEngine;

[System.Serializable]
public class GeneticTrait : UnityEngine.ScriptableObject {

	//public new string name;
	public string description = "";
	[Range(-1,1)]
	public float aggresivenessModificator = 0;
	[Range(-1,1)]
	public float technologyModificator = 0;
	[Range(-1,1)]
	public float offensiveCapModificator = 0;
	[Range(-1,1)]
	public float defensiveCapModificator = 0;
	[Range(-1,1)]
	public float manpowerModificator = 0;
	[Range(-1,1)]
	public float moraleModificator = 0;
	public float adaptability = 0;


	public GeneticTrait () {}

	public GeneticTrait (string _name) 
	{
		name = _name;
	}

	public virtual void apply(Species sp){
		apply (sp.culture);
	}

	public virtual void apply(Culture c){
		c.agressiveness += (int) (BaseVals.Agressiveness * aggresivenessModificator);
		c.technology += (int) (BaseVals.Technology * technologyModificator );
	}

	public virtual void apply(ManpowerInventory m){
		Troopers[] troops = m.getArray ();
		for (int i = 0; i < troops.Length; i++) {
			apply ( troops [i] );
		}
	}


	public virtual void apply(Troopers t){
		t.offensiveCap = (int) (BaseVals.OffensiveCap * offensiveCapModificator);
		t.defensiveCap = (int) (BaseVals.DefensiveCap * defensiveCapModificator);
		t.morale 	  += (int) (BaseVals.Morale 		* moraleModificator);
	}


	public override string ToString ()
	{
		StringBuilder sb = new StringBuilder ();

		if ( name != string.Empty ) sb.AppendLine(name);
		if ( description != string.Empty ) sb.AppendLine(description);

		if (aggresivenessModificator != 0){
			sb.AppendLine("Aggresiveness "+aggresivenessModificator); 
		}
		if (technologyModificator != 0){
			sb.AppendLine("Technology "+technologyModificator); 
		}
		if (offensiveCapModificator != 0){
			sb.AppendLine("OffensiveCap "+offensiveCapModificator); 
		}
		if (defensiveCapModificator != 0){
			sb.AppendLine("DefensiveCapabilities "+defensiveCapModificator); 
		}
		if (manpowerModificator != 0){
			sb.AppendLine("Troops "+manpowerModificator); 
		}
		if (moraleModificator != 0){	
			sb.AppendLine("Morale "+moraleModificator); 
		}
		return sb.ToString ();
	}

	private float sum(){
		return aggresivenessModificator + offensiveCapModificator + defensiveCapModificator + technologyModificator + manpowerModificator + moraleModificator;
	}

	public string getStringQuantity(char separator = ':'){
		return name + separator +"\n"+ description;
	}

	public string getStringQuantity(string separator){
		return name + separator +"\n"+ description;
	}



	public string getName (){ return name; }

	public float normalizedQuantity {	get { return (sum() / 6f); } }
}
