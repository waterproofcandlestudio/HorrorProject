using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : ObjectInteractable
{
    List<Light> lights;
    [SerializeField]
    GameObject switchKey;
    [SerializeField]
    Vector3 changePosition;  


    public override void Interact()
    {
        base.Interact();
        
        foreach (Light v in lights)
        {
            v.enabled =! v.enabled;
            PlayerSFXManager.instance.PlaySFX("switch");
        }
        switchKey.transform.Rotate(changePosition);//rota cada vez que se interactua -180 grados,
                                                   //lo que lo hace ir intercalando entre una posicion y otra


    }
    private void Awake()
    {
        changePosition = new Vector3(-180, 0, 0);
        lights = new List<Light>();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Light"))
        {
            lights.Add(g.GetComponent<Light>());
        }
        switchKey = this.transform.GetChild(0).GetChild(1).gameObject;
    }
    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

}
