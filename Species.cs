using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Species {

	[SerializeField]
	public string name = "";
	public int population {
		get { return m_population; }
		set { m_population = Mathf.Clamp (value, 0, BaseVals.maxPopulation); }
	}
	private int m_population = 10000;
	public Culture culture;
	public GeneticInventory gen  = new GeneticInventory ();
	public ManpowerInventory man = new ManpowerInventory ();

	public Species ()
	{
		culture = new Culture();
	}

	public Species (Culture newCulture)
	{
		culture = newCulture;
	}

	public Species(string _name, Culture _culture){
		name = _name;
		culture = _culture;
		gen = new GeneticInventory ();
		man = new ManpowerInventory ();
	}

	public void Init(){
		name = RandomText.reallyRandomText (4, 10);//.getBisilableText (2, 10);
		gen.addRange ( RandomExt.rndTraits () );
		gen.apply (this);
		int q = RandomExt.rndTroopValues (this);
		man.add ( new Troopers (BaseVals.BaseTroops, q) );
	}

	public void addTroops(Troopers t){
		man.add (t);
	}

	public void addTroops(string name, int quantity){
		man.add (name, quantity);
	}

	public Troopers getTroops(){
		return man.get (BaseVals.BaseTroops);
	}

	public Troopers getTroops(string troopsName){
		return man.get (troopsName);
	}


	public void applyDamage(float dmg){
		applyDamage( (int) dmg);
	}

	public void applyDamage(int dmg){
		applyDamage (dmg, BaseVals.BaseTroops);
	}

	public void applyDamage(int dmg, string troopsName){
		Troopers t = man.get (troopsName);
		t.applyDamage (dmg);
		if (t.manpower == 0) {
			//Debug.Log (troopsName);
			//man.remove (troopsName);
		}
	}

	public override string ToString ()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder ();
		sb.Append ("Race: ");
		sb.AppendLine (this.name);
		sb.AppendLine ("Genetic traits: ");
		sb.Append ("\t"); sb.Append (gen.ToString ());
		sb.AppendLine (culture.ToString ());
		sb.AppendLine ("Armed force: ");
		sb.Append ("\t"); sb.AppendLine (man.ToString ());

		return sb.ToString ();
	}

	public static implicit operator bool(Species sp){
		return (sp != null);
	}


}
