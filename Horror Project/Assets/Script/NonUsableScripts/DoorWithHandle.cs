using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//LUCAS GARCÍA SCRIPT//
public class DoorWithHandle : ObjectInteractable
{
    public int numDoorOpened;
    [SerializeField]
    Animator anim;
    public void Awake()
    {
    }
    public override void Interact()
    {
        base.Interact();
        Debug.Log("opened door");
        anim.SetTrigger("Open");

    }


}
