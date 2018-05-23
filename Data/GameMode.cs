public enum GameState { 
	IDLE, 
	LOADING,
	NAVEGATION,
	COMBAT, 
	TROOP_CREATION,
	GENE_SPLICING,
}
public static class GameMode {
	public static GameState current;

	public static void setMode(GameState state) {
		current = state;
	}

	public static bool isMode(GameState state) {
		return current.Equals (state);
	}
}
