using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomExt {

	//function for  spiral out in x axis f(t) = ( t, t*cos(t), t*sin(t))


	public static Vector3 rndPositionInBounds(Bounds b){
		float x = Random.Range (-b.extents.x, b.extents.x);
		float y = Random.Range (-b.extents.y, b.extents.y);
		float z = Random.Range (-b.extents.z, b.extents.z);
		return new Vector3 (x, y, z);
	}

		
	public static Vector3 rndPositionInOrbit(Transform orbitCenter, float orbitDistance){
		return rndPositionInOrbit (orbitCenter.position, orbitDistance);
	}

	public static Vector3 rndPositionInOrbit(Vector3 orbitCenter, float orbitDistance){
		return (Vector3) Random.insideUnitSphere * orbitDistance;
	}

	public static Vector3 uniformVector3(float max){
		return Vector3.one * Random.Range (1, max);
	}

	public static Vector3 rndVector3(Vector3 min, Vector3 max){
		float x = Random.Range (min.x, max.x);
		float y = Random.Range (min.y, max.y);
		float z = Random.Range (min.z, max.z);

		return new Vector3 (x, y, z);
	}

	public static List<ResourceBase> rndResources(int resourcePoints = 10){
		//TODO: make a way to make automatic resources regardless of quantity
		var rnd = new System.Random ();
		int total = resourcePoints;
		List<ResourceBase> rs = new List<ResourceBase> (total);//= new Resource[ rnd.Next (1, 4) ];

		while (total > 0) {
			//TODO>
			//choose resources from list/file of randomly generated resource names
			string nam = RandomText.getBisilableText (3, 5);

			int q = rnd.Next (0, resourcePoints);
			var r = new ResourceBase(nam, q);
			rs.Add (r);
			total -= r.quantity;
		}

		return rs;
	}

	//select quantity of numbers up to max
	public static int[] rndNonRepeatingIndexes(int quantity, int maxValue){
		//generate random indexes
		var r = new System.Random ();
		List<int> all = Enumerable.Range (0, maxValue).ToList ();
		int[] indxs = new int[quantity];
		int randomIndex = -1;

		for (int i = 0; i < quantity; i++) {
			randomIndex = r.Next (0, all.Count);
			indxs [i] = all [ randomIndex  ];
			all.Remove ( randomIndex );
		}

		return indxs;
	}

	public static ResourceBase[] rndResourcesFromFile(int resourcePoints = 10){
		var rnd = new System.Random ();
		int total = resourcePoints;

		string path = Application.dataPath + "/Resources/resources";
		string[] resources = System.IO.File.ReadAllLines (path, Encoding.UTF8);

		string name = "";
		int index = 0, qua = 0; 
		int len = rnd.Next (BaseVals.minResPerPlanet, BaseVals.maxResPerPlanet);
		List<ResourceBase> ls = new List<ResourceBase> (len);
		//ResourceBase[] rs = new ResourceBase[len];


		for (int i = 0; i < len; i++) {
			if (total <= 0) break;

			index = Random.Range (0, resources.Length);
			name = resources [index];
			qua = rnd.Next (resourcePoints);
			ls.Add (new ResourceBase(name, qua));
			total -= qua;
		}

		return ls.ToArray ();
	}

	public static int rndTroopValues(Species s){
		int a = s.culture.agressiveness;
		int t = s.culture.technology;
		//TODO: make a formular based on parameters

		return Random.Range (a+t, BaseVals.Manpower);
	}

	public static GeneticTrait[] rndTraits(){
		var rnd = new System.Random ();

		int quantityTraits = rnd.Next(BaseVals.MaxTraits);
		var alltraits = Resources.LoadAll <GeneticTrait> ("Traits");

		int[] indxs = rndNonRepeatingIndexes (quantityTraits, alltraits.Length);
		GeneticTrait[] gs = new GeneticTrait[quantityTraits];


		int index;
		for (int i = 0; i < quantityTraits; i++) {
			index = indxs [i];
			gs [i] = alltraits [ index ];
		}

		return gs;
	}

}
