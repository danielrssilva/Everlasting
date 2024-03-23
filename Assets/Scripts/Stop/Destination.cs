using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : Stop
{
    public List<ExtraRewardData> extraRewards;
    public List<DestinationPenaltyData> penalties;
    public List<DestinationWeatherData> weatherChanges;
    public float dd;
    public float dms;
    public int stopsCount; 

    public void SetData(float eta, string placeName, float dd, float dms, List<Reward> rewards, List<ExtraRewardData> extraRewards)
    {
        this.eta = eta;
        this.placeName = placeName;
        this.dd = dd;
        this.dms = dms;
        this.rewards = rewards;
        this.extraRewards = extraRewards;
        this.penalties = new List<DestinationPenaltyData>();
        this.weatherChanges = new List<DestinationWeatherData>();
        this.isDestination = true;
        this.stopsCount = 5;
    }

    public void SetData(float eta, string placeName, float dd, float dms, List<Reward> rewards, List<ExtraRewardData> extraRewards, List<DestinationPenaltyData> penalties, List<DestinationWeatherData> weatherChanges, int stopsCount)
    {
        this.eta = eta;
        this.placeName = placeName;
        this.dd = dd;
        this.dms = dms;
        this.rewards = rewards;
        this.extraRewards = extraRewards;
        this.penalties = penalties;
        this.weatherChanges = weatherChanges;
        this.stopsCount = stopsCount;
        this.isDestination = true;
    }
}
