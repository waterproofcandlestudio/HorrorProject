using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    /// Referencia para entrar a este script desde fuera
    public static PlayerUIManager instance;
    // Asegura que la instancia del AudioManager est� linkeado a este script para poder acceder a el desde fuera
    void Awake() { instance = this; }

    // Regula si 1 sonido corta al anterior tras sonar...
    private static bool cutPlayerUI = true;
    // Regula mediante el largo d la pista q el "gameObject" del sonido se destruya aunq no destruya al q son� antes...
    private static float playerUILength;

    /// UI List
    [Header("UI")]
    public string �;
    public AudioClip ui_enterMenu, ui_button1, ui_button2;

    /// Current UI Object
    [Header("Current UI")]
    public GameObject currentUIObject;

    /// Sound Object
    [Header("Sound Reference")]
    public GameObject referenceSoundObject;

    public void PlayUI(string uiName)
    {
        switch (uiName)
        {
            case "enterMenu": SoundObjectCreation(ui_enterMenu); break;
            case "button1": SoundObjectCreation(ui_button1); break;
            case "button2": SoundObjectCreation(ui_button2); break;
            default: break;
        }
    }

    public void PlayButton1()
    {
        SoundObjectCreation(ui_button1);
    }
    public void PlayButton2()
    {
        SoundObjectCreation(ui_button2);
    }

    /// Object Creations
    void SoundObjectCreation(AudioClip clip)
    {
        playerUILength = clip.length;
        // Creo un SoundObject gameobject dentro d la escena
        currentUIObject = Instantiate(referenceSoundObject, transform);
        // Le asigno un "audioClip" a su "AudioSource"
        currentUIObject.GetComponent<AudioSource>().clip = clip;
        // Ejecuto el audio
        currentUIObject.GetComponent<AudioSource>().Play();
        // Revisar si hay otro "gameObject" de otro sonido. Si lo hay lo elimina
        if (currentUIObject == true && cutPlayerUI == true) { Destroy(currentUIObject, playerUILength); }
    }

    /// EFECTOS / MODIFICACIONES
    // Hacer efecto
    public void Loop() { currentUIObject.GetComponent<AudioSource>().loop = true; }

    /* El false desactiva el destroy de "SoundObjectCreation" para q no corte el sonido anterior...
     * 
     * El "Destroy(currentSFXObject, playerSFXpLength);" elimina el sonido tras finalizar su duraci�n
     *  && no corta al siguiente (B�sicamente ==> Corte autom�tico con el tiempo mediante la duraci�n del sonido...)
    */
    public void NoCut() { cutPlayerUI = false; Destroy(currentUIObject, playerUILength); }

    // Deshacer efecto
    public void NoLoop() { currentUIObject.GetComponent<AudioSource>().loop = false; }

    public void Cut() { cutPlayerUI = false; }
}
