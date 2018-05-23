

public interface IToggleable {

	bool shown { get; set; }
	event SimpleDelegate BeforeShow;
	void toggle();
}
