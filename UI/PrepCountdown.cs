using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepCountdown : MonoBehaviour {

	public Text timerSign;
	public int countdownOffset = 3;
	Timer time;

	void Awake () {
		EvHandler.RegisterEv (GameEvent.COMBAT_CALCULATION, ResetTimer);
		EvHandler.RegisterEv (GameEvent.PREPARATION_PHASE, BeginCountdown);
		gameObject.SetActive (false);
	}

	void ResetTimer() {
		print("Reset countdown");
		time = null;
		timerSign.text = string.Empty;
		gameObject.SetActive (false);
	}

	void BeginCountdown(object countdown){
		gameObject.SetActive (true);

		int finalCount = countdownOffset + (int)countdown;
		time = new Timer ( finalCount );
		print("Begin countdown "+finalCount);
	}

	void EndCountdown(){
		print("End countdown");
		EvHandler.ExecuteEv (GameEvent.COMBAT_CALCULATION);
//		time = null;
//		timerSign.text = string.Empty;
//		gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		
		UpdateTimeSign ();

		if (time.check ()) {
			EndCountdown ();
		}
		
	}

	void UpdateTimeSign(){
		string timeLeft = (time.intervalo - Mathf.Round(time.timeAccumulator)).ToString ();
		timerSign.color = Color.Lerp (Color.green, Color.red, time.progress);
		timerSign.text = timeLeft;

	}
}
