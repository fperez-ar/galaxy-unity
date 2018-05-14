using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorSnippets : Editor{}

[CustomEditor(typeof(EvDispatcher))]
public class EvDispatcherInspector : Editor {

	private static string evStr = "";
	private static bool fold = true;
	private EvDispatcher evDisObj;

	public override void OnInspectorGUI ()
	{
		//base.OnInspectorGUI ();

		EditorGUILayout.Separator ();
		evDisObj = (EvDispatcher)target;

		evDisObj.dispatchEventType = (EventTypes) EditorGUILayout.EnumPopup ("Event Type", evDisObj.dispatchEventType);

		if (evDisObj.dispatchEventType.Equals (EventTypes.GameEvent)) {
			evDisObj.gameEvent = (GameEvent) EditorGUILayout.EnumPopup ("Game Event", evDisObj.gameEvent);
		} else {
			evDisObj.uiEvent = (UIEvent) EditorGUILayout.EnumPopup ("ui Event", evDisObj.uiEvent);
		}

		fold = EditorGUILayout.Foldout (fold, "Show debug", true);
		if ( fold ) {
			if ( GUILayout.Button ("Dispatch ENUM event ") ){
				((EvDispatcher)target).DispatchEvent ();
			}

			GUILayout.BeginHorizontal ();
			evStr = GUILayout.TextField (evStr);

			if ( GUILayout.Button ("Dispatch STR event", GUILayout.MaxWidth (250)) ){
				EvDispatcher o = (EvDispatcher)target;
				Debug.Log ("dispatching custom ev: "+ evStr);
				EvHandler.ExecuteEv (evStr);
			}

			GUILayout.EndHorizontal ();
		}
		serializedObject.ApplyModifiedProperties ();
	}

}

/*[CanEditMultipleObjects, CustomEditor(typeof(Transform))]
public class TrInspector: Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		if (GUILayout.Button ("Round position")) {
			Vector3 pos = ((Transform)target).position;
			pos.x = Mathf.Round (pos.x);
			pos.y = Mathf.Round (pos.y);
			pos.z = Mathf.Round (pos.z);
			((Transform)target).position = pos;
		}


	}
}
*/