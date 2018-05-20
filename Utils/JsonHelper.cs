using System.Collections;
using System.Collections.Generic;


public static class JsonHelper  {
	
	//Usage:
	//YouObject[] objects = JsonHelper.getJsonArray<YouObject> (jsonString);
	public static T[] getJsonArray<T>(string json)
	{
		UnityEngine.Debug.Log("loading \b"+json);
		Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>> (json);
		return wrapper.array;
	}
	//Usage:
	//string jsonString = JsonHelper.arrayToJson<YouObject>(objects);
	public static string arrayToJson<T>(T[] array)
	{
		Wrapper<T> wrapper = new Wrapper<T> ();
		wrapper.array = array;
		return UnityEngine.JsonUtility.ToJson (wrapper, true);
	}

	[System.Serializable]
	private class Wrapper<T>
	{
		public T[] array;
	}
}
