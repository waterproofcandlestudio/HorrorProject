using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//LUCAS GARCÍA SCRIPT//
public class EnterTriggerByBall : MonoBehaviour
{
    public bool isIn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isIn = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isIn = false;
    }


}
