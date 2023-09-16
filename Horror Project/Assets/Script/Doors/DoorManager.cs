using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//LUCAS GARCÍA SCRIPT//
public class DoorManager : MonoBehaviour
{
    public bool doorOpened=false;
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
    [SerializeField]
    HingeJoint hgJoint;
    Rigidbody body;
    [Header("Sound")]
    [SerializeField]
    private AudioSource closeAudio;



    void Start()
    {
        hgJoint = GetComponent<HingeJoint>();
        body=GetComponent<Rigidbody>();
    }
    private void Update()
    {

    }
    public  void ActionDoor()
    {        
        doorOpened = !doorOpened;
        if(doorOpened==true)
        {
            if (numberDoorOpened==0)
            {
                EnableZero();

            }
            if (numberDoorOpened == 1)
            {
                EnableOne();

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
        wedge.GetComponent<BoxCollider>().isTrigger = true;


    }
    public void EnableOne()
    {
        wedge2.GetComponent<BoxCollider>().isTrigger = true;


    }
    public void SetDoorOpened(int numDoor)
    {
        this.numberDoorOpened = numDoor;
        ActionDoor();
    }
    IEnumerator  CloseDoor()
    {
        timePassed = 0;
        initialPositionRotationDoor = WrapAngle(transform.localEulerAngles.y);
        Debug.Log(initialPositionRotationDoor);
        while (timePassed < timeToClose)
        {
            //Mathf.Lerp(initialPositionRotationDoor, 0, timePassed / timeToClose);
            transform.localEulerAngles= new Vector3(0, Mathf.Lerp(initialPositionRotationDoor, 0, timePassed / timeToClose),0);
            //Debug.Log(timePassed / timeToClose);
            timePassed += Time.deltaTime;
            Debug.Log(timePassed);
            yield return null;


        }
        closeAudio.Play();
        Destroy(hgJoint);
        Destroy(body);
        StopCoroutine(CloseDoor());
    }
    public void ChangeDoorCloseStatus()
    {
        isClosing = true;
        StartCoroutine(CloseDoor());
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
