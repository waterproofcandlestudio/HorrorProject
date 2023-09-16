using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//LUCAS GARCÍA SCRIPT//
public class ManageLantern : MonoBehaviour
{
    Lantern lantern;
    [SerializeField]
    Text percentLantern;
    string lanternTag="Lantern";
    Sprite imageLanternFullBattery;
    Sprite imageLanternEmptyBattery;
    Image batteryImage;
    public bool batteryDropped=false;

    void Start()
    {
        batteryImage = this.gameObject.transform.GetChild(3).GetComponent<Image>();
        imageLanternEmptyBattery = Resources.Load<Sprite>("NoBatterySprite");
        imageLanternFullBattery = Resources.Load<Sprite>("BatteryBaseSprite");

        lantern = GameObject.FindGameObjectWithTag(lanternTag).GetComponent<Lantern>();
        percentLantern=this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Text>();
        batteryImage.sprite = imageLanternFullBattery;

    }
    private void Update()
    {
        percentLantern.text = lantern.MakePercent();
        if(lantern.isWorking==false)
        {
            batteryImage.sprite = imageLanternEmptyBattery;

        }
    }


    public void OnDropBattery()
    {

        batteryImage.sprite = imageLanternFullBattery;
        lantern.isWorking = true;
        lantern.ResetLantern();
        batteryDropped = true;
        

    }
}
