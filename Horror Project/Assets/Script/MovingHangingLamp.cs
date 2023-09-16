using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//LUCAS GARCÍA SCRIPT//
public class MovingHangingLamp : MonoBehaviour
{
    [SerializeField]
    Rigidbody rg;
    bool check=true;
    private void FixedUpdate()
    {
        if (check==true)
        {
            rg.AddForce(rg.transform.forward*30);   //añade una fuerza en el eje x para que empiece
                                                    //el movimiento de la lámpara
            check = false;
        }
    }


}
