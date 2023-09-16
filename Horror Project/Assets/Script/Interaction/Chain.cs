using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LUCAS GARCÍA SCRIPT//
public class Chain : ObjectInteractable
{
    [SerializeField]
    InventorySlots slots;
    [SerializeField]
    MeshRenderer meshPadlock;
    ChainSingleton singletonChain;
    public override void Interact()
    {
        base.Interact();
        slots = FindObjectOfType<InventorySlots>();
        if (slots.FindItem(TypeItem.Key) == true)
        {
            PlayerSFXManager.instance.PlaySFX("chains");
            slots.DeleteItem(TypeItem.Key);
            StartCoroutine(Fade());
            singletonChain.DecreaseChains();
        }
        else
        {
            PlayerSFXManager.instance.PlaySFX("locked");
        }

    }
    public void Awake()
    {
    }
    public void Start()
    {
        singletonChain=FindObjectOfType<ChainSingleton>();
    }
    IEnumerator Fade()
    {
        for (float i = 1f; i >= -0.05f; i -= 0.05f)
        {
            Color color = meshPadlock.material.color;
            color.a = i;
            Debug.Log(color.a);

            meshPadlock.material.color = color;
            
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(this.gameObject);
    }
}
