using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LUCAS GARCÍA SCRIPT//
[CreateAssetMenu(fileName = "New Key", menuName = "InventorySO/KeySO")]
public class KeySO : InventoryItemData
{

    void Start()
    {
        type = TypeItem.Key;
    }


}
