using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EScreenKey
{
	EMPTY,
	EXAMPLEMENU,
	EXAMPLEPOPUP,
    EXAMPLEMVP,
}

class ScreenKeyComparer : IEqualityComparer<EScreenKey>
{
    public bool Equals(EScreenKey x, EScreenKey y)
    {
        return x == y;
    }
    public int GetHashCode(EScreenKey obj)
    {
        return (int)obj;
    }
}

[Serializable]
public class ScreenData
{
	public EScreenKey key;
	public Screen prefab;
}

[CreateAssetMenu(menuName = "Data/ScreenData")]
public class ScreenKeyData : ScriptableObject
{
	public List<ScreenData> screenList;
}
