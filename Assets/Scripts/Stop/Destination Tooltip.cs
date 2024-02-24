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
    public GameObject extraRewardsContainer;
    public Reward reward;
    public ExtraReward extraReward;
    public GameObject extraRewardRenderer;

    public TMP_Text destinationName;
    public TMP_Text eta;

    public TMP_Text dd;
    public TMP_Text dms;

    private List<Reward> instantiatedRewards;
    private List<ExtraReward> instantiatedExtraRewards;

    public void SetData()
    {
        // Populate data
        destinationName.SetText(RoguelikeManager.Instance.CurrentDestination.placeName);
        eta.SetText(string.Format("{0:N0}", RoguelikeManager.Instance.CurrentDestination.eta));
        dd.SetText(RoguelikeManager.Instance.CurrentDestination.dd + "º S");
        dms.SetText(RoguelikeManager.Instance.CurrentDestination.dms + "º W");

        // Determine position
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;
        pivotX -= 0.6F;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }



    void Awake()
    {
        instantiatedRewards = new List<Reward>();
        instantiatedExtraRewards = new List<ExtraReward>();
        // Render rewards
        foreach (Reward _reward in RoguelikeManager.Instance.CurrentDestination.rewards)
        {
            reward.SetData(_reward.value, _reward.type);
            Reward instantiatedReward = Instantiate(reward, rewardsContainer.transform);
            instantiatedRewards.Add(instantiatedReward);
        }
        // Render extra rewards
        if (RoguelikeManager.Instance.CurrentDestination.extraRewards.Count > 0)
        {
            extraRewardRenderer.SetActive(true);
            foreach (ExtraRewardData _extraReward in RoguelikeManager.Instance.CurrentDestination.extraRewards)
            {
                extraReward.SetData(_extraReward.label);
                Instantiate(extraReward, extraRewardsContainer.transform);
            }

        }
    }

}
