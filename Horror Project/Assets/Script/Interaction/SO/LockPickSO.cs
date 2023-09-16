using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LUCAS GARCÍA SCRIPT//
[CreateAssetMenu(fileName = "New LockPick", menuName = "InventorySO/LockPickSO")]
public class LockPickSO : InventoryItemData
{
    void Start()
    {
        type = TypeItem.LockPick;
    }
   


}
