using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//LUCAS GARCÍA SCRIPT//
public class DoorEntrance : MonoBehaviour
{
    [SerializeField]
    DoorManager doorManager;
    [SerializeField]
    int numWedge;
    private void Awake()
    {
        //doorManager = GetComponent<DoorManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        doorManager.SetDoorOpened(numWedge);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            doorManager.doorOpened = false;
    }


}
