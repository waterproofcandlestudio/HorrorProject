using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : ObjectInteractable
{
    [SerializeField]
    GameObject drawer1;
    Animator animDrawer;
    bool checker=true;

    public override void Interact()
    {
        base.Interact();
        //PlayerSFXManager.instance.PlaySFX("switch"); equivalente 
        if(checker==true)
        {
            animDrawer.SetBool("EnableOpen",true);
            animDrawer.SetBool("EnableClose", false);

            //animDrawer.SetBool("EnableOpen", false);

            checker = false;
        }
        else
        {
            animDrawer.SetBool("EnableClose", true);
            animDrawer.SetBool("EnableOpen", false);

            //animDrawer.SetBool("EnableClose", false);
            checker = true;

        }
    }
    private void Awake()
    {
        drawer1 = this.transform.GetChild(0).gameObject;
        animDrawer = GetComponent<Animator>();



    }
    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }
}
