
public static class BaseVals {

	public const string StatsFormat = "F2";
	public const float Agressiveness = 10;
	public const float Technology = 10;
	public const float OffensiveCap = 100;
	public const float DefensiveCap = 100;
	public const int Manpower = 100;
	public const float Morale = 100;
	public static UnityEngine.Color minMoraleColor = new UnityEngine.Color (0, 0.65f, 0);
	public static UnityEngine.Color maxMoraleColor = new UnityEngine.Color (0.7f, 0, 0);

	public const float minAdapt = -2f, maxAdapt = 2f;

	public const int maxTroopersPerUnit = 1000;
	public const int maxTraits = 3;
	public const int maxResource = 100;
	public const int minResPerPlanet = 0, maxResPerPlanet = 3;
	public const int maxPopulation = int.MaxValue;

	public const string BaseTroops = "Infantry";
}
