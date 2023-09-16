using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldVoiceManager : MonoBehaviour
{
    /// Referencia para entrar a este script desde fuera
    public static WorldVoiceManager instance;
    // Asegura que la instancia del AudioManager esté linkeado a este script para poder acceder a el desde fuera
    void Awake() { instance = this; }

    // Regula si 1 sonido corta al anterior tras sonar...
    private static bool cutWorldVoice = true;
    // Regula mediante el largo d la pista q el "gameObject" del sonido se destruya aunq no destruya al q sonó antes...
    private static float worldVoiceLength;

    /// Voices List
    [Header("Voices")]
    public string ª;
    public AudioClip v_ghost;

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
            case "ghost": SoundObjectCreation(v_ghost); break;
            default: break;
        }
    }

    /// Object Creations
    void SoundObjectCreation(AudioClip clip)
    {
        worldVoiceLength = clip.length;
        // Creo un SoundObject gameobject dentro d la escena
        currentVoiceObject = Instantiate(referenceSoundObject, transform);
        // Le asigno un "audioClip" a su "AudioSource"
        currentVoiceObject.GetComponent<AudioSource>().clip = clip;
        // Ejecuto el audio
        currentVoiceObject.GetComponent<AudioSource>().Play();
        // Revisar si hay otro "gameObject" de otro sonido. Si lo hay lo elimina
        if (currentVoiceObject == true && cutWorldVoice == true) { Destroy(currentVoiceObject, worldVoiceLength); }
    }

    /// EFECTOS / MODIFICACIONES
    // Hacer efecto
    public void Loop() { currentVoiceObject.GetComponent<AudioSource>().loop = true; }

    /* El false desactiva el destroy de "SoundObjectCreation" para q no corte el sonido anterior...
     * 
     * El "Destroy(currentSFXObject, playerSFXpLength);" elimina el sonido tras finalizar su duración
     *  && no corta al siguiente (Básicamente ==> Corte automático con el tiempo mediante la duración del sonido...)
    */
    public void NoCut() { cutWorldVoice = false; Destroy(currentVoiceObject, worldVoiceLength); }

    // Deshacer efecto
    public void NoLoop() { currentVoiceObject.GetComponent<AudioSource>().loop = false; }

    public void Cut() { cutWorldVoice = false; }
}
