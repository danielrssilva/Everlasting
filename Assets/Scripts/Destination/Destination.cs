using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : Stop
{
    public List<ExtraRewardData> extraRewards;
    public string dd;
    public string dms;

    public void SetData(float eta, string placeName, string dd, string dms, List<Reward> rewards, List<ExtraRewardData> extraRewards)
    {
        this.eta = eta;
        this.placeName = placeName;
        this.dd = dd;
        this.dms = dms;
        this.rewards = rewards;
        this.extraRewards = extraRewards;
        this.isDestination = true;
    }
}
