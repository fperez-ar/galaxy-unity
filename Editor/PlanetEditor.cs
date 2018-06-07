using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(CelestialBody), true)]
public class PlanetEditor : Editor
{
	GUIStyle style = new GUIStyle();

	ResourceHelper r;
	private ResourceInventory resourcesInv;
	private static bool[] rfold = new bool[2]{true, false};

	//private GUILayoutOption mCharWidth = GUILayout.MaxWidth (20);
	//private GUILayoutOption mMinWidth = GUILayout.MinWidth (50);
	private GUILayoutOption m250Width = GUILayout.MaxWidth (250);
	private GUILayoutOption mStrMaxWidth = GUILayout.MaxWidth (500);

	void OnEnable()
	{
		style.normal.textColor = Color.white;
		style.fontSize = 12;

		if ( target is Planet )
		{
			Planet p = (Planet) target;
			resourcesInv = p.resources;
		}
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		ResourceSerialize();
		if ( GUILayout.Button("init civilization")) {
			((Planet)target).initCiv();
		}

	}

	void OnSceneGUI ()
	{
		CelestialBody c = target as CelestialBody;
		Transform tr = c.transform;
		Vector3 v = c.GetComponent <Renderer> ().bounds.extents;

		Handles.DrawLine (tr.position, tr.position + new Vector3 (v.x, 0, 0) );
		Handles.DrawLine (tr.position, tr.position + new Vector3 (0, v.y, 0) );
		Handles.DrawLine (tr.position, tr.position + new Vector3 (0, 0, v.z) );

		Handles.DrawWireCube (tr.position, v);

		Handles.Label (tr.position + new Vector3 (v.x, 0, 0) , "x" + v.x, style);
		Handles.Label (tr.position + new Vector3 (0, v.y, 0) , "y" + v.y, style);
		Handles.Label (tr.position + new Vector3 (0, 0, v.z) , "z" + v.z, style);
	}

	void ResourceSerialize()
	{
		if (GUILayout.Button ("Resource Inventory")) { rfold [0] = !rfold [0]; }

		if ( rfold[0] && resourcesInv != null){
			EditorGUILayout.LabelField ("Resources");

			string slatedForRemoval = null;
			ResourceBase[] rs = resourcesInv.getArray ();
			for (int i = 0; i < rs.Length; i++) {

				GUILayout.BeginHorizontal ();
				if ( GUILayout.Button("x", mStrMaxWidth)){ slatedForRemoval = rs [i].name; }
				rs [i].name = EditorGUILayout.TextField ("Name", rs [i].name);
				rs [i].quantity = EditorGUILayout.IntField ("Quantity", rs [i].quantity);
				GUILayout.EndHorizontal ();
				GUILayout.Space (5);
			}
			if (slatedForRemoval != null){ resourcesInv.remove (slatedForRemoval); }

			rfold [1] = EditorGUILayout.Foldout(rfold [1], "Add Resource", true);
			if (rfold [1]) {

				GUILayout.BeginHorizontal ();
				r.name = EditorGUILayout.TextField ("Name", r.name);
				r.quantity = EditorGUILayout.IntField ("Quantity", r.quantity);
				GUILayout.EndHorizontal ();

				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Add", m250Width) ){
					resourcesInv.add ( r.getResource ());
					EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
				}

				/*if ( GUILayout.Button ("Remove", defStyle ()) ){
					resourcesInv.remove (r.name);
				}*/

				GUILayout.EndHorizontal ();
			}
		}
	}
}
