using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//https://docs.unity3d.com/Manual/script-Serialization-Custom.html

[CustomEditor(typeof(PlayerShip))]
public class SerializePlayerShip : Editor {

	Species sp;
	Culture mCulture;
	GeneticInventory geneticInv;
	ResourceInventory resourcesInv;
	ManpowerInventory manInv;

	GeneticTrait g;
	TrooperHelper t;
	ResourceHelper r;
	string removeName;

	private static bool[] gfold = new bool[2]{true, false};
	private static bool[] rfold = new bool[2]{true, false};
	private static bool[] tfold = new bool[2]{true, false};

	private GUILayoutOption mCharWidth = GUILayout.MaxWidth (20);
	private GUILayoutOption mMinWidth = GUILayout.MinWidth (50);
	private GUILayoutOption m250Width = GUILayout.MaxWidth (250);
	private GUILayoutOption mStrMaxWidth = GUILayout.MaxWidth (500);


	void OnEnable(){
		PlayerShip p = ((PlayerShip)target);

		sp = p.dominantSpecies;
		mCulture = sp.culture;
		manInv = p.dominantSpecies.man;
		resourcesInv = p.resources;
		geneticInv = p.dominantSpecies.gen;
	}

	public override void OnInspectorGUI ()
	{

		serializedObject.Update ();

		//base.OnInspectorGUI ();

		EditorGUILayout.LabelField ("Species: ", sp.name, EditorStyles.boldLabel);
		sp.name = EditorGUILayout.TextField (sp.name);


		CultureSerialize ();

		EditorGUILayout.Separator ();

		ResourceSerialize ();
		EditorGUILayout.Separator ();

		GeneSerialize ();
		EditorGUILayout.Separator ();

		TroopSerialize ();
		EditorGUILayout.Separator ();

		serializedObject.ApplyModifiedProperties ();
		//EditorUtility.SetDirty (target);

	}

	void CultureSerialize (){
		EditorGUILayout.LabelField ("Culture", EditorStyles.helpBox);
		mCulture.ideology = EditorGUILayout.IntField ("Ideology", mCulture.ideology);
		EditorGUILayout.LabelField ("\t",mCulture.getIdeologyDescription(), EditorStyles.boldLabel);

		mCulture.agressiveness = EditorGUILayout.IntField ("Aggresiveness", mCulture.agressiveness);
		EditorGUILayout.LabelField ("\t", mCulture.getAggroDescription(), EditorStyles.boldLabel);

		mCulture.technology = EditorGUILayout.IntField ("Technology", mCulture.technology);
		EditorGUILayout.LabelField ("\t", mCulture.getTechDescription(), EditorStyles.boldLabel);
	}

