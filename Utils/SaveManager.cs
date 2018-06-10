using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager {

	public static char sep = Path.DirectorySeparatorChar;
	public static string baseDir =
		Application.dataPath + sep + "Resources" + sep + "ply" + sep;

	public static void Load(PlayerShip pShip) {

		var sp = LoadSpecies (baseDir + sep + Filenames.Species);
		if (sp != null)
			pShip.dominantSpecies = sp;

		LoadShipPrefs(pShip);

		string troopDir = baseDir + Filenames.PlyTroopsDir;
		if (Directory.Exists (troopDir)) {
			pShip.addTroopers (LoadTroopers (troopDir));
		}

		string resDir = baseDir + Filenames.PlyResourcesDir;
		if (Directory.Exists (resDir)) {
			ResourceBase[] rs = LoadResources (resDir);
			if ( rs != null ) pShip.addResources (rs);
		}
		string genesDir = baseDir + Filenames.PlyGenesDir;
		if (Directory.Exists (genesDir)) {
			pShip.addGenes (LoadGenes (genesDir));
		}
		
		EvHandler.ExecuteEv(UIEvent.UPDATE_INV);
	}

	public static void Save(PlayerShip pShip) {
		SaveShipPrefs (pShip);
		SaveSpecies (pShip.dominantSpecies);
		SaveTroopers (pShip);
		SaveGenes (pShip);
		SaveResources (pShip);
	}

	public static void Save(PlayerShip p, SimpleDelegate onSaveFinish) {
		Save (p);
		onSaveFinish ();
	}

	public static void SaveTroopers(PlayerShip p) {
		string troopDir = baseDir + Filenames.PlyTroopsDir;
		CreateDir (troopDir);
		Save (troopDir, p.getAllTroopers ());
	}

	public static void SaveGenes(PlayerShip p) {
		string genesDir = baseDir + Filenames.PlyGenesDir;
		CreateDir (genesDir);
		Save (genesDir, p.getAllGenes ());
	}

	public static void SaveResources(PlayerShip p) {
		string resDir = baseDir + Filenames.PlyResourcesDir;
		CreateDir (resDir);
		Save (resDir, p.getAllResources ());
	}

	#region troopers
	public static void Save(string path, Troopers[] ts) {
		for (int i = 0; i < ts.Length; i++) {
			Save (path, ts [i]);
		}
	}

	public static void Save(string path, Troopers t) {
		string filepath = path + sep + t.name + Filenames.resExt;
		string jsonTroop = JsonUtility.ToJson (t);
		File.WriteAllText (filepath, jsonTroop);
	}

	public static Troopers[] LoadTroopers(string path) {
		string[] trooperFiles = Directory.GetFiles(path, Filenames.resSearchPattrn);
		Troopers[] troopers = new Troopers[trooperFiles.Length];
		for (int i = 0; i < trooperFiles.Length; i++) {
			troopers[i] = LoadTrooper (trooperFiles[i]);
		}
		return troopers;
	}

	public static Troopers LoadTrooper(string path) {
		string jsonTroop = File.ReadAllText (path);
		Troopers t = JsonUtility.FromJson<Troopers> (jsonTroop);
		return t;
	}
	#endregion

	#region genes
	public static void Save(string path, GeneticTrait[] gs) {
		for (int i = 0; i < gs.Length; i++) {
			Save (path, gs [i]);
		}
	}

	public static void Save(string path, GeneticTrait g) {
		string filepath = path + sep + g.name + Filenames.resExt;
		string jsonGene = JsonUtility.ToJson (g);
		File.WriteAllText (filepath, jsonGene);
	}

	public static GeneticTrait[] LoadGenes(string path) {
		string[] geneFiles = Directory.GetFiles(path, Filenames.resSearchPattrn);
		GeneticTrait[] genes = new GeneticTrait[geneFiles.Length];
		for (int i = 0; i < geneFiles.Length; i++) {
			genes[i] = LoadGene (geneFiles[i]);
		}
		return genes;
	}

	public static GeneticTrait LoadGene(string path) {
		string jSonGene = File.ReadAllText (path);
		GeneticTrait g = ScriptableObject.CreateInstance<GeneticTrait> ();
		g.name = Path.GetFileNameWithoutExtension (path);
		JsonUtility.FromJsonOverwrite (jSonGene, g);
		return g;
	}
	#endregion

	#region resources
	public static void Save(string path, ResourceBase[] rs) {
		string filepath = path + sep + Filenames.PlyResourcesFile;
		if ( File.Exists (filepath)) File.Delete (filepath);
		if (rs.Length == 0) return;

		string jArray = JsonHelper.arrayToJson<ResourceBase> (rs);
		File.WriteAllText (filepath, jArray);
	}

	public static ResourceBase[] LoadResources(string path) {
		string filepath = path + sep + Filenames.PlyResourcesFile ;
		if (!File.Exists (filepath)) { return null; }

		return JsonHelper.getJsonArray<ResourceBase> (File.ReadAllText (filepath));
	}
	#endregion

	private static void CreateDir(string path ) {
		if (Directory.Exists (path)) {
			Directory.Delete (path, true);
		}
		Directory.CreateDirectory (path);
	}


	private static void LoadShipPrefs(PlayerShip pShip) {
		if (PlayerPrefs.HasKey ("population")) {
			pShip.dominantSpecies.population = PlayerPrefs.GetInt ("population");
		}
	}

	private static void SaveShipPrefs(PlayerShip pShip) {
		PlayerPrefs.SetInt("population", pShip.dominantSpecies.population );
		PlayerPrefs.Save ();
	}

	private static Species LoadSpecies(string path) {
		if (!File.Exists (path)) return null;
		return JsonUtility.FromJson<Species> (File.ReadAllText (path));
	}

	private static void SaveSpecies(Species sp) {
		string filepath = baseDir + sep + Filenames.Species;
		if ( File.Exists (filepath)) File.Delete (filepath);
		string jsonSpecies = JsonUtility.ToJson (sp);
		File.WriteAllText (filepath, jsonSpecies);
	}
}
