using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIListElement : MonoBehaviour {

	public virtual void set(Troopers t) {}
	public virtual void set(ResourceBase r) {}
	public virtual void set(GeneticTrait g) {}

}
