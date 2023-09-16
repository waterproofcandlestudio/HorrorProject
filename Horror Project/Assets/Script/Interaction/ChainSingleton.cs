using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//LUCAS GARCÍA SCRIPT//
public class ChainSingleton : MonoBehaviour
{
    static ChainSingleton instance = null;
    static int numberChains=2;
    int padLocksRequiredToOpen = 3;
    [SerializeField]
    Animator door1;
    [SerializeField]
    Animator door2;
    GameManager manager;
    GameObject canvasFade;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        numberChains = 0;
    }
    public void DecreaseChains()
    {
        numberChains++;
        manager = FindObjectOfType<GameManager>();
        manager.isValidatedKey = true;

        if (numberChains == padLocksRequiredToOpen)
        {
            PlayerMusicManager.instance.PlayMusic("end");
            door1.SetTrigger("EnableDoor");
            door2.SetTrigger("EnableDoor");

            canvasFade=FindObjectOfType<GDTFadeEffect>().gameObject;
            StartCoroutine(ChargeCredits());       

        }
    }
    IEnumerator ChargeCredits()
    {
        yield return new WaitForSeconds(2f);
        canvasFade.GetComponent<GDTFadeEffect>().enabled = true;

        yield return new WaitForSeconds(5f);
        manager.IsOver();
    }
}
