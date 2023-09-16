using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
public class Note : ObjectInteractable
{
    [SerializeField]
    GameObject objectViewer;
    [SerializeField]
    NoteSO noteSO;
    [SerializeField]
    InventorySlots slots;
    [SerializeField]
    Image img;
    [SerializeField]
    Sprite imageSpriteNote;
    [SerializeField]
    InputPlayer playerInput;


    string spriteName;
    bool picked=false;
    private void Awake()
    {
        noteSO = Resources.Load<NoteSO>(this.name+"SO");
        slots = FindObjectOfType<InventorySlots>();
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<InputPlayer>();
    }

    public override void Interact()
    {
        base.Interact();
        spriteName = this.gameObject.name + "Sprite";
        imageSpriteNote = Resources.Load<Sprite>(spriteName);
        noteSO.icon = imageSpriteNote;

        objectViewer = GameObject.FindGameObjectWithTag("ObjectViewer");
        img = objectViewer.GetComponent<Image>();


        EnterViewMode();

        PlayerSFXManager.instance.PlaySFX("pickNote");


    }

    // Update is called once per frame
    void Update()
    {
        if(picked==true)
        {
            if (Input.GetKeyDown(playerInput.keyDontGetElement))    //NO COGERLA
            {
                ExitViewMode();
                PlayerSFXManager.instance.PlaySFX("dontPick");
            }
            else if(Input.GetKeyDown(playerInput.keyGetElement))    //COGERLA Y AÑADIR EL SPRITE AL INVENTARIO
            {
                ExitViewMode();
                slots.CheckSpaceAvailable(noteSO);
                Destroy(this.gameObject);
                PlayerSFXManager.instance.PlaySFX("saveInInventory");
            }
        }

    }

    void EnterViewMode()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("Player").GetComponent<InteractionMode>().AcessInspectorMode(noteSO.itemMesh);


        picked = true;


    }
    void ExitViewMode()
    {

        GameObject.FindGameObjectWithTag("Player").GetComponent<InteractionMode>().ExitInspectorMode();

        picked = false;

    }
}
