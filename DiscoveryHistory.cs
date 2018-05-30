using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoveryHistory
{
	protected Dictionary<string, int> history = new Dictionary<string, int> ();

	public void add (string entryName)
	{
		if (!history.ContainsKey (entryName)) {
			history.Add (entryName, 0);
		}
	}

	public void add (string entryName, int discoveryLevel)
	{
		if (!history.ContainsKey (entryName)) {
			history.Add (entryName, discoveryLevel);
		}
	}

	public bool contains(string entryName)
	{
		return history.ContainsKey (entryName);
	}

	public void modifyDiscoveryLevel(string entryName, int mod)
	{
		history [entryName] += mod;
	}

	public int getDiscoveryLevel(string entryName)
	{
		//return history.ContainsKey (entryName) ? history [entryName] : -1;
		return history [entryName];
	}
}

public static class DiscoveryProgress
{
	public const int Undiscovered = 0;
	public const int Discovered = 10;
	public const int Probed = 50;
	public const int Invaded = 100;
}
