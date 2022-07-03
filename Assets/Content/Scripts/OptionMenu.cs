using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider ambientSlider;
    [SerializeField] private Slider soundsSlider;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle AudioToggle;
    [SerializeField] private Toggle FullscreenToggle;

    [SerializeField] private TMP_Text RoomsText;
    [SerializeField] private TMP_Text DurationText;
    [SerializeField] private TMP_Text CatText;


    void Start()
    {
        //Graphics/Audio settings
        if (PlayerPrefs.HasKey("MasterVolume"))
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        if (PlayerPrefs.HasKey("AmbientVolume"))
            ambientSlider.value = PlayerPrefs.GetFloat("AmbientVolume");    
        if (PlayerPrefs.HasKey("SoundsVolume"))
            soundsSlider.value = PlayerPrefs.GetFloat("SoundsVolume");
        if (PlayerPrefs.HasKey("Quality"))
            qualityDropdown.value = PlayerPrefs.GetInt("Quality");     
        if(PlayerPrefs.HasKey("SoundsToggle"))
            AudioToggle.isOn = PlayerPrefs.GetInt("SoundsToggle") == 0;
        if(PlayerPrefs.HasKey("Fullscreen"))
            FullscreenToggle.isOn = !(PlayerPrefs.GetInt("Fullscreen") == 1);

        //Difficult settings
        if (PlayerPrefs.HasKey("RoomsCount"))
            RoomsText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("RoomsCount");
        else
            RoomsText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = 4;
        
        if (PlayerPrefs.HasKey("Duration"))
            DurationText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("Duration");
        else
            DurationText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = 120;

        if (PlayerPrefs.HasKey("CatActivity"))
            CatText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("CatActivity");
        else
            CatText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = 0.05f;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetSoundsVolume(float volume)
    {
        audioMixer.SetFloat("Sounds", Mathf.Log10(volume) * 20);
        audioMixer.SetFloat("EventSounds", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SoundsVolume", volume);
    }

    public void SetAmbientVolume(float volume)
    {
        audioMixer.SetFloat("Ambient", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("AmbientVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }

    public void Sound()
    {    
        AudioListener.pause = !AudioListener.pause;
        PlayerPrefs.SetInt("SoundsToggle", AudioListener.pause ? 1 : 0);
    }

    public void FullScreenToggle()
    {
        Screen.fullScreen = FullscreenToggle.isOn;
        PlayerPrefs.SetInt("Fullscreen", Screen.fullScreen ? 1 : 0);
    }


    public void SetRoomsCount(float value)
    {
        PlayerPrefs.SetFloat("RoomsCount", value);
        RoomsText.text = value.ToString();
    }
    public void SetDuration(float value)
    {
        PlayerPrefs.SetFloat("Duration", value);
        DurationText.text = value.ToString();
    }
    public void SetCatActivity(float value)
    {
        PlayerPrefs.SetFloat("CatActivity", value);
        CatText.text = System.Math.Round((value * 100),3).ToString();
    }

    public void SetPreset(int value)
    {
        switch(value)
        {
            case 0:
                RoomsText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = 4;
                DurationText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = 130;
                CatText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = 0.02f;
                break;
            case 1:
                RoomsText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = 8;
                DurationText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = 210;
                CatText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = 0.04f;
                break;
            case 2:
                RoomsText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = 16;
                DurationText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = 500;
                CatText.transform.parent.gameObject.GetComponentInChildren<Slider>().value = 0.07f;
                break;
        }
    }

    public void GoToPlay()
    {
        SceneManager.LoadScene(1);
    }
}
