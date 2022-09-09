using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorModel : Model
{
    private Color _color;
    public Color color { get => _color; set => _color = value; }

    public UnityAction<Color> onColorChange;
}
