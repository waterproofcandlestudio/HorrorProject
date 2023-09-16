using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//LUCAS GARCÍA SCRIPT//
public class Battery : ObjectInteractable
{

    [SerializeField]
    GameObject objectViewer;
    [SerializeField]
    BatterySO batterySO;
    [SerializeField]
    InventorySlots slots;
    [SerializeField]
    Image img;
    [SerializeField]
    Sprite imageSpriteBattery;
    [SerializeField]
    InputPlayer playerInput;


    string spriteName;
    bool picked = false;
    private void Awake()
    {
        batterySO = Resources.Load<BatterySO>(this.name + "SO");
        slots = FindObjectOfType<InventorySlots>();
        playerInput=GameObject.FindGameObjectWithTag("Player").GetComponent<InputPlayer>();
        Debug.Log("sdad");

    }

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Entered");

        spriteName = this.gameObject.name + "Sprite";
        imageSpriteBattery=Resources.Load<Sprite>(spriteName);
        batterySO.icon = imageSpriteBattery;

        objectViewer = GameObject.FindGameObjectWithTag("ObjectViewer");
        img = objectViewer.GetComponent<Image>();


        EnterViewMode();


        PlayerSFXManager.instance.PlaySFX("pickBattery");

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
                slots.CheckSpaceAvailable(batterySO);
                //falta añadir al inventario
                Destroy(this.gameObject);
                PlayerSFXManager.instance.PlaySFX("saveInInventory");
            }
        }

    }

    void EnterViewMode()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("Player").GetComponent<InteractionMode>().AcessInspectorMode(batterySO.itemMesh);


        picked = true;


    }
    void ExitViewMode()
    {

        GameObject.FindGameObjectWithTag("Player").GetComponent<InteractionMode>().ExitInspectorMode();

        picked = false;

    }


}
