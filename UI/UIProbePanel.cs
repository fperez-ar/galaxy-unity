using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProbePanel : UIUsePanel<ResourceBase>, IToggleable {

	public UIResourceElement uiUseElement;

	protected override void Awake ()
	{
		base.Awake();
		EvHandler.RegisterEv (UIEvent.SHOW_PROBE_PANEL, setResource);
	}

	public void add()
	{
		int q = int.Parse (inputField.text);
		q += consumeInterval [index];
		inputField.text = q.ToString ();
	}

	public void subs()
	{
		int q = int.Parse (inputField.text);
		q -= consumeInterval [index];
		inputField.text = q.ToString ();
	}

	public void setResource(object oResource)
	{
		ResourceBase r = (ResourceBase)oResource;
		uiUseElement.set (r);
		show ();
	}

	public override void Use()
	{
		int q = int.Parse (inputField.text);
		EvHandler.ExecuteEv (GameEvent.PROBE_PLT, q);
	}

	protected override void validateInput()
	{
		if (inputField.text.Equals (string.Empty)) return;
		int q = int.Parse (inputField.text);
		int max = uiUseElement.getRefObj.quantity ;
		int a = Mathf.Clamp (q, 0, max);
		inputField.text = a.ToString ();
	}

}
