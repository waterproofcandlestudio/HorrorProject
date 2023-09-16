using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//LUCAS GARCÍA SCRIPT//
public class Padlock : ObjectInteractable
{
    int numberPadLock = 1;  //numero de llave requerida para abrir el candado
    [SerializeField]
    InventorySlots slots;
    [SerializeField]
    MeshRenderer meshPadlock;

    public override void Interact()
    {
        base.Interact();
        if (slots.FindItem(TypeItem.Key, numberPadLock) == true)
        {
            StartCoroutine(Fade());
        }
        else
        {
            Debug.Log("Key not working!");
        }


    }
    public void Awake()
    {
        slots = FindObjectOfType<InventorySlots>();
        //meshPadlock.AddRange(GetComponentsInChildren<MeshRenderer>());
    }
    IEnumerator Fade()
    {
        for (float i = 1f; i >= -0.05f; i -= 0.05f)
        {
            Color color = meshPadlock.material.color;
            color.a = i;
            Debug.Log(color.a);
            //foreach (MeshRenderer item in meshPadlock)
            //{
            meshPadlock.material.color = color;
            //}
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(this.gameObject);
    }


}
