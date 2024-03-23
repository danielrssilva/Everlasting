using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class ManagerModal : MonoBehaviour
{
    public RectTransform rectTransform;

    [Header("Cart data")]
    private Cart cart;

    [Header("Cart display labels")]
    public Image cartIcon;
    public TMP_Text typeDisplay;

    [Header("Left container labels")]
    public GameObject workerContainer;
    public TMP_Text workerCount;
    public TMP_Text workerTotal;
    public GameObject removeWorkerButton;
    public GameObject addWorkerButton;

    [Header("Middle container labels")]
    public GameObject outputContainer;
    public GameObject batteryCartModifierContainer;
    public TMP_Text batteryCartModifierValue;
    public TMP_Text outputValue;
    public TMP_Text outputLabel;
    public Image outputIcon;

    [Header("Right container labels")]
    public TMP_Text consumptionValue;

    [Header("Sprites")]
    public Image backgroundImage;
    public Sprite backgroundSprite;
    public Sprite disabableBackgroundSprite;
    public Image enableDisableButton;
    public Sprite enabledButton;
    public Sprite disabledButton;


    public void SetData(Cart _cart)
    {
        CartType _cartType = _cart.info.type;
        // Modal position
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;
        pivotY -= 0.03F;
        pivotX -= 0.3F;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;

        // Cart data
        typeDisplay.SetText(_cartType.ToString());
        cartIcon.sprite = _cart.info.cartTypeIcon;
        workerContainer.SetActive(true);
        outputContainer.SetActive(true);
        outputLabel.gameObject.SetActive(true);
        batteryCartModifierContainer.SetActive(false);
        removeWorkerButton.SetActive(false);
        addWorkerButton.SetActive(false);

        // Workers
        workerCount.SetText(_cart.activeWorkers.ToString());
        workerTotal.SetText(_cart.maxWorkers.ToString());

        // Consumption
        consumptionValue.SetText(string.Format("-{0:N2}", TrainManager.Instance.cartsEnergyConsumption));

        // Production
        if (_cartType == CartType.FARM && !_cart.isDisabled && _cart.activeWorkers > 0)
        {
            float output = 0;
            if (_cart.activeWorkers == TrainManager.Instance.farmWorkersCapacityPerCart)
            {
                output += 0.1f;
            }
            output += TrainManager.Instance.farmOutput * _cart.activeWorkers;
            outputValue.SetText(string.Format("{0:N1}", output));
            consumptionValue.SetText(string.Format("-{0:N2}", TrainManager.Instance.cartsEnergyConsumption + TrainManager.Instance.farmFoodProductionEnergyCost));
        }
        outputIcon.sprite = _cart.info.productionOutputIcon;


        // Cart specific renderer
        if (_cartType == CartType.EMPTY || _cartType == CartType.HOUSING || _cartType == CartType.REACTOR || _cartType == CartType.BATTERY)
        {
            outputContainer.SetActive(false);
        }
        if (_cartType == CartType.EMPTY)
        {
            workerContainer.SetActive(false);
        }
        if (_cartType == CartType.BATTERY)
        {
            workerContainer.SetActive(false);
            batteryCartModifierContainer.SetActive(true);
            batteryCartModifierValue.SetText("2");
            outputValue.SetText(string.Format("{0:N1}", TrainManager.Instance.batteryModifier));
            outputLabel.gameObject.SetActive(false);
        }
        if (_cartType == CartType.EXTRA_ENGINE)
        {
            outputContainer.SetActive(false);
        }
        if (_cartType == CartType.FARM || _cartType == CartType.EXTRA_ENGINE || _cartType == CartType.REACTOR)
        {
            removeWorkerButton.SetActive(true);
            addWorkerButton.SetActive(true);
        }
        if (_cartType == CartType.FARM || _cartType == CartType.EXTRA_ENGINE)
        {
            _cart.canBeDisabled = true;
        }
        if (_cartType == CartType.REACTOR)
        {
            consumptionValue.SetText(string.Format("+{0:N1}", TrainManager.Instance.reactorOutput));
        }

        // Disabler manager
        backgroundImage.sprite = backgroundSprite;
        enableDisableButton.gameObject.SetActive(false);
        if (_cart.canBeDisabled && !_cart.isBuilding)
        {
            enableDisableButton.sprite = enabledButton;
            ChangeButtonSprite(_cart);
            enableDisableButton.gameObject.SetActive(true);
            backgroundImage.sprite = disabableBackgroundSprite;
        }
        if (_cart.isBuilding || _cart.isDisabled)
        {
            removeWorkerButton.SetActive(false);
            addWorkerButton.SetActive(false);
            outputValue.SetText(string.Format("{0:N0}", 0));
        }
        cart = _cart;
    }

    public void RemoveWorker()
    {
        cart = TrainManager.Instance.RemoveWorkerFromCart(cart);
        SetData(cart);
    }


    public void AddWorker()
    {
        SetData(TrainManager.Instance.AddWorkerToCart(cart));
    }

    public void ToggleDisable()
    {
        TrainManager.Instance.ToggleDisableCart(cart);
        ChangeButtonSprite(cart);
    }

    public void ConstructCart(CartInfo cartInfo)
    {
        CartManager.Instance.ChangeCart(cart, cartInfo);
    }

    private void ChangeButtonSprite(Cart _cart)
    {
        if (_cart.isDisabled)
        {
            enableDisableButton.sprite = disabledButton;
            return;
        }
        enableDisableButton.sprite = enabledButton;
    }
}
