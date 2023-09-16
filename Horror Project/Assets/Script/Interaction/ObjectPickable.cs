using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//LUCAS GARCÍA SCRIPT//
public class ObjectPickable : ObjectInteractable
{

    private Camera camera;
    bool isPicked = false;
    // Clase que hereda de ObjectInteractable y que permite no solo a los objetos ser interactuables,
    // sino que cogerse y verse en el visor
    public override void Interact()
    {
        base.Interact();
        isPicked = true;
        PlayerSFXManager.instance.PlaySFX("dontPick");
    }
    void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {

        if (isPicked == true)
        {
            this.GetComponent<Rigidbody>().useGravity = false;
            transform.position = Vector3.Lerp(transform.position, camera.transform.position + camera.transform.forward, Time.deltaTime * 8);
            //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPicked = false;
            this.GetComponent<Rigidbody>().useGravity = true;


        }



    }


}
