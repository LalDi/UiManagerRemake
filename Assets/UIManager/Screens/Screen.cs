using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class Screen : IScreen
{
    public Id id { get; private set; }
    public SCREENKEY key { get; private set; }

    public bool keepCached = false;
    public bool overrideManagedSorting;
    public int overrideSortValue;

	public event IScreen.ScreenDelegate onPushFinished;
	public event IScreen.ScreenDelegate onPopFinished;

	public abstract void Setup(Id id, SCREENKEY key);
	public abstract void OnSetup(Data data);
	public abstract void OnHierFixed();
	public abstract void InAnimEnd();
	public abstract void OnHiding();
	public abstract void OutAnimEnd();

	public abstract void OnPush();
	public abstract void OnPop();
	public abstract void OnFocus();


	public void PushFinished()
	{
		onPushFinished?.Invoke(this);
	}

	public void PopFinished()
	{
		onPopFinished?.Invoke(this);
	}

}
