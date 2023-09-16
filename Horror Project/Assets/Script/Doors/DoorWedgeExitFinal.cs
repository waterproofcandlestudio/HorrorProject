using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//LUCAS GARCÍA SCRIPT//
public class DoorWedgeExitFinal : MonoBehaviour
{
    [SerializeField]
    DoorManagerEnterStairs doorManager;
    [SerializeField]
    int numWedge;
    private void Awake()
    {
        //doorManager = GetComponent<DoorManager>();
    }
    
     void OnTriggerExit(Collider other)
    {
        Debug.Log("sdadassd");

        if (other.tag=="Player")
        {
            doorManager.ChangeDoorCloseStatus();

        }
    }

}
