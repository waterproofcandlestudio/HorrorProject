using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXManager : MonoBehaviour
{
    /// Referencia para entrar a este script desde fuera
    public static PlayerSFXManager instance;
    // Asegura que la instancia del AudioManager esté linkeado a este script para poder acceder a el desde fuera
    void Awake() { instance = this; }

    // Regula si 1 sonido corta al anterior tras sonar...
    private static bool cutPlayerSFX = true;
    // Regula mediante el largo d la pista q el "gameObject" del sonido se destruya aunq no destruya al q sonó antes...
    private static float playerSFXLength;

    /// SFX List
    [Header("SFX")]
    public string ª;
    public AudioClip sfx_OpenDrawer, sfx_CloseDrawer, sfx_lockpickOpened, sfx_lockpickOpening, sfx_woodStep, sfx_dontPick, sfx_pickLockpick, sfx_saveInInventory, sfx_pickKey, sfx_pickBattery, sfx_pickNote, 
        sfx_switch, sfx_reloadLantern, sfx_switchLanternOn, sfx_openBag, sfx_heartBeat1, sfx_heartBeat2, sfx_locked, sfx_chains;

    /// Current SFX Object
    [Header("Current SFX")]
    public GameObject currentSFXObject;

    /// Sound Object
    [Header("Sound Reference")]
    public GameObject referenceSoundObject;

    public void PlaySFX(string sfxName)
    {
        switch (sfxName)
        {
            case "openBag": SoundObjectCreation(sfx_openBag); break;
            case "heartBeat1": SoundObjectCreation(sfx_heartBeat1); break;
            case "heartBeat2": SoundObjectCreation(sfx_heartBeat2); break;
            case "switchLanternOn": SoundObjectCreation(sfx_switchLanternOn); break;
            case "reloadLantern": SoundObjectCreation(sfx_reloadLantern); break;
            case "switch": SoundObjectCreation(sfx_switch); break;
            case "pickNote": SoundObjectCreation(sfx_pickNote); break;
            case "pickBattery": SoundObjectCreation(sfx_pickBattery); break;
            case "pickKey": SoundObjectCreation(sfx_pickKey); break;
            case "saveInInventory": SoundObjectCreation(sfx_saveInInventory); break;
            case "pickLockpick": SoundObjectCreation(sfx_pickLockpick); break;
            case "dontPick": SoundObjectCreation(sfx_dontPick); break;
            case "woodStep": SoundObjectCreation(sfx_woodStep); break;

            case "lockpickOpened": SoundObjectCreation(sfx_lockpickOpened); break;
            case "OpenDrawer": SoundObjectCreation(sfx_OpenDrawer); break;
            case "CloseDrawer": SoundObjectCreation(sfx_CloseDrawer); break;
            case "locked": SoundObjectCreation(sfx_locked); break;
            case "chains": SoundObjectCreation(sfx_chains); break;
            default: break;
        }
    }

    /// Object Creations
    void SoundObjectCreation(AudioClip clip)
    {
        playerSFXLength = clip.length;
        // Creo un SoundObject gameobject dentro d la escena
        currentSFXObject = Instantiate(referenceSoundObject, transform);
        // Le asigno un "audioClip" a su "AudioSource"
        currentSFXObject.GetComponent<AudioSource>().clip = clip;
        // Ejecuto el audio
        currentSFXObject.GetComponent<AudioSource>().Play();
        // Revisar si hay otro "gameObject" de otro sonido. Si lo hay lo elimina
        if (currentSFXObject == true) { Destroy(currentSFXObject, playerSFXLength); }
    }

    /// EFECTOS / MODIFICACIONES
    // Hacer efecto
    public void Loop()  { currentSFXObject.GetComponent<AudioSource>().loop = true; }

    /* El false desactiva el destroy de "SoundObjectCreation" para q no corte el sonido anterior...
     * 
     * El "Destroy(currentSFXObject, playerSFXpLength);" elimina el sonido tras finalizar su duración
     *  && no corta al siguiente (Básicamente ==> Corte automático con el tiempo mediante la duración del sonido...)
    */
    //public void NoCut() { cutPlayerSFX = false;     Destroy(currentSFXObject, playerSFXLength); } 

    // Deshacer efecto
    public void NoLoop(){ currentSFXObject.GetComponent<AudioSource>().loop = false; }

    public void Cut()   { cutPlayerSFX = false; }
    public void Stop() { Object.DestroyImmediate(currentSFXObject); }
    public void RePlay() { currentSFXObject.SetActive(true); }
}
