using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManagerModalTrigger : MonoBehaviour, IPointerClickHandler
{
    public Cart cart;

    public void OnPointerClick(PointerEventData eventData)
    {
        ManagerModalSystem.Show(cart);
    }

    /*public void OnPointerExit(PointerEventData eventData)
    {
        ModalSystem.Hide();
    }*/
}
