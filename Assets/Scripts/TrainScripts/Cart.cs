using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cart : MonoBehaviour
{
    public CartInfo info;
    public bool isBuilding = false;
    private float timer = 0;
    public TMP_Text timeDisplay;
    public TMP_Text typeDisplay;
    public TMP_Text disabledLabel;
    public bool isDisabled;
    public bool canBeDisabled = false;
    public int activeWorkers = 0;
    public int maxWorkers = 0;

    public int cartNumber;

    public void Init()
    {
        info = (CartInfo)CartInfo.CreateInstance(typeof(CartInfo));
        timer = info.time;
        cartNumber = TrainManager.Instance.cartsTotal;
    }

    public void SetCartNumber(int number)
    {
        cartNumber = number;
    }
    public void SetData(CartInfo _info)
    {
        typeDisplay.SetText(_info.type.ToString());
        timer = _info.time;
        info = _info;
    }
    void Update()
    {
        if (TrainManager.Instance.TimePaused) { return; }
        if (isDisabled && !isBuilding)
        {
            this.gameObject.GetComponent<Image>().sprite = info.disabledBackground;
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = info.cartBackground;
        }
        if (isBuilding)
        {
            this.gameObject.GetComponent<Image>().sprite = info.constructingBackground;
        }
        else
        {
            return;
        }
        if (TrainManager.Instance.FastForward)
        {
            timer -= Time.deltaTime * 2;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        timeDisplay.SetText(string.Format("{0:N0}s", timer));
        if (timer < 1.0f)
        {
            timeDisplay.SetText("");
            typeDisplay.SetText("");
            this.gameObject.GetComponent<Image>().sprite = info.cartBackground;
            isBuilding = false;
            canBeDisabled = false;
            isDisabled = false;
            if (info.type == CartType.FARM)
            {
                maxWorkers = 3;
                canBeDisabled = true;
                isDisabled = false;
            }
            if (info.type == CartType.REACTOR)
            {
                maxWorkers = 3;
            }
            if (info.type == CartType.HOUSING)
            {
                maxWorkers = 5;
            }
            if (info.type == CartType.EXTRA_ENGINE)
            {
                maxWorkers = 2;
            }
            TrainManager.Instance.ManufactureCart(this);
        }
    }

    public CartInfo GetInfo()
    {
        return info;
    }

    public void DestroyThisObject()
    {
        TrainManager.Instance.HandleDecouple(cartNumber);
    }
}
