public enum GameState
{
	IDLE,
	LOADING,
	NAVEGATION,
	COMBAT,
	TROOP_CREATION,
	GENE_SPLICING,
}
public static class GameMode
{
	private static GameState current;
	private static GameState last;

	public static void setMode (GameState state)
	{
		last = current;
		current = state;

		UnityEngine.Debug.LogWarning ("GAME MODE SET TO: " + state);
		switch (state) {
		//when interacting with ui, disable exploration controls
		case GameState.COMBAT:
		case GameState.GENE_SPLICING:
		case GameState.TROOP_CREATION:
			EvHandler.ExecuteEv (GameEvent.UI_PHASE);
			break;

		case GameState.NAVEGATION:
			EvHandler.ExecuteEv (GameEvent.EXPLORE_PHASE);
			break;
		}
	}


	public static void reset ()
	{
		if (last.Equals (current))
			return;
		setMode (last);
	}

	public static bool isMode (GameState state)
	{
		return current.Equals (state);
	}
}
