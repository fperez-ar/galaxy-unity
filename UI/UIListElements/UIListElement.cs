using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIListElement<T> : MonoBehaviour where T : class
{

	protected T refObj;

	public virtual void set (T t)
	{
	}

	public virtual void unset ()
	{
		this.gameObject.SetActive (false);
		refObj = null;//default(T);
	}
}
