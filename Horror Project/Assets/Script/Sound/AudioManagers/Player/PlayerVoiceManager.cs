using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVoiceManager : MonoBehaviour
{
    /// Referencia para entrar a este script desde fuera
    public static PlayerVoiceManager instance;
    // Asegura que la instancia del AudioManager esté linkeado a este script para poder acceder a el desde fuera
    void Awake() { instance = this; }

    // Regula si 1 sonido corta al anterior tras sonar...
    private static bool cutPlayerVoice = true;
    // Regula mediante el largo d la pista q el "gameObject" del sonido se destruya aunq no destruya al q sonó antes...
    private static float playerVoiceLength;

    /// Voice List
    [Header("Voice")]
    public string ª;
    public AudioClip v_ghostOnBack;

    /// Current Voice Object
    [Header("Current Voice")]
    public GameObject currentVoiceObject;

    /// Sound Object
    [Header("Sound Reference")]
    public GameObject referenceSoundObject;

    public void PlayVoice(string voiceName)
    {
        switch (voiceName)
        {
            case "ghostOnBack": SoundObjectCreation(v_ghostOnBack); break;
            default: break;
        }
    }

    public void PlayVoiceGhost()
    {
        SoundObjectCreation(v_ghostOnBack);
    }

    /// Object Creations
    void SoundObjectCreation(AudioClip clip)
    {
        playerVoiceLength = clip.length;
        // Creo un SoundObject gameobject dentro d la escena
        currentVoiceObject = Instantiate(referenceSoundObject, transform);
        // Le asigno un "audioClip" a su "AudioSource"
        currentVoiceObject.GetComponent<AudioSource>().clip = clip;
        // Ejecuto el audio
        currentVoiceObject.GetComponent<AudioSource>().Play();
        // Revisar si hay otro "gameObject" de otro sonido. Si lo hay lo elimina
        if (currentVoiceObject == true && cutPlayerVoice == true) { Destroy(currentVoiceObject, playerVoiceLength); }
    }

    /// EFECTOS / MODIFICACIONES
    // Hacer efecto
    public void Loop() { currentVoiceObject.GetComponent<AudioSource>().loop = true; }

    /* El false desactiva el destroy de "SoundObjectCreation" para q no corte el sonido anterior...
     * 
     * El "Destroy(currentSFXObject, playerSFXpLength);" elimina el sonido tras finalizar su duración
     *  && no corta al siguiente (Básicamente ==> Corte automático con el tiempo mediante la duración del sonido...)
    */
    public void NoCut() { cutPlayerVoice = false; Destroy(currentVoiceObject, playerVoiceLength); }

    // Deshacer efecto
    public void NoLoop() { currentVoiceObject.GetComponent<AudioSource>().loop = false; }

    public void Cut() { cutPlayerVoice = false; }
}
