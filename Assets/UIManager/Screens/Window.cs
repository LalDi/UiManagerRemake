using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Window : Screen
{
	public abstract void OnReFocus();
	public abstract void OnFocusLost();

}
