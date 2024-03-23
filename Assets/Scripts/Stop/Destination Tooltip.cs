using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class DestinationTooltip : MonoBehaviour
{

    public RectTransform rectTransform;
    public GameObject rewardsContainer;
    public Reward reward;
    public ExtraReward extraReward;
    public ExtraReward penalty;
    public DestinationWeather weatherChange;
    public ExtraDataContainer extraDataContainer;

    public TMP_Text destinationName;
    public TMP_Text eta;

    public TMP_Text dd;
    public TMP_Text dms;

    private Destination destination;

    public void SetData(Destination _destination)
    {
        // Populate data
        destinationName.SetText(_destination.placeName);
        eta.SetText(string.Format("{0:N0}", _destination.eta));
        dd.SetText(string.Format("{0:##.####} º S", _destination.dd));
        dms.SetText(string.Format("{0:##.####} º W", _destination.dms));

        // Determine position
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;
        pivotX -= 0.6F;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;

        destination = _destination;

        // Render rewards   
        foreach (Transform child in rewardsContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Reward _reward in destination.rewards)
        {
            reward.SetData(_reward.value, _reward.type);
            Instantiate(reward, rewardsContainer.transform);
        }

        // Render extra rewards
        if (destination.extraRewards.Count > 0)
        {
            extraDataContainer.SetData("Extra Rewards");
            ExtraDataContainer instantiatedExtraDataContainer = Instantiate(extraDataContainer, rewardsContainer.transform);
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
            ExtraDataContainer instantiatedExtraDataContainer = Instantiate(extraDataContainer, rewardsContainer.transform);
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
            ExtraDataContainer instantiatedExtraDataContainer = Instantiate(extraDataContainer, rewardsContainer.transform);
            foreach (DestinationWeatherData _weatherChange in destination.weatherChanges)
            {
                weatherChange.SetData(_weatherChange);
                Instantiate(weatherChange, instantiatedExtraDataContainer.rewardsContainer.transform);
            }

        }
    }
}
