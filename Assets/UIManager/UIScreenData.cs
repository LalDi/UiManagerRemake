using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SCREENKEY
{
	EMPTY,
	EXAMPLEMENU,
	EXAMPLEPOPUP,
}

class ScreenKeyComparer : IEqualityComparer<SCREENKEY>
{
    public bool Equals(SCREENKEY x, SCREENKEY y)
    {
        return x == y;
    }
    public int GetHashCode(SCREENKEY obj)
    {
        return (int)obj;
    }
}

[Serializable]
public class ScreenData
{
	public SCREENKEY key;
	public Screen prefab;
}

[CreateAssetMenu(menuName = "Data/ScreenData")]
public class UIScreenData : ScriptableObject
{
	public List<ScreenData> screenList;
}
