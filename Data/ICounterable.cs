using System.Collections;
using System.Collections.Generic;

public interface ICounterable {
	
	int counterQuantity {
		get;
	}

	float percQuantity {
		get;
	}

	string getStringQuantity (char separator = ':');
	string getStringQuantity (string separator);
	string getName ();

}
