using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIUsePanel<T> : MonoBehaviour, IToggleable 
{
    //components
    public Animation anim;
    public InputField inputField;
	public Text consumeIntervalText;
    //internal
	protected int index = 0;
	public int[] consumeInterval = new int[]{1,2,3,5,10};
    //interface
	public event SimpleDelegate BeforeShow;
	protected bool mShown;
	public bool shown {
		get { return mShown; }
		set { mShown = value; }
	}

    protected virtual void Awake () 
	{
		if (anim == null) anim = GetComponent <Animation> ();
		consumeIntervalText.text = "x" + consumeInterval [index];
		inputField.onValueChanged.AddListener (delegate{validateInput();});
	}

    public virtual void toggle ()
	{
		if (mShown) {
			hide ();
		} else {
			OnBeforeShow ();
			show ();
		}
	}

    protected virtual void show ()
	{
		mShown = true;
		clear ();
		anim.CrossFade ("in");
	}

	public virtual void hide ()
	{
		mShown = false;
		anim.CrossFade ("out");
	}

    public virtual void toggleConsumeQuantity()
	{
		index = ++index % consumeInterval.Length;
		consumeIntervalText.text = "x" + consumeInterval [index];
	}

    protected virtual void clear()
	{
		inputField.text = "1";
	}
    
    protected virtual void OnBeforeShow ()
	{
		if (BeforeShow != null) {
			BeforeShow ();
		}
	}

    public virtual void Use(){}

    protected virtual void validateInput(){}
}