using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//LUCAS GARCÍA SCRIPT//
public class Key : ObjectInteractable
{

    [SerializeField]
    GameObject objectViewer;
    [SerializeField]
    KeySO keySO;
    [SerializeField]
    InventorySlots slots;
    [SerializeField]
    Sprite imageSpriteKey;
    [SerializeField]
    Image img;
    InputPlayer playerInput;


    string spriteName;
    bool picked = false;
    private void Awake()
    {
        keySO = Resources.Load<KeySO>(this.name + "SO");
        slots = FindObjectOfType<InventorySlots>();
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<InputPlayer>();

    }

    public override void Interact()
    {
        base.Interact();
        spriteName = this.gameObject.name + "Sprite";
        imageSpriteKey = Resources.Load<Sprite>(spriteName);
        keySO.icon = imageSpriteKey;

        objectViewer = GameObject.FindGameObjectWithTag("ObjectViewer");
        img = objectViewer.GetComponent<Image>();


        EnterViewMode();




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
                slots.CheckSpaceAvailable(keySO);
                //falta añadir al inventario
                Destroy(this.gameObject);
                PlayerSFXManager.instance.PlaySFX("saveInInventory");
            }
        }

    }

    void EnterViewMode()
    {
        picked = true;

        GameObject.FindGameObjectWithTag("Player").GetComponent<InteractionMode>().AcessInspectorMode(keySO.itemMesh);



        PlayerSFXManager.instance.PlaySFX("pickKey");
    }
    void ExitViewMode()
    {

        GameObject.FindGameObjectWithTag("Player").GetComponent<InteractionMode>().ExitInspectorMode();

        picked = false;

    }




}