	void ResourceSerialize(){
		
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

	void GeneSerialize(){
		if (GUILayout.Button ("Genetic Inventory")) { gfold [0] = !gfold [0]; }

		if ( gfold[0] && geneticInv != null){

			EditorGUILayout.LabelField ("Key","Values");
			string slatedForRemoval = null;
			foreach (var itemVal in geneticInv.traits) {
				
				GUILayout.BeginHorizontal ();
				 EditorGUILayout.LabelField (itemVal.Key, mStrMaxWidth);
				 if ( GUILayout.Button("x", mCharWidth)){ slatedForRemoval = itemVal.Key; }
				GUILayout.EndHorizontal ();

				GUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField ("Description", GUILayout.MaxWidth (75));//, EditorStyles.textArea
				EditorGUILayout.HelpBox (itemVal.Value.description, MessageType.None);
				GUILayout.EndHorizontal ();
				GUILayout.Space (5);

			}
			if (slatedForRemoval != null){ geneticInv.remove (slatedForRemoval); }

			gfold [1] = EditorGUILayout.Foldout (gfold [1], "Add Genetic trait", true);
			if (gfold [1]) {
				
				GUILayout.BeginHorizontal ();
				g = (GeneticTrait) EditorGUILayout.ObjectField (g, typeof(ScriptableObject), false, m250Width);

				if (GUILayout.Button ("Add", m250Width) ){					
					geneticInv.add (g);
					EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
				}

				/*if ( GUILayout.Button ("Remove", defStyle ()) ){
					geneticInv.remove (g);
				}*/
				GUILayout.EndHorizontal ();
			}
		}
	}

	void TroopSerialize(){
		if (GUILayout.Button ("Troops")) { tfold [0] = !tfold [0]; }

		if (tfold [0] && manInv != null) {
			//EditorGUILayout.LabelField ("Troop types", EditorStyles.helpBox);
			GUILayout.Space (8);

			string slatedForRemoval = null;
			Troopers[] ts = manInv.getArray ();
			for (int t = 0; t < ts.Length; t++) {

				GUILayout.BeginHorizontal ();
				ts [t].name = EditorGUILayout.TextField("Name", ts [t].name);
				if (GUILayout.Button ("x", mCharWidth)) { slatedForRemoval = ts [t].name; }
				GUILayout.EndHorizontal ();

				GUILayout.BeginHorizontal ();
				ts [t].offensiveCap = EditorGUILayout.FloatField("Offensive Capabilities", ts [t].offensiveCap, mMinWidth);
				ts [t].defensiveCap = EditorGUILayout.FloatField("Defensive Capabilities", ts [t].defensiveCap, mMinWidth);
				GUILayout.EndHorizontal ();

				GUILayout.BeginHorizontal ();
				ts [t].quantity = EditorGUILayout.IntField ("Manpower", ts [t].quantity, mMinWidth, mMinWidth);
				ts [t].morale = EditorGUILayout.FloatField("Morale", ts [t].morale, mMinWidth, mMinWidth);
				GUILayout.EndHorizontal ();

				ts [t].adaptability = EditorGUILayout.FloatField("Adaptability", ts [t].adaptability, mStrMaxWidth);
				GUILayout.Label("____________________________", EditorStyles.centeredGreyMiniLabel);
				//EditorGUILayout.Space ();
			}
			if (slatedForRemoval != null) manInv.remove (slatedForRemoval);

			tfold [1] = EditorGUILayout.Foldout (tfold [1], "Add Troops", true);
			if (tfold [1]) {

				t.name = EditorGUILayout.TextField ("Name", t.name, mStrMaxWidth);

				GUILayout.BeginHorizontal ();
				 t.offense = EditorGUILayout.IntField ("Offensive Capabilities", t.offense);
				 t.defense = EditorGUILayout.IntField ("Defensive Capabilities", t.defense);
				GUILayout.EndHorizontal ();

				GUILayout.BeginHorizontal ();
				 t.quantity = EditorGUILayout.IntField ("Manpower", t.quantity);
				 t.morale = EditorGUILayout.IntField ("Morale", t.morale);
				GUILayout.EndHorizontal();

				t.adaptability = EditorGUILayout.Slider("Adaptability", t.adaptability, BaseVals.minAdapt, BaseVals.maxAdapt);

				GUILayout.BeginHorizontal ();
					if (GUILayout.Button ("Add", m250Width)) {
						manInv.add (t.getTrooper ());
						EvHandler.ExecuteEv (UIEvent.UPDATE_INV);
					}

					/*if (GUILayout.Button ("Remove", defStyle ())) {
						if (manInv.get (removeName) != null) {
							manInv.remove (removeName);
						}
					}
				removeName = EditorGUILayout.TextField (removeName, defStyle ());
				*/
				GUILayout.EndHorizontal ();
			}
		}
	}


	void SerializeGeneticTrait(GeneticTrait g){
		EditorGUILayout.LabelField("Aggresiveness", g.aggresivenessModificator.ToString ());
		EditorGUILayout.LabelField("Technology", g.technologyModificator.ToString ());
		EditorGUILayout.LabelField("Offensive", g.offensiveCapModificator.ToString ());
		EditorGUILayout.LabelField("Defensive", g.defensiveCapModificator.ToString ());
		EditorGUILayout.LabelField("Manpower", g.manpowerModificator.ToString ());
		EditorGUILayout.LabelField("Morale", g.moraleModificator.ToString ());

	}

	void SerializeTroops(TrooperHelper t){
		t.name = EditorGUILayout.TextField ("Name", t.name);

		GUILayout.BeginHorizontal ();
		t.offense = EditorGUILayout.IntField ("Offensive Capabilities", t.offense);
		t.defense = EditorGUILayout.IntField ("Defensive Capabilities", t.defense);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		t.quantity = EditorGUILayout.IntField ("Manpower", t.quantity);
		t.morale = EditorGUILayout.IntField ("Morale", t.morale);
		GUILayout.EndHorizontal();

		t.adaptability = EditorGUILayout.Slider("Adaptability", t.adaptability, BaseVals.minAdapt, BaseVals.maxAdapt);
	}


}

[CustomPropertyDrawer(typeof(Troopers))]
public class TrooperDrawer : PropertyDrawer {


	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		// Draw label
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		// Don't make child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		SerializedProperty _name = property.FindPropertyRelative ("name");
		_name.stringValue = EditorGUI.TextField (position, _name.stringValue);

		// Set indent back to what it was
		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty();
	}
}


public struct ResourceHelper{
	public string name;
	public int quantity;

	public ResourceBase getResource(){
		return new ResourceBase (this.name, this.quantity);
	}
}

public struct TrooperHelper{
	public string name;
	public int quantity, offense, defense, morale;
	public float adaptability;

	public Troopers getTrooper(){
		return new Troopers (name, quantity) {
			offensiveCap = offense,
			defensiveCap = defense,	
			morale = morale,
			adaptability = adaptability
		};
	}
}
