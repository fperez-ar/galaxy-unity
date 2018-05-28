using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICelestialBodyElement : UIListElement<CelestialBody>, IPointerClickHandler
{

	public Text bodyNameText;
	public Text raceNameText;

	public override void set (CelestialBody c)
	{
		this.gameObject.SetActive (true);
		bodyNameText.text = c.name;

		Planet p = (Planet) c;
		if ( p != null) {
			if ( p.hasCivilization ) {
				raceNameText.text = p.dominantSpecies.name;
			}
		}
		refObj = c;
	}

	public override void unset ()
	{
		base.unset ();
	}

	public void GoToPlanet(){
		EvHandler.ExecuteEv (UIEvent.ENTER_PLT, refObj.transform);
	}


	public void OnPointerClick (PointerEventData eventData)
	{
		if (GameMode.isMode (GameState.NAVEGATION)) {
			//Navigate to that planet...
		}
	}
}
