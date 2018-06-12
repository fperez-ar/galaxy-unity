public struct ResourceHelper{
	public string name;
	public int quantity;

	public ResourceBase getResource(){
		return new ResourceBase (this.name, this.quantity);
	}
}
