using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyGen : MonoBehaviour {

	//public Bounds galaxyBounds;
	public float galaxyRange = 5;
	public SystemGenNode[] systemPrefabs;

	public int qSystems = 2;

	// Use this for initialization
	void Start () {
		StartCoroutine ( coGalaxyGeneration () );
	}


	IEnumerator coGalaxyGeneration(){
		for (int i = 0; i < qSystems; i++) {
			
			int l = Random.Range (0, systemPrefabs.Length);
			Vector3 pos = transform.position + Random.insideUnitSphere * galaxyRange;
			Quaternion rot = Random.rotation;
			SystemGenNode sys = Instantiate<SystemGenNode> (systemPrefabs [l], pos, rot, transform);
			sys.systemName = RandomText.getNiceText (Random.Range (4, 10));
			sys.name = "System " + sys.systemName;//+ i;

			yield return new WaitForSeconds(0.1f);
		}
	}


	void OnDrawGizmos()
	{
		//Gizmos.DrawWireCube (transform.position+galaxyBounds.center, galaxyBounds.extents);
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, galaxyRange);
	}
}
