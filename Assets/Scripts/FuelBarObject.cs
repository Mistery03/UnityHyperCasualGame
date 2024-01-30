using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarObject : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void setMaxFuel(float fuel)
    {
        slider.maxValue = fuel;
        gradient.Evaluate(1f);
    }
    public void SetFuel(float fuel)
    {
        slider.value = fuel;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
}
