using System.Collections;
using System.Collections.Generic;
//using System.Windows.Controls;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    //AudioSource audioData;

    public AudioMixer audioMixer;

    // Valores de los sliders del menú opciones
    public Slider GeneralVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider EffectsVolumeSlider;
    public Slider UIVolumeSlider;
    public Slider VoiceVolumeSlider;

    void Start()
    {
        // Paso los valores de las opciones al "PlayerPrefs" para q no se pierdan con el cambio de escena
        GeneralVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        EffectsVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        UIVolumeSlider.value = PlayerPrefs.GetFloat("uiVolume");
        VoiceVolumeSlider.value = PlayerPrefs.GetFloat("voiceVolume");

        //audioData = GetComponent<AudioSource>();
    }

    void Update()
    {
        //// Si se pausa el juego... / se va al menú...
        //if(PauseMenu.gameIsPaused == true)
        //{
        //    audioData.Pause();
        //}
        //if (PauseMenu.gameIsPaused == false)
        //{
        //    audioData.UnPause();
        //}

        ///*
        //// Durante el juego
        //if (scene == "Menu")
        //{
        //    menuMusicSource.play();
        //}
        //*/
    }


    /// Menú ==> Control del sonido
    public void SetVolume(float volume)
    {
        // "MasterVolume" == Referencia al grupo dentro del mixer
        // "volume" == variable q le asigna el valor a "MasterVolume"
        audioMixer.SetFloat("masterVolume", volume);
        // Guardo el valor en las preferencias del jugador entre escenas
        PlayerPrefs.SetFloat("masterVolume", volume);
    }
    public void SetMusic(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void SetSFX(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    public void SetUI(float volume)
    {
        audioMixer.SetFloat("uiVolume", volume);
        PlayerPrefs.SetFloat("uiVolume", volume);
    }
    public void SetVoice(float volume)
    {
        audioMixer.SetFloat("voiceVolume", volume);
        PlayerPrefs.SetFloat("voiceVolume", volume);
    }

    public void SoundReset()
    {
        PlayerPrefs.DeleteKey("masterVolume");
        audioMixer.FindMatchingGroups("masterVolume");
    }
}
