using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeItem
{
    Key,
    Battery,
    Note,
    LockPick,   //ganzúa para abrir cajas fuertes y puertas
}
public abstract class InventoryItemData : ScriptableObject
{
    public string nameItem;
    public int id;
    public string information;
    public TypeItem type;
    public Sprite icon;
    public GameObject itemMesh;
}
