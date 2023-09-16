using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//LUCAS GARCÍA SCRIPT//
public class DoorManagerEnterStairs : MonoBehaviour     //hace lo mismo que el door manager principal, pero habilitando las puerta para entrar y salir libremente
{

    public bool doorOpened = false;
    [SerializeField]
    GameObject wedge;   //cuña para la puerta
    [SerializeField]
    GameObject wedge2;   //cuña para la puerta 2
    [SerializeField]
    public int numberDoorOpened;
    [SerializeField]
    public bool isClosing = false;
    [SerializeField]
    public float timeToClose;
    [SerializeField]
    float amount;
    float initialPositionRotationDoor;
    float timePassed;




    void Start()
    {

    }
    private void Update()
    {

    }
    public void ActionDoor()
    {
        doorOpened = !doorOpened;
        if (doorOpened == true)
        {
            if (numberDoorOpened == 0)
            {
                EnableZero();

            }

        }
        else
        {


        }



    }
    public void EnableZero()
    {
        //wedge.SetActive(true);
        //wedge2.SetActive(false);
        wedge2.GetComponent<BoxCollider>().isTrigger = true;


    }

    public void SetDoorOpened(int numDoor)
    {
        this.numberDoorOpened = numDoor;
        ActionDoor();
    }

    public void ChangeDoorCloseStatus()
    {
        isClosing = true;
        //wedge2.GetComponent<BoxCollider>().isTrigger = false;

    }
    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }



}
