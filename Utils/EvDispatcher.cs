using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventTypes{
	GameEvent,
	UIEvent
}

public class EvDispatcher : MonoBehaviour{ 

	public EventTypes dispatchEventType;

	public GameEvent gameEvent;
	public UIEvent uiEvent;

	public void DispatchEvent(object o){
		string ev = null;
		if (dispatchEventType.Equals (EventTypes.GameEvent)) {
			ev = gameEvent.ToString ();
		} else {
			ev = uiEvent.ToString ();
		}

		EvHandler.ExecuteEv (ev, o);
	}

	public void DispatchEvent(){
		string ev = null;
		if (dispatchEventType.Equals(EventTypes.GameEvent)) {
			ev = gameEvent.ToString ();
		} else {
			ev = uiEvent.ToString ();
		}
		EvHandler.ExecuteEv (ev);
	}

}
