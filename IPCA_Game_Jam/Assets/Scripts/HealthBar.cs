using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        fill.color = gradient.Evaluate(1f);
    }

    public void IncreaseHealth(int h)
    {
        SetHealth((int)slider.value + h);
    }

    public void DecreaseHealth(int h)
    {
        SetHealth((int)slider.value - h);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
