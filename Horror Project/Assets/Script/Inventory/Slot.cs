using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;




public class Slot : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
{
    CanvasGroup canvasGroup;
    public bool isOcuppied=false;
    [SerializeField]
    public int numberSlot;
    public InventoryItemData item;
    [SerializeField]
    Image imageSlot;
    [SerializeField]
    Text textSlot;
    [SerializeField]
     RectTransform rectTransform;
    [SerializeField]
    RectTransform rectTransformOriginal;
    ManageLantern manageLantern;
    RawImage objectViewer;
    Animator anim;
    static bool checkerviewer=true;
    Text nameItemPreview;
    Text descriptionItemPreview;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();
        imageSlot = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>();
        textSlot = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        nameItemPreview = GameObject.FindGameObjectWithTag("ManagerDescription").transform.GetChild(1).GetComponent<Text>();
        descriptionItemPreview = GameObject.FindGameObjectWithTag("ManagerDescription").transform.GetChild(2).GetComponent<Text>();
    }

    private void Start()
    {
        manageLantern = GameObject.FindGameObjectWithTag("ManagerLantern").GetComponent<ManageLantern>();
        rectTransform = this.gameObject.transform.GetChild(0).GetComponent<RectTransform>();
        rectTransformOriginal = this.GetComponent<RectTransform>(); ;

    }
    public void Update()
    {
        

    }
    public void AddItem(InventoryItemData newItem)
    {
        isOcuppied = true;
        item = newItem;
        imageSlot.sprite = newItem.icon;
        textSlot.text = newItem.nameItem;

    }
    public void ShowPreview()
    {
        if (isOcuppied == true)
        {
            anim.SetBool("Active", true);
            Debug.Log("adsas");
            nameItemPreview.text = item.nameItem;
            descriptionItemPreview.text = item.information;

            //mostrar mensaje de la descripcion del objeto

        }
        else
        {
            Debug.Log("adsas");

            anim.SetBool("Active", true);
            nameItemPreview.text = "";
            descriptionItemPreview.text = "";

        }
    }
    public void DisablePreview()
    {
        if (isOcuppied==true)
        {
            anim.SetBool("Active", false);

            //mostrar mensaje de la descripcion del objeto
        }
        else
        {
            anim.SetBool("Active", false);

        }
    }


    


    

    public void OnDrag(PointerEventData eventData)
    {

        if (isOcuppied == true)
        {
            if(item.type==TypeItem.Battery)
            {
                rectTransform.anchoredPosition += eventData.delta / this.gameObject.GetComponent<RectTransform>().localScale;

            }
            Debug.Log(item.type);

        }
        else
        {

        }
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        canvasGroup.blocksRaycasts = true;
        rectTransform.position = rectTransformOriginal.position;
        if (manageLantern.batteryDropped==true)
        {
            RemoveItem();
            manageLantern.batteryDropped = false;
        }

    }
    public void RemoveItem()
    {
        imageSlot.sprite = null;
        textSlot.text = null;
        isOcuppied = false;
        item = null;
    }
    public void onClick()
    {
        if (isOcuppied == true && checkerviewer == true)
        {
            Debug.Log("hey");
            GameObject.FindGameObjectWithTag("Player").GetComponent<InteractionMode>().AcessPreviewItemInventaryMode(item.itemMesh);

            objectViewer = GameObject.FindGameObjectWithTag("ObjectViewer").GetComponent<RawImage>();
            objectViewer.enabled = true;


        }

    }
}
