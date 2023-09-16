using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int batteryDuration=60;
    int percentTime;
    float timePassed;
    public bool isWorking=true;
    [SerializeField]
    Light luz;
    public AudioSource lanternAudioSource;

    static bool check=true;
    void Awake()
    {
        luz = GetComponentInChildren<Light>();
        timePassed = batteryDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(isWorking==true)
            {
                luz.enabled = !luz.enabled;
                //PlayerSFXManager.instance.PlaySFX("switchLanternOn");
                lanternAudioSource.volume = UnityEngine.Random.Range(0.9f, 1f);
                lanternAudioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
                lanternAudioSource.Play();  //(Antigua manera d usarlo enlazando al audiosource dentro del "gameObject"...)
            }
        }
        if(luz.enabled==true && isWorking==true)
        {
            timePassed -= Time.deltaTime;

        }
        if(check==true)
        {
            if (timePassed < (batteryDuration-batteryDuration))
            {
                isWorking = false;
                StartCoroutine(DisconnectingLantern());
                check = false;
            }
        }
    }
    public string MakePercent()
    {
        percentTime = (Convert.ToInt32(timePassed) * 100) / batteryDuration;
        return percentTime.ToString()+"%";
    }
    public void ResetLantern()
    {
        timePassed = batteryDuration;
        percentTime = 0;
        isWorking = true;
        check = true;
        PlayerSFXManager.instance.PlaySFX("reloadLantern");
    }
    IEnumerator DisconnectingLantern()
    {
        for (int i = 0; i < 5; i++) //tiene que ser impar para que se desactive finalmente
        {
            luz.enabled = !luz.enabled;
            yield return new WaitForSeconds(0.3f);
        }

    }
}
