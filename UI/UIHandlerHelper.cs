using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandlerHelper {


	private static Dictionary<int, Transform> history = new Dictionary<int, Transform>();
	
	public static void Reparent(GameObject go, GameObject newParent) {
		Reparent (go.transform, newParent.transform);
	}

	public static void Reparent(Transform tr, Transform newParent) {
		if (history.ContainsKey (tr.GetInstanceID ())) throw new System.Exception ();
		history.Add (tr.GetInstanceID (), tr.parent);
		tr.SetParent (newParent.transform);
		tr.gameObject.SetActive (false);
		tr.gameObject.SetActive (true);
	}

	public static void ParentRestore(GameObject go) {
		ParentRestore (go.transform);
	}

	public static void ParentRestore(Transform tr) {
		int key = tr.GetInstanceID ();
		Transform oldParent = history [ key ];
		history.Remove (key);
		tr.SetParent (oldParent);
		tr.gameObject.SetActive (false);
		tr.gameObject.SetActive (true);
	}
}
