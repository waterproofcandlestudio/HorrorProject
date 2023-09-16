using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//LUCAS GARCÍA SCRIPT//
public class IsBatteryDropped : MonoBehaviour, IDropHandler
{
    [SerializeField]
    ManageLantern manageLantern;
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.selectedObject.GetComponent<Slot>().isOcuppied==true)
        {
            if (eventData.selectedObject.GetComponent<Slot>().item.type == TypeItem.Battery)
            {
                manageLantern.OnDropBattery();
            }
        }


    }

}
