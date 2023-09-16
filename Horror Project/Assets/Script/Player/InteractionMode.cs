using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//LUCAS GARCÍA SCRIPT//
public class InteractionMode : MonoBehaviour
{
    [SerializeField]
    RawImage objectViewer;
    [SerializeField]
    public GameObject lastItemPrefab;
    Texture2D cursorInspector;
    GameObject panelViewObject;
    GameObject canvasInventario;
    [SerializeField]
    GameObject canvasUnlockKey;
    [SerializeField]
    GameObject ChildPanelInventario;
    [SerializeField]
    GameObject ChildPanelLantern;
    [SerializeField]
    GameObject ChildPanelInventoryDescription;
    public bool itemInventaryActivated=false;
    public bool isInInspector = false;
    public bool isInPreviewItemInventary = false;
    public bool isInMiniGame = false;
    public bool isClickedInMiniGame = false;






    private void Start()
    {
        Cursor.SetCursor(cursorInspector, Vector2.zero, CursorMode.ForceSoftware);
        objectViewer = GameObject.FindGameObjectWithTag("ObjectViewer").GetComponent<RawImage>();
        canvasInventario = GameObject.Find("CanvasInventory");    //busca el objeto con el script inventario
        ChildPanelInventario = canvasInventario.transform.GetChild(0).gameObject;   //encuentra al hijo que tiene todo lo visual de los slots
        ChildPanelLantern = canvasInventario.transform.GetChild(1).gameObject;   //encuentra al hijo que tiene todo lo visual de la linterna
        ChildPanelInventoryDescription= canvasInventario.transform.GetChild(2).gameObject;
        ChildPanelInventario.SetActive(false);    //oculta la UI del inventario desde el inicio
        ChildPanelLantern.SetActive(false);    //oculta la UI de la linterna desde el inicio
        ChildPanelInventoryDescription.SetActive(false);
        panelViewObject = GameObject.FindGameObjectWithTag("PanelBackGround");
        canvasUnlockKey= GameObject.Find("CanvasUnlockKey");
        canvasUnlockKey.SetActive(false);

    }
    public void AcessInspectorMode(GameObject gameObjectItem)
    {
        Cursor.SetCursor(cursorInspector, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        objectViewer.enabled = true;
        lastItemPrefab = Instantiate(gameObjectItem, new Vector3(1000, 1000, 1000), Quaternion.identity);
        panelViewObject.GetComponent<Image>().enabled = true;
        this.gameObject.GetComponent<FPSCamera>().enabled = false;
        this.gameObject.GetComponent<PlayerController>().enabled = false;
        isInInspector = true;

    }
    public void ExitInspectorMode()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        objectViewer.enabled = false;
        Destroy(lastItemPrefab.gameObject);
        panelViewObject.GetComponent<Image>().enabled = false;
        this.gameObject.GetComponent<FPSCamera>().enabled = true;
        this.gameObject.GetComponent<PlayerController>().enabled = true;
        isInInspector = false;

    }
    public void AcessInventaryMode()
    {
        Cursor.SetCursor(cursorInspector,Vector2.zero,CursorMode.ForceSoftware);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        ChildPanelInventario.active = true;
        ChildPanelLantern.active = true;
        ChildPanelInventoryDescription.active = true;


        this.gameObject.GetComponent<FPSCamera>().enabled = false;
        this.gameObject.GetComponent<PlayerController>().enabled = false;
    }
    public void AcessPreviewItemInventaryMode(GameObject gameObjectItem)
    {
        panelViewObject.GetComponent<Image>().enabled = true;
        AcessInspectorMode(gameObjectItem);

        ChildPanelInventario.active = false;
        ChildPanelLantern.active = false;
        ChildPanelInventoryDescription.active = false;

        itemInventaryActivated = true;
        isInInspector = false;

    }
    public void ExitPreviewItemInventaryMode()
    {

        panelViewObject.GetComponent<Image>().enabled = false;
        objectViewer.enabled = false;
        Destroy(lastItemPrefab.gameObject);

        Cursor.SetCursor(cursorInspector, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        ChildPanelInventario.active = true;
        ChildPanelLantern.active = true;
        ChildPanelInventoryDescription.active = true;

        itemInventaryActivated = false;

    }
    public void ExitInventaryMode()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ChildPanelInventario.active = false;
        ChildPanelLantern.active = false;
        ChildPanelInventoryDescription.active = false;


        this.gameObject.GetComponent<FPSCamera>().enabled = true;
        this.gameObject.GetComponent<PlayerController>().enabled = true;
    }
    public void AcessMiniGameMode()
    {
        isInMiniGame = true;
        canvasUnlockKey.active = true;
        canvasUnlockKey.GetComponentInChildren<RotateAround>().enabled = true;
        canvasUnlockKey.GetComponentInChildren<RotateAround>().ActivateRotation();
        this.gameObject.GetComponent<FPSCamera>().enabled = false;
        this.gameObject.GetComponent<PlayerController>().enabled = false;

        PlayerSFXManager.instance.PlaySFX("lockpickOpening"); 
    }
    public void ExitMiniGameMode()
    {
        isInMiniGame = false;
        canvasUnlockKey.active = false;
        canvasUnlockKey.GetComponentInChildren<RotateAround>().enabled = false;
        canvasUnlockKey.GetComponentInChildren<RotateAround>().ActivateRotation();
        this.gameObject.GetComponent<FPSCamera>().enabled = true;
        this.gameObject.GetComponent<PlayerController>().enabled = true;
        PlayerSFXManager.instance.PlaySFX("lockpickOpened");
    }

}
