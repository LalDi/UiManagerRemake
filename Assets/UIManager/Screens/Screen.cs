using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class Screen : MonoBehaviour, IScreen
{
    public IScreen.Id id { get; private set; }
    public SCREENKEY key { get; private set; }

    public bool keepCached = false;
    public bool overrideManagedSorting;
    public int overrideSortValue;

	public event IScreen.ScreenDelegate onPushFinished;
	public event IScreen.ScreenDelegate onPopFinished;

	public void Setup(IScreen.Id id, SCREENKEY key)
	{
		this.id = id;
		this.key = key;
	}

	public abstract void OnSetData(IScreen.Data data);
	public abstract void OnHierFixed();
	public abstract void OnShowing();
	public abstract void InAnimEnd();
	public abstract void OnHiding();
	public abstract void OutAnimEnd();


	public void PushFinished()
	{
		onPushFinished?.Invoke(this);
	}

	public void PopFinished()
	{
		onPopFinished?.Invoke(this);
	}
}
