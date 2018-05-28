using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(PlanetEditor), true)]
public class PlanetEditor : Editor
{
	GUIStyle style = new GUIStyle();

	void OnEnable()
	{
		style.normal.textColor = Color.white;
		style.fontSize = 12;
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
}
