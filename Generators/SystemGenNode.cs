using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SystemGenNode : MonoBehaviour {

	[Range(0,1)]
	public float lifePosibility = 0.2f;
	public string systemName = "";
	public float systemRange = 1;

	public int qPlanets = 2;
	public float planetSizeMod = 0.2f, orbitalSpeedMod = 0.5f;
	public Planet[] planetPrefabs;

	public float sunSizeMax = 6;
	public Sun[] sunPrefabs;

	//Name generation
	public int nameLenMin = 6, nameLenMax = 12;
	public int vowelIntervalMin = 2, vowelIntervalMax = 4;

	void Start(){
		StartCoroutine ( coSystemGeneration () );
	}

	IEnumerator coSystemGeneration(){

		int l = Random.Range (0, sunPrefabs.Length);
		Sun sun = Instantiate<Sun> (sunPrefabs[l], transform.position, Quaternion.identity, transform);
		sun.name = systemName+"'s sun";
		sun.transform.localScale = RandomExt.uniformVector3 (sunSizeMax) + transform.localScale;

		for (int i = 0; i < qPlanets; i++) {

			l = Random.Range (0, planetPrefabs.Length);

			float sunOffset = sun.transform.lossyScale.magnitude;
			float orbitalPos =  sunOffset + i * (qPlanets / systemRange);

			Vector3 pos = transform.position + RandomExt.rndPositionInOrbit (transform.position, orbitalPos); //+ Random.insideUnitSphere * systemRange;
			Quaternion rot = Random.rotation;
			Planet pln = Instantiate<Planet> (planetPrefabs [l], pos, rot, transform);
			pln.enabled = false;

			//name
			pln.name = RandomText.reallyRandomText(nameLenMin, nameLenMax);
			 //print (pln.name);
			 //RandomExt.getBisilableText (nameLenMin, nameLenMax);
			 //RandomExt.getNiceText ( Random.Range (nameLenMin, nameLenMax), Random.Range (vowelIntervalMin, vowelIntervalMax) ); 
			 //RandomExt.getEsperanto  (nameLenMin, nameLenMax); 

			//planet resources
			pln.initResources ();
			float lifeChance = pln.getResourcePoints () + lifePosibility;
			if (Random.value < lifeChance ) {
				pln.initCiv ();
			}

			//planet orbit
			pln.setSun(sun.transform);

			pln.transform.localScale *= planetSizeMod * transform.lossyScale.magnitude;
			pln.orbitSpeed *= Random.Range (-1f, 1f) * orbitalSpeedMod;
			pln.xAmplitude *= Random.Range (sunOffset, systemRange);
			pln.yAmplitude *= Random.Range (sunOffset, systemRange);
			pln.enabled = true;
			//pln.baseAngle = Random.value < 0.5f ? 360f : 180f;
			yield return null;

		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		//Gizmos.DrawWireSphere(transform.position, systemRange);
	}
}
