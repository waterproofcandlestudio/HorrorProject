using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField]
    float speedRotation;
    [SerializeField]
    GameObject pointRotation;
    [SerializeField]
    bool isActivated;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated==true)
        {
            transform.RotateAround(pointRotation.transform.position, Vector3.forward, speedRotation * Time.deltaTime);
        }
    }
    public void ActivateRotation()
    {
        isActivated = true;
    }
}
