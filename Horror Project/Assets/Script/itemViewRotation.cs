using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//LUCAS GARCÍA SCRIPT//
public class itemViewRotation : MonoBehaviour,IDragHandler
{
    InteractionMode interactionMode;
    InputPlayer inputPlayer;
    void Start()
    {
        interactionMode = FindObjectOfType<InteractionMode>().
            GetComponent<InteractionMode>();
        inputPlayer = FindObjectOfType<InputPlayer>().
        GetComponent<InputPlayer>();

    }
    public void OnDrag(PointerEventData eventData)
    {
        interactionMode.lastItemPrefab.transform.eulerAngles
        += new Vector3(-eventData.delta.y*inputPlayer.sensibilityObjectViewerDrag, -eventData.delta.x*inputPlayer.sensibilityObjectViewerDrag);
        //gira el objeto segun el input del mouse
        Debug.Log(interactionMode.lastItemPrefab.transform.eulerAngles);
    }


}
