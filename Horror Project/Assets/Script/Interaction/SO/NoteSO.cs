using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Note",menuName ="InventorySO/NoteSO")]
public class NoteSO : InventoryItemData
{
    public void Awake()
    {
        type = TypeItem.Note;
    }
}
