using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//LUCAS GARCÍA SCRIPT//
public class EnableAndDisableLights : MonoBehaviour
{
    [SerializeField]
	Light light;
    float time;
    bool check=true;
    private void Start()
    {
        time = 0;
    }
    private void Update()
    {
        time+=Time.deltaTime;
        if (time > 12f)
        {
            if (check == true)
            {
                StartCoroutine(EnablingAndDisablingLight());
                check = false;

            }

        }
    }
    IEnumerator EnablingAndDisablingLight()
    {


        for (int i = 0; i < 6; i++)
        {
            light.enabled =! light.enabled;
            yield return new WaitForSeconds(0.2f);

        }

        check = true;
        time = 0;
    }


}
