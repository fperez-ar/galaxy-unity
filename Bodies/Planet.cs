using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : CelestialBody
{


	[HideInInspector]
	public bool hasCivilization = false;
	public Species dominantSpecies = null;

	public ResourceInventory resources = new ResourceInventory ();
	private int resourcePoints = -1, probeDifficulty = 1, mineEfficiency = 1;

	private Transform sun;
	public float xAmplitude = 100, yAmplitude = 100;
	public float orbitSpeed;
	[HideInInspector]
	public float baseAngle = 180;

	public void initCiv ()
	{
		hasCivilization = true;
		dominantSpecies = new Species ();
		dominantSpecies.Init ();
	}

	public void initCiv (int cmax)
	{
		hasCivilization = true;
		Culture cult = new Culture (cmax);
		dominantSpecies = new Species (cult);
		dominantSpecies.Init ();
	}

	public void setSun (Sun _sun)
	{
		sun = _sun.transform;
	}

	public void setSun (Transform sunTransform)
	{
		sun = sunTransform;
	}

	public void initResources (int rmax = 10)
	{
		resources.addRange (RandomExt.rndResourcesFromFile (rmax));
		resourcePoints = rmax;
		mineEfficiency = (BaseVals.maxResource * 10)/rmax;//somewhat related with resource availability
		probeDifficulty = rmax / 2;//atmosphere elements, yada yada
	}

	public int getResourcePoints ()
	{
		return resourcePoints / BaseVals.maxResource;
	}

	public int getProbeDifficulty()
	{
		return probeDifficulty;
	}

	public string getResourcesList ()
	{
		return resources.ToString ();
	}

	void FixedUpdate ()
	{
		if (sun)
			orbit ();
	}

	void orbit ()
	{

		float angle = Mathf.Deg2Rad * baseAngle * Time.time;
		float x = Mathf.Cos (angle * orbitSpeed) * xAmplitude;
		float z = Mathf.Sin (angle * orbitSpeed) * yAmplitude;

		transform.position = sun.position + new Vector3 (x, 0, z) * Time.deltaTime;
	}

	public override string ToString ()
	{
		return this.name;
	}

}
