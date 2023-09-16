using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//LUCAS GARCÍA SCRIPT//
public class SafeDepositBox : ObjectInteractable
{
    public bool isOpened=false;
    [SerializeField]
    InventorySlots slots;
    [SerializeField]
    InteractionMode interactionMode;
    [SerializeField]
    public bool isInteracted=false;
    [SerializeField]
    Animator anim;

    public override void Interact()
    {
        base.Interact();
        if (slots.FindItem(TypeItem.LockPick) == true&&isOpened==false)
        {
            ActivateMiniGame();
            slots.DeleteItem(TypeItem.LockPick);
            isInteracted = true;
        }
        else if(isOpened == true)
        {
            PlayerSFXManager.instance.PlaySFX("locked");
 
        }
        else
        {
            PlayerSFXManager.instance.PlaySFX("locked");

        }

    }
    public void Awake()
    {
        slots = FindObjectOfType<InventorySlots>();
        interactionMode = GameObject.Find("Player").GetComponent<InteractionMode>();
        anim = this.transform.GetChild(1).GetComponent<Animator>();
    }
    public void ActivateMiniGame()
    {
        interactionMode.AcessMiniGameMode();
    }
    public void Open()
    {
        PlayerSFXManager.instance.PlaySFX("lockpickOpened");
        Debug.Log("Is Opened!!!!");
        anim.SetTrigger("OpenSafeDepositBox");
    }
    public void Close()
    {
        PlayerSFXManager.instance.PlaySFX("locked");
    }


}

