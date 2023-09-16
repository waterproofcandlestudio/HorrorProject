using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LUCAS GARCÍA SCRIPT//
[CreateAssetMenu(fileName = "New Battery", menuName = "InventorySO/BatterySO")]

public class BatterySO : InventoryItemData
{

    void Start()
    {
        type = TypeItem.Battery;

    }


}
