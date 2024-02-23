using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(RoguelikeManager))]
public class StopUpdater : MonoBehaviour
{
    public static RoguelikeManager roguelikeManager;

    public TMP_Text eta;
    public TMP_Text placeName;
    public Image rewardType;

    public Sprite energy;
    public Sprite food;
    public Sprite cart;
    public Sprite passenger;
    public Sprite visited;

    void Start()
    {
        roguelikeManager = GetComponent<RoguelikeManager>();
    }

    void Update()
    {
        placeName.SetText(roguelikeManager.NextStop.placeName);
        eta.SetText(roguelikeManager.NextStop.eta.ToString());
        switch (roguelikeManager.NextStop.rewards[0].type)
        {
            case RewardType.MAX_ENERGY:
                rewardType.sprite = energy;
                break;
            case RewardType.ENERGY:
                rewardType.sprite = energy;
                break;
            case RewardType.MAX_FOOD:
                rewardType.sprite = food;
                break;
            case RewardType.FOOD:
                rewardType.sprite = food;
                break;
            case RewardType.PASSENGER:
                rewardType.sprite = passenger;
                break;
            case RewardType.CART:
                rewardType.sprite = cart;
                break;
            case RewardType.VISITED:
                rewardType.sprite = visited;
                break;
            default:
                break;
        }
    }
}
