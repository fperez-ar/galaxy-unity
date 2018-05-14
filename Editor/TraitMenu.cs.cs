using UnityEngine;
using UnityEditor;
using System.IO;

public class GenericMenuExample : EditorWindow
{

	// open the window from the menu item Example -> GUI Color
	[MenuItem("Editor/Trait Designer")]
	static void Init()
	{
		EditorWindow window = GetWindow<GenericMenuExample>();
		window.position = new Rect(250f, 250f, 300f, 600f);
		window.Show();
	}

	// serialize field on window so its value will be saved when Unity recompiles

	public GeneticTrait m_trait;
	bool overrideLimits = false;
	float mmax = 10, mmin = -10, max = 2, min = -2;
	float fixedMin = -2, fixedMax = 2;

	string m_name = "", m_description = "";
	float m_aggroMod = 0, m_techMod = 0,
		  m_offCapMod = 0, m_defCapMod = 0,
		  m_manMod = 0, m_moraleMod = 0;

	string defaultPath = "Assets/Prefabs/Traits/";
	GUILayoutOption mWidth = GUILayout.MaxWidth (500);

	void OnEnable()
	{
		titleContent = new GUIContent("Trait Designer");
	}

	void OnGUI()
	{
		GUILayout.Space ( 10 );
		m_trait = (GeneticTrait) EditorGUILayout.ObjectField (m_trait, typeof(GeneticTrait), true, mWidth);
		GUILayout.Space ( 10 );




		if (m_trait) {
			Serialize (m_trait);
		} else {
			AuxSerialize ();
		}

		EditorGUILayout.Separator ();
		if (GUILayout.Button("Create trait", mWidth ) )
		{
			GeneticTrait asset; 
			if (m_trait) {
				asset = m_trait;
			} else {
				asset = new GeneticTrait () {
					name = m_name,
					description = m_description,
					aggresivenessModificator = m_aggroMod,
					technologyModificator = m_techMod,
					offensiveCapModificator = m_offCapMod,
					defensiveCapModificator = m_defCapMod,
					manpowerModificator = m_manMod,
					moraleModificator = m_moraleMod
				};

			}

			string path = EditorUtility.SaveFilePanelInProject ("Save Trait file", m_name, "asset", "Where to save?", defaultPath);
			if (path != string.Empty) {
				AssetDatabase.CreateAsset (asset, path);
			}
		}
		EditorGUILayout.Separator ();
		if (GUILayout.Button ("Reset", GUILayout.MaxWidth (150)) ){
			m_trait = null;
			m_name = string.Empty;
			m_description = string.Empty;
			m_aggroMod = 0;
			m_techMod = 0;
			m_offCapMod = 0;
			m_defCapMod = 0;
			m_manMod = 0;
			m_moraleMod = 0;
		}
	}


	void Serialize(GeneticTrait g){
		
		EditorGUILayout.PrefixLabel ("Name");
		g.name = EditorGUILayout.TextField (g.name, GUILayout.MinWidth (100), mWidth);
		EditorGUILayout.PrefixLabel ("Description");
		g.description = EditorGUILayout.TextArea (g.description, mWidth);
		EditorGUILayout.Separator ();

		OverrideLimits ();
		g.aggresivenessModificator = EditorGUILayout.Slider("Aggresiveness Modifier",g.aggresivenessModificator, min, max, mWidth);
		g.technologyModificator = EditorGUILayout.Slider("Technology Modifier",   	g.technologyModificator,  min, max, mWidth);
		g.offensiveCapModificator = EditorGUILayout.Slider("OffensiveCap Modifier", g.offensiveCapModificator,min, max, mWidth);
		g.defensiveCapModificator = EditorGUILayout.Slider("DefensiveCap Modifier", g.defensiveCapModificator,min, max, mWidth);
		g.manpowerModificator = EditorGUILayout.Slider("Manpower Modifier", 	  	g.manpowerModificator,   min, max, mWidth);
		g.moraleModificator = EditorGUILayout.Slider("Morale Modifier", 	  		g.moraleModificator,min, max, mWidth);

	}


	void AuxSerialize(){
		EditorGUILayout.PrefixLabel ("Name");
		m_name = EditorGUILayout.TextField (m_name, GUILayout.MinWidth (100), mWidth);
		EditorGUILayout.PrefixLabel ("Description");
		m_description = EditorGUILayout.TextArea (m_description, mWidth);
		EditorGUILayout.Separator ();

		OverrideLimits ();
		m_aggroMod = EditorGUILayout.Slider ("Aggresiveness Modifier", m_aggroMod, min, max, mWidth);
		m_techMod = EditorGUILayout.Slider ("Technology Modifier", m_techMod, min, max, mWidth);
		m_offCapMod = EditorGUILayout.Slider ("OffensiveCap Modifier", m_offCapMod, min, max, mWidth);
		m_defCapMod = EditorGUILayout.Slider ("DefensiveCap Modifier", m_defCapMod, min, max, mWidth);
		m_manMod	= EditorGUILayout.Slider ("Manpower Modifier", m_manMod, min, max, mWidth);
		m_moraleMod = EditorGUILayout.Slider ("Morale Modifier", m_moraleMod, min, max, mWidth);

	}

	void OverrideLimits(){
		overrideLimits = EditorGUILayout.Toggle ("Override limits", overrideLimits, GUILayout.MinWidth (150));
		if (overrideLimits) {
			EditorGUILayout.MinMaxSlider ("New limits", ref min, ref max, mmin, mmax, mWidth);
		} else {
			min = fixedMin;
			max = fixedMax;
		}
	}
}
