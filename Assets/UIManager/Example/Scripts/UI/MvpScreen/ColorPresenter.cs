using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPresenter : PresenterBase<ColorModel>
{
    enum EColorParamKind
    {
        R,G,B,A
    }
    [Serializable]
    class ColorParamView
    {
        public Slider parameterSlider;
        public Text parameterValue;
        public EColorParamKind kind;

        public Color GetNewColor(float value, Color baseColor)
        {
            parameterValue.text = value.ToString();
            float colorValue = value / 255f;
            Color newColor = baseColor;
            switch (kind)
            {
                case EColorParamKind.R: newColor.r = colorValue;    break;
                case EColorParamKind.G: newColor.g = colorValue;    break;
                case EColorParamKind.B: newColor.b = colorValue;    break;
                case EColorParamKind.A: newColor.a = colorValue;    break;
            }

            return newColor;  
        }
    }

    [SerializeField] private Image imageView;

    [SerializeField] private ColorParamView[] colorViews;

    private void Awake()
    {
        model = new ColorModel();
        model.color = Color.white;
        SetColorSample(model.color);
        foreach (var item in colorViews)
        {
            item.parameterSlider.onValueChanged.AddListener((value) => SetColor(item.GetNewColor(value, model.color)));
        }
        model.onColorChange += SetColorSample;
    }

    public void SetColor(Color newColor)
    {
        model.color = newColor;
        model.onColorChange?.Invoke(newColor);
    }

    public void SetColorSample(Color value)
    {
        imageView.color = value;
    }
}
