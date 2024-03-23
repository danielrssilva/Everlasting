using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartManager : MonoBehaviour
{
    public static CartManager Instance;

    public Cart cart;
    private TrainManager trainManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        trainManager = GetComponent<TrainManager>();
    }

    public void SpawnCart()
    {
        cart.SetCartNumber(trainManager.cartsTotal);
        Cart instance = Instantiate(cart, GameObject.FindGameObjectWithTag("TrainController").transform);
        trainManager.AddEmptyCart(instance);
    }

    public void ChangeCart(Cart _cart, CartInfo _cartInfo)
    {
        if (_cart.info.type == _cartInfo.type) { return; }
        if(!trainManager.CanBuildCart(_cartInfo)) { return ; }

        _cart.gameObject.GetComponent<Image>().sprite = _cartInfo.constructingBackground;
        _cart.SetData(_cartInfo);
        _cart.isBuilding = true;
    }
}
