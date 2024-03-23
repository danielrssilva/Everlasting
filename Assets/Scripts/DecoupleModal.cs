using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class DecoupleModal : MonoBehaviour
{
    public RectTransform rectTransform;
    public TMP_Text description;
    public bool keepPassengers;
    public int cartCount;
    public int cartNumber;
    public int passengerCount;

    public void SetData(int cartCount, int passengerCount, int cartNumber)
    {

        gameObject.SetActive(true);
        description.SetText(string.Format("Doing so will decouple the chosen cart and every other behind it <size=6>[{0:N0} cart]</size>!\n\n<b>This action cannot be undone!</b>\n\ndo you want to keep your passengers <size=6>[{1:N0} passengers]</size> or leave the behind?", cartCount, passengerCount));

        // Determine position
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;
        pivotY -= 0.2F;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
        this.cartCount = cartCount;
        this.cartNumber = cartNumber;
        this.passengerCount = passengerCount;
    }

    public void KeepPassengers()
    {
        keepPassengers = true;
    }
    public void LeavePassengers()
    {
        keepPassengers = false;
    }

    public void ConfirmDecouple()
    {
        gameObject.SetActive(false);
        TrainManager.Instance.ConfirmDecouple(keepPassengers, passengerCount, cartCount, cartNumber);
    }
}
