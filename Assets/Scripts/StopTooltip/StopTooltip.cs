using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class StopTooltip : MonoBehaviour
{

    public RectTransform rectTransform;

    public TMP_Text stopName;
    public TMP_Text reward;
    public TMP_Text eta;
    public Image backgroundImage;
    public Image icon;

    public Sprite visited;
    public Sprite cart;
    public Sprite passenger;

    public Sprite passengerIcon;
    public Sprite cartIcon;
    public Sprite visitedIcon;

    public void SetData(Stop stop)
    {
        //backgroundImage.sprite = background;
        switch (stop.rewards[0].type)
        {
            case RewardType.PASSENGER:
                backgroundImage.sprite = passenger;
                icon.sprite = passengerIcon;
                break;
            case RewardType.CART:
                backgroundImage.sprite = cart;
                icon.sprite = cartIcon;
                break;
            default:
                break;
        }

        // Populate data
        stopName.SetText(stop.placeName);
        eta.SetText(string.Format("{0:N0}", stop.eta));

        // [INFO] Change if the reward becomes more than one!!
        reward.SetText(string.Format("{0:N0} " + stop.rewards[0].type.ToString(), stop.rewards[0].value));
        if (stop.visited)
        {
            reward.SetText(string.Format("Visited <size=7>[{0:N0} " + stop.rewards[0].type.ToString() + "]</size>", stop.rewards[0].value));
            icon.sprite = visitedIcon;
            backgroundImage.sprite = visited;
        }
        // Determine position
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;
        pivotY -= 0.6F;
        pivotX -= 0.2F;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }
}
