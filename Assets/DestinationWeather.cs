using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DestinationWeather : MonoBehaviour
{
    public TMP_Text label;
    public Image icon;
    public Sprite food;
    public Sprite enegy;
    public Color red;
    public Color purple;

    public void SetData(DestinationWeatherData weatherData)
    {
        label.SetText(weatherData.label);
        label.color = purple;
        icon.color = purple;
        if (weatherData.value < 1f)
        {
            label.color = red;
            icon.color = red;
        }
        switch (weatherData.type)
        {
            case DestinationWeatherType.FOOD:
                icon.sprite = food;
                break;
            default:
                icon.sprite = enegy;
                break;
        }
    }
}