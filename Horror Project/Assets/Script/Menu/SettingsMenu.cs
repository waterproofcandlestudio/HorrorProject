using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // Fullscreen
    public Toggle fullscreenToggle;

    // Resolución
    public Dropdown resolutionDropdown;

    Resolution[] resolutionsInfo;

    // Calidad Visual
    public Dropdown qualityDropdown;

    // Brillo
    public Slider brightnessSlider;
    protected float brightness;
    public Image brightnesspanel;

    // Sensibilidad (solo en el menú, tmb toco esto ingame en el movimiento d la cám ("FPS")
    public Slider sensivitySlider;
    protected float sensivity;

    void Start()
    {
        //fullscreenToggle.isOn = (PlayerPrefs.GetInt("fullscreen") == 1);        
        resolutionDropdown.value = PlayerPrefs.GetInt("resolution");
        qualityDropdown.value = PlayerPrefs.GetInt("quality");
        brightnessSlider.value = PlayerPrefs.GetFloat("brightness");
        //sensivitySlider.value = PlayerPrefs.GetFloat("sensivity");

        // Cojo las resoluciones que están guardadas en Unity mediante un método interno
        resolutionsInfo = Screen.resolutions;
        // Elimino las posibles opciones que haya en el "dropdown" pa q no etorpezcan
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        // Creo una lista d strings con el array d resoluciones, ya que no puedo añadirlas
        //  como array al "dropdown", sino como strings...
        // Entonces, esta lista d strings funcionará como las opciones al final...
        List<string> options = new List<string>();

        // El for hace un loop en las resoluciones del array "resolutionsInfo", 
        //  creando pa cada 1 d los elementos un string "volatileOption" para guardarlas y justo 
        //  en la linea siguiente, añadirla a la lista "options"
        for (int i = 0; i < resolutionsInfo.Length; i++)
        {
            string volatileOption = resolutionsInfo[i].width + " x " + resolutionsInfo[i].height + " p";
            options.Add(volatileOption);

            if (resolutionsInfo[i].width == Screen.currentResolution.width &&
                resolutionsInfo[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        // Añado la lista al "dropdown"
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutionsInfo[resolutionIndex]; 
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolution", resolutionIndex);
    }

    /// Control visual
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        //PlayerPrefs.SetInt("fullScreen", BoolToInt(isFullscreen));

        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("fullscreen", isFullscreen ? 1 : 0);
    }
    public void ChangeBrightness(float value)
    {
        brightness = value;
        PlayerPrefs.SetFloat("brightness", brightness);
        brightnesspanel.color = new Color(brightnesspanel.color.r, brightnesspanel.color.g, brightnesspanel.color.b, brightnessSlider.value);
    }

    public void ChangeSensivityMainMenu(float value)
    {
        sensivity = value;
        PlayerPrefs.SetFloat("sensivity", sensivity);
    }

    private int BoolToInt(bool val)
    {
        return val ? 1 : 0;
    }

    private bool IntToBool(int val)
    {
        return val == 1;
    }
}
