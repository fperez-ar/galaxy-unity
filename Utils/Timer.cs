using UnityEngine;
using System.Collections;

[System.Serializable]
public class Timer
{
	public float timeAccumulator { get { return t; }}
	public float progress { get { return t/intervalo; }}

	public float intervalo;
	private float t;
	private bool resetOntime;
	public SimpleDelegate onTimePassed;

	public Timer (float interval = 1, bool resetOnTimePassed = true)
	{
		t = 0;
		resetOntime = resetOnTimePassed; 
		intervalo = interval;
	}

	public bool check () 
	{
		t += Time.deltaTime;

		if(t >= intervalo)	
		{
			t = 0;
			if (onTimePassed != null) {
				onTimePassed ();
			}
			if ( resetOntime ) reset ();
			return true;
		}

		return false;
	}

	public void reset()
	{
		t = 0;
	}

}


