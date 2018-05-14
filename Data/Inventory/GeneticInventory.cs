using System.Collections.Generic;
using System.Text;


[System.Serializable]
public class GeneticInventory {
	
	public Dictionary<string, GeneticTrait> traits ;

	public GeneticInventory ()
	{
		traits = new Dictionary<string, GeneticTrait>();
	}

	public void apply(Species sp){
		foreach (GeneticTrait t in traits.Values) {
			t.apply (sp);
		}
	}
	
	public void add(string traitName){
		if (!traits.ContainsKey (traitName)) {
			traits.Add (traitName, new GeneticTrait (traitName));
		}
	}

	public void add(GeneticTrait t){
		if (!traits.ContainsKey (t.name)) {
			traits.Add (t.name, t);
		}
	}

	public void addRange(GeneticTrait[] tArray){
		
		for (int i = 0; i < tArray.Length; i++) {
			if (!traits.ContainsKey (tArray [i].name)) {
				traits.Add ( tArray [i].name, tArray [i]);
			}
		}
	}

	public void addRange(List<GeneticTrait> tList){
		for (int i = 0; i < tList.Count; i++) {
			if (!traits.ContainsKey ( tList [i].name )) {
				traits.Add (tList [i].name, tList [i]);
			}
		}
	}

	public GeneticTrait get(string traitName){
		if (traits.ContainsKey (traitName)) {
			return traits [traitName];
		}
		return null;
	}

	public void remove(string traitName){
		if (traits.ContainsKey (traitName)) {
			traits.Remove (traitName);
		}
	}

	public void remove(GeneticTrait trait){
		if (traits.ContainsValue (trait)) {
			traits[trait.name] = null;
			traits.Remove (trait.name);
		}
	}

	public GeneticTrait[] getArray(){
		return new List<GeneticTrait> (traits.Values).ToArray ();
	}

	public override string ToString ()
	{
		StringBuilder sb = new StringBuilder ();
		foreach (GeneticTrait g in traits.Values) {
			sb.AppendLine (g.name);
		}
		return sb.ToString ();
	}


}
