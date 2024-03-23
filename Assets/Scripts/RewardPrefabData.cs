using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardPrefabData : MonoBehaviour
{
    public Reward reward;
    public TMP_Text label;
    public TMP_Text value;
    public Image valueLabel;
    public Sprite energySprite;
    public Sprite foodSprite;
    public Sprite passengerSprite;
    public Sprite cartSprite;
    public RewardType type = RewardType.MAX_ENERGY;

    void Awake()
    {
        label.SetText(reward.type.ToString());
        value.SetText(string.Format("{0:0.##}", reward.value));
        valueLabel.sprite = energySprite;
        switch (reward.type)
        {
            case RewardType.ENERGY:
                valueLabel.sprite = energySprite;
                break;
            case RewardType.MAX_FOOD:
                valueLabel.sprite = foodSprite;
                break;
            case RewardType.FOOD:
                valueLabel.sprite = foodSprite;
                break;
            case RewardType.PASSENGER:
                valueLabel.sprite = passengerSprite;
                break;
            case RewardType.CART:
                valueLabel.sprite = cartSprite;
                break;
            default:
                break;
        }
    }
}
