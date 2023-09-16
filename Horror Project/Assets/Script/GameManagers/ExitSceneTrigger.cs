using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//LUCAS GARCÍA SCRIPT//
public class ExitSceneTrigger : MonoBehaviour
{
    GameManager gameManager;
    GameObject canvasFade;
    private void Awake()
    {
       gameManager=GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
       canvasFade = GameObject.Find("FadeFinalLoop");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            StartCoroutine(ChargeNewLevel());
        }
    }
    IEnumerator ChargeNewLevel()
    {
        canvasFade.GetComponent<GDTFadeEffect>().enabled = true;

        yield return new WaitForSeconds(1f);
        gameManager.ChangeLevel();

    }


}
