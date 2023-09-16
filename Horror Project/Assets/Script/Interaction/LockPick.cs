using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//LUCAS GARCÍA SCRIPT//
public class LockPick : ObjectInteractable
{

    [SerializeField]
    GameObject objectViewer;
    [SerializeField]
    LockPickSO lockPickSO;
    [SerializeField]
    InventorySlots slots;
    [SerializeField]
    Sprite imageSpriteLockPick;
    [SerializeField]
    Image img;
    InputPlayer playerInput;

    string spriteName;
    bool picked = false;
    private void Awake()
    {
        lockPickSO = Resources.Load<LockPickSO>(this.name + "SO");
        slots = FindObjectOfType<InventorySlots>();
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<InputPlayer>();
    }

    public override void Interact()
    {
        base.Interact();
        spriteName = this.gameObject.name + "Sprite";
        imageSpriteLockPick = Resources.Load<Sprite>(spriteName);

        lockPickSO.icon = imageSpriteLockPick;

        objectViewer = GameObject.FindGameObjectWithTag("ObjectViewer");
        img = objectViewer.GetComponent<Image>();


        EnterViewMode();

        PlayerSFXManager.instance.PlaySFX("pickLockpick");


    }

    // Update is called once per frame
    void Update()
    {
        if (picked == true)
        {
            if (Input.GetKeyDown(playerInput.keyDontGetElement))    //NO COGERLA
            {
                ExitViewMode();
                PlayerSFXManager.instance.PlaySFX("dontPick");
            }
            else if (Input.GetKeyDown(playerInput.keyGetElement))    //COGERLA Y AÑADIR EL SPRITE AL INVENTARIO
            {
                ExitViewMode();
                slots.CheckSpaceAvailable(lockPickSO);
                //falta añadir al inventario
                Destroy(this.gameObject);
                PlayerSFXManager.instance.PlaySFX("saveInInventory");
            }
        }

    }

    void EnterViewMode()
    {

        GameObject.FindGameObjectWithTag("Player").GetComponent<InteractionMode>().AcessInspectorMode(lockPickSO.itemMesh);


        picked = true;


    }
    void ExitViewMode()
    {

        GameObject.FindGameObjectWithTag("Player").GetComponent<InteractionMode>().ExitInspectorMode();

        picked = false;

    }


}
