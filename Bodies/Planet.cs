
using System.Collections.Generic;
using UnityEngine;

public class Planet : CelestialBody
{

	[HideInInspector]
	public bool hasCivilization = false;
	public Species dominantSpecies ;
	public ResourceInventory resources = new ResourceInventory ();
	public float xAmplitude = 100, yAmplitude = 100;
	public float orbitSpeed;
	[HideInInspector]
	public float baseAngle = 180;
	private Transform sun;
	private int resourcePoints = -1;

	#if UNITY_EDITOR
	void Awake()
	{
		//initCiv();
		hasCivilization = true;
	}
	#endif

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
		//explotation.mineEfficiency = (BaseVals.maxResource * 10)/rmax;//somewhat related with resource availability
		//explotation.probeDifficulty = rmax / 2;//atmosphere elements, yada yada
	}

	public int getResourcePoints ()
	{
		return resourcePoints / BaseVals.maxResource;
	}

	public string getResourcesList ()
	{
		return resources.ToString ();
	}

	public ExplotationState getExplotationState()
	{
		return explotation.state;
	}


	public void probe(int probeQuantity)
	{
		explotation.probe (probeQuantity);

		if ( hasCivilization && explotation.civKnowledge == Exploitation.civDiscovered) {
			EvHandler.ExecuteEv (UIEvent.SHOW_AUTOFADE_TOOLTIP, string.Format("Civilization '{0}' discovered!", dominantSpecies.name));
		}

		//mining
		int resourceQ = resources.getResourcesQuantity;
		if (resourceQ == 0) {
			print("No resources in this planet...");
			return;
		}
		print(string.Format("{0}", resourceQ));
		int intExpState = System.Convert.ToInt32(explotation.state);
		int mineChance = probeQuantity + intExpState;

		print(string.Format("mining roll chance: {0} vs plt chance: {1}", mineChance, explotation.mineChance));
		int mine = Random.Range(0, mineChance);

		print ("die roll of "+mine);
		if ( mine > explotation.mineChance ) {
			int minResourceGain = probeQuantity / intExpState;

			print(string.Format("total plt resource q {0}, min res gain {1}", resourceQ, minResourceGain));
			int mineVarietyRess = (int) Random.Range(minResourceGain, explotation.mineEfficiency);
			int mineAmountRess = (int) (probeQuantity * explotation.mineEfficiency);

			print(string.Format("will gain {0} different resources, for a max of q:{1}", mineVarietyRess, mineAmountRess));
			//genate n amount of indexes
			int[] idxs = RandomExt.rndNonRepeatingIndexes(mineVarietyRess, resourceQ);
			ResourceBase[] mined = new ResourceBase[mineVarietyRess];
			ResourceBase[] res = resources.getArray();

			print(string.Format("actual len resources {0} index q {1}", res.Length, idxs.Length));
			for (int i = 0; i < idxs.Length; i++) {
				string nam = res[ idxs[i] ].name;
				int q = Random.Range(1, mineAmountRess);
				mined[i] = new ResourceBase(nam, q); //add
				resources.modify(nam, -q); //substract from

				print(mined[i]);
				EvHandler.ExecuteEv(GameEvent.MOD_RES, mined[i]);
			}
		}
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
