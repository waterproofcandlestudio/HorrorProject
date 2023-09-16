using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMusicManager : MonoBehaviour
{
    /// Referencia para entrar a este script desde fuera
    public static PlayerMusicManager instance;
    // Asegura que la instancia del AudioManager esté linkeado a este script para poder acceder a el desde fuera
    void Awake() { instance = this; }

    // Regula si 1 sonido corta al anterior tras sonar...
    private static bool cutPlayerMusic = true;
    // Regula mediante el largo d la pista q el "gameObject" del sonido se destruya aunq no destruya al q sonó antes...
    private static float playerMusicLength;

    /// Music List
    [Header("Music")]
    public string ª;
    public AudioClip music_menu, music_persecution,
        music_terrorPad, music_end;

    /// Current Music Object
    [Header("Current Music")]
    public GameObject currentMusicObject;

    /// Sound Object
    [Header("Sound Reference")]
    public GameObject referenceSoundObject;

    public void PlayMusic(string musicName)
    {
        switch (musicName)
        {
            case "menu": SoundObjectCreation(music_menu); break;
            case "persecution": SoundObjectCreation(music_persecution); break;
            case "terrorPad": SoundObjectCreation(music_terrorPad); break;
            case "end": SoundObjectCreation(music_end); break;
            default: break;
        }
    }

    /// Object Creations
    void SoundObjectCreation(AudioClip clip)
    {
        playerMusicLength = clip.length;
        // Creo un SoundObject gameobject dentro d la escena
        currentMusicObject = Instantiate(referenceSoundObject, transform);
        // Le asigno un "audioClip" a su "AudioSource"
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        // Ejecuto el audio
        currentMusicObject.GetComponent<AudioSource>().Play();
        // Revisar si hay otro "gameObject" de otro sonido. Si lo hay lo elimina
        if (currentMusicObject == true) { Destroy(currentMusicObject, playerMusicLength); }
    }

    /// EFECTOS / MODIFICACIONES
    // Hacer efecto
    public void Loop() { currentMusicObject.GetComponent<AudioSource>().loop = true; }

    /* El false desactiva el destroy de "SoundObjectCreation" para q no corte el sonido anterior... DA ERROR
     * 
     * El "Destroy(currentSFXObject, playerSFXpLength);" elimina el sonido tras finalizar su duración
     *  && no corta al siguiente (Básicamente ==> Corte automático con el tiempo mediante la duración del sonido...)
    */
    //public void NoCut() { cutPlayerMusic = false; Destroy(currentMusicObject, playerMusicLength); }

    // Deshacer efecto
    public void NoLoop() { currentMusicObject.GetComponent<AudioSource>().loop = false; }

    public void Cut() { cutPlayerMusic = false; }

    public void Stop() { currentMusicObject.SetActive(false); }
    public void RePlay() { currentMusicObject.SetActive(true); }
}
