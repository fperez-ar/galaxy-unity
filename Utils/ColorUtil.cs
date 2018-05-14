
using UnityEngine;

public static class ColorUtil {
	


	public static Color getOpposite(Color c){
		return new Color (1-c.r, 1-c.g, 1-c.b);
	}

	public static Color getContrastBorW(Color c){
		float l = c.getLuminance ();
		if (l <= 0.1883f)
			return Color.black;
		else if (l >= 0.175f) 
			return Color.white;
		
		return Color.white;
	}

	public static float minColorComponent(this Color c){
		return Mathf.Min (c.r, c.g, c.b);
	}


	 //http://www.niwa.nu/2013/05/math-behind-colorspace-conversions-rgb-hsl/
	public static float getLuminance(this Color c){
		return ( c.maxColorComponent + c.minColorComponent () ) / 2;
	}
}
