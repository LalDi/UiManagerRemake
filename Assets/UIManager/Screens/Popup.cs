using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class Popup : Screen
{
	public abstract void OnFocusLost();
}
