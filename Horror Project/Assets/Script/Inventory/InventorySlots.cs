using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InventorySlots : MonoBehaviour
{
    [SerializeField]
    public List<Slot> slots = new List<Slot>();
    private void Awake()
    {
        AddSlots();
        //nada m�s "despertar" busca todas los Gameobjects de la UI para a�adirlos a la lista
        //despu�s en inputPlayer se ocultar�n en el start(justo un instantante despu�s de este awake)
        //as� el player nunca ver� la UI del inventario en un inicio
                                            

    }
    public void AddSlots()
    {
        slots.AddRange(FindObjectsOfType<Slot>());
        Comparison<Slot> customerComparer = new Comparison<Slot>(CompareSlots);
        slots.Sort(customerComparer);

    }
    private static int CompareSlots(Slot x, Slot y)
    {
        return x.numberSlot.CompareTo(y.numberSlot);
    }
    public void CheckSpaceAvailable(InventoryItemData item)
    {
        for (int i = 0; i < 6; i++)
        {
            Debug.Log("asss");

            if (slots[i].isOcuppied == false)
                {
                    slots[i].AddItem(item);

                    i = 6;

                }

        }
    }
    public bool FindItem(TypeItem itemType, int IDitem)
    {
        bool exists;
        exists=slots.Exists(j => j.isOcuppied==true && j.item.type == itemType && j.item.id == IDitem);
        if(exists==true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool FindItem(TypeItem itemType)
    {
        bool exists;
        exists = slots.Exists(j => j.isOcuppied == true && j.item.type == itemType);
        if (exists == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void DeleteItem(TypeItem itemType)
    {
        bool exists;
        exists = slots.Exists(j => j.isOcuppied == true && j.item.type == itemType);
        if (exists == true)
        {
            int i = slots.FindLastIndex(j => j.isOcuppied == true && j.item.type == itemType);
            slots[i].RemoveItem();
            slots.RemoveAt(i);
        }
        else
        {
            
        }
    }
}
 
