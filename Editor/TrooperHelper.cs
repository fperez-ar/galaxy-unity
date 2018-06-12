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
