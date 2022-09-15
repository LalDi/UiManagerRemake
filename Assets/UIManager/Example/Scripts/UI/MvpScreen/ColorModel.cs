using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ColorModel : Model
{
	[SerializeField] private Color _color;
    public Color color { get => _color; set => _color = value; }

    public UnityAction<Color> onColorChange;

	private const string _saveKeyR = "SaveKeyR";
	private const string _saveKeyG = "SaveKeyG";
	private const string _saveKeyB = "SaveKeyB";
	private const string _saveKeyA = "SaveKeyA";

	public void Init()
	{
		float r = PlayerPrefs.GetFloat(_saveKeyR, 1f);
		float g = PlayerPrefs.GetFloat(_saveKeyG, 1f);
		float b = PlayerPrefs.GetFloat(_saveKeyB, 1f);
		float a = PlayerPrefs.GetFloat(_saveKeyA, 1f);

		color = new Color(r, g, b, a);
	}

	public void SaveColor()
	{
		PlayerPrefs.SetFloat(_saveKeyR, _color.r);
		PlayerPrefs.SetFloat(_saveKeyG, _color.g);
		PlayerPrefs.SetFloat(_saveKeyB, _color.b);
		PlayerPrefs.SetFloat(_saveKeyA, _color.a);
	}

}
