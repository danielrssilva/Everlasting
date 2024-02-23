using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode()]
public class DestinationOption : MonoBehaviour
{
    public GameObject rewardsContainer;
    public GameObject extraRewardsContainer;
    public Reward reward;
    public ExtraReward extraReward;

    public TMP_Text destinationName;

    public TMP_Text dd;
    public TMP_Text dms;

    public TMP_Text locations;
    public TMP_Text difficulty;
    public Destination destination;

    public void Init(Destination destination)
    {
        destinationName.SetText(destination.placeName);
        dd.SetText(destination.dd + "º S");
        dms.SetText(destination.dms + "º W");

        DestinationOption instantiated = Instantiate(this, GameObject.FindGameObjectWithTag("AvailableDestinations").transform);
        foreach (Reward _reward in destination.rewards)
        {
            reward.SetData(_reward.value, _reward.type);
            Instantiate(reward, instantiated.rewardsContainer.transform);
        }
        // Render extra rewards
        if (destination.extraRewards.Count > 0)
        {
            extraRewardsContainer.SetActive(true);
            foreach (ExtraRewardData _extraReward in destination.extraRewards)
            {
                extraReward.SetData(_extraReward.label);
                Instantiate(extraReward, instantiated.extraRewardsContainer.transform);
            }
        }

        instantiated.destination = destination;
    }

    public void Click()
    {
        RoguelikeManager.Instance.ChooseDestination(destination);
    }
}
