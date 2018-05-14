using System.Collections;
[System.Serializable]
public class ResourceBase : ICounterable{
	
	public const int maxResourePoints = 10;

	public string name = "";
	public int quantity = 0;

	public int counterQuantity {
		get { return quantity; }
	}

	public float percQuantity {
		get { return (float)quantity / BaseVals.maxResource; }
	}

	public ResourceBase ()
	{
		name = "";
		quantity = 0;
	}
	public ResourceBase (string _name, int _quantity)
	{	
		name = _name;
		quantity = _quantity;
	}

	public ResourceBase (int _quantity)
	{	
		quantity = _quantity;
	}

	public override string ToString () {
		return name;
	}
		
	public string getName () {
		return name;
	}

	public string getStringQuantity(char separator = ':') {
		return name + separator +" "+ quantity;
	}

	public string getStringQuantity(string separator) {
		return name + separator +" "+ quantity;
	}

	public static implicit operator string(ResourceBase resource) {
		//System.Text.StringBuilder res = new System.Text.StringBuilder ();
		return resource.name;
	}

}
