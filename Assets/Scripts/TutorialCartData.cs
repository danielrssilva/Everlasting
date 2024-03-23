using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialCartData : MonoBehaviour
{
    public TMP_Text cartType;
    public Image cartIcon;
    public TMP_Text description;

    public Sprite farm;
    public Sprite housing;
    public Sprite reactor;
    public Sprite battery;
    public Sprite extraEngine;

    public void SetData(CartType cartType, string description)
    {
        this.cartType.SetText(cartType.ToString());
        this.description.SetText(description);
        cartIcon.sprite = farm;
    }
}
