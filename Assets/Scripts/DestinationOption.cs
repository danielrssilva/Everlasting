using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class DestinationOption : MonoBehaviour
{
    public GameObject rewardsContainer;
    public Reward reward;
    public ExtraReward extraReward;
    public ExtraReward penalty;
    public DestinationWeather weatherChange;
    public ExtraDataContainer extraDataContainer;

    public TMP_Text destinationName;

    public TMP_Text dd;
    public TMP_Text dms;

    public TMP_Text locations;
    public TMP_Text difficulty;
    public Destination destination;

    public void Init(Destination destination)
    {
        destinationName.SetText(destination.placeName);
        dd.SetText(string.Format("{0:##.####} º S", destination.dd));
        dms.SetText(string.Format("{0:##.####} º W", destination.dms));
        locations.SetText(string.Format("<b>{0:N0}</b> <size=6>locations</size>", destination.stopsCount));

        DestinationOption instantiated = Instantiate(this, GameObject.FindGameObjectWithTag("AvailableDestinations").transform);
        foreach (Reward _reward in destination.rewards)
        {
            reward.SetData(_reward.value, _reward.type);
            Instantiate(reward, instantiated.rewardsContainer.transform);
        }
        // Render extra rewards
        if (destination.extraRewards.Count > 0)
        {
            extraDataContainer.SetData("Extra Rewards");
            ExtraDataContainer instantiatedExtraDataContainer = Instantiate(extraDataContainer, instantiated.rewardsContainer.transform);
            foreach (ExtraRewardData _extraReward in destination.extraRewards)
            {
                extraReward.SetData(_extraReward.label);
                Instantiate(extraReward, instantiatedExtraDataContainer.rewardsContainer.transform);
            }
        }
        // Render Penalties
        if (destination.penalties.Count > 0)
        {
            extraDataContainer.SetData("Penalties");
            ExtraDataContainer instantiatedExtraDataContainer = Instantiate(extraDataContainer, instantiated.rewardsContainer.transform);
            foreach (DestinationPenaltyData _penalty in destination.penalties)
            {
                penalty.SetData(_penalty.label);
                Instantiate(penalty, instantiatedExtraDataContainer.rewardsContainer.transform);
            }

        }
        // Render Weather changes
        if (destination.weatherChanges.Count > 0)
        {
            extraDataContainer.SetData("Weather changes");
            ExtraDataContainer instantiatedExtraDataContainer = Instantiate(extraDataContainer, instantiated.rewardsContainer.transform);
            foreach (DestinationWeatherData _weatherChange in destination.weatherChanges)
            {
                weatherChange.SetData(_weatherChange);
                Instantiate(weatherChange, instantiatedExtraDataContainer.rewardsContainer.transform);
            }

        }

        instantiated.destination = destination;
    }

    public void Click()
    {
        RoguelikeManager.Instance.ChooseDestination(destination);
    }
}
