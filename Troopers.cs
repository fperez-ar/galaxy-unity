using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troopers : ResourceBase {

	public int manpower {
		get { return quantity;}
		set { quantity = value;}
	}
	public float morale = 100, adaptability = 1;
	public float offensiveCap = 1, defensiveCap = 1;

	public Troopers ()
	{
		
	}

	public Troopers (int _quantity) : base (_quantity)
	{

	}

	public Troopers (string _name, int _quantity) : base (_name, _quantity)
	{

	}

	public override string ToString ()
	{
		return string.Format ("{0}: \nmanpower {1} \nattacking {2} \ndefending {3}", name, manpower, offensiveCap, defensiveCap);

	}

	public void applyDamage(float dmg) {
		applyDamage ((int)dmg);
	}
	public void applyDamage(int dmg) {
		manpower -= dmg;
		if (manpower <= 0) {
			manpower = 0;
		}

		EvHandler.ExecuteEv ("TroopKilled", this);
	}

	public static Troopers operator +(Troopers t1, Troopers t2){
		Troopers t = new Troopers ();
		t.offensiveCap 	= (t1.offensiveCap + t2.offensiveCap) / 2;
		t.defensiveCap 	= (t1.defensiveCap + t2.defensiveCap) / 2;
		t.morale 		= (t1.morale + t2.morale) 			  / 2;
		t.adaptability 	= (t1.adaptability + t2.adaptability) / 2f;
		t.manpower		= t1.manpower + t2.manpower;

		return t;
	}

	public static Troopers sum(params Troopers[] troops){
		float off = 0, def = 0, moral = 0;
		int man = 0;
		float adapt = 0f;
		int len = troops.Length;
		if (len == 0) return null;

		for (int i = 0; i < len; i++) {
			off += troops [i].offensiveCap;
			def += troops [i].defensiveCap;
			adapt += troops [i].adaptability;
			moral += troops [i].morale;
			man += troops [i].manpower;
		}

		off /= len;
		def /= len;
		adapt /= len;
		moral /= len;

		return new Troopers () { 
			offensiveCap = off, 
			defensiveCap = def, 
			adaptability = adapt,
			manpower = man,
			morale = moral
		};
	}


}
