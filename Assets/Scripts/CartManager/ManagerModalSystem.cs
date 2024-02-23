using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerModalSystem : MonoBehaviour
{
    private static ManagerModalSystem current;
    
    public ManagerModal managerModal;

    public void Awake()
    {
        current = this;
    }

    public static void Show(Cart _cart)
    {
        current.managerModal.SetData(_cart);
        current.managerModal.gameObject.SetActive(true);
        //current.cartManagerModal.transform.position = position;
    }

    public static void Hide()
    {
        current.managerModal.gameObject.SetActive(false);
    }
}
