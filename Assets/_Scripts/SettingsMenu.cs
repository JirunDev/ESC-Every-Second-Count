using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider MasterVolumeSLD, MusicVolumeSLD, SFXVolumeSLD, mouseSensSLD;
    [Space()]
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown resolutionDropdown;
    [Space()]
    public Toggle fullScreenToggle, vSyncToggle;

    private const string resolutionWidthPlayerPrefKey = "ResolutionWidth";
    private const string resolutionHeightPlayerPrefKey = "ResolutionHeight";
    private const string resolutionRefreshRatePlayerPrefKey = "RefreshRate";
    private const string fullScreenPlayerPrefKey = "FullScreen";
    Resolution[] resolutions;
    Resolution selectedResolution;

    void Start()
    {
        //RecoverSavedValue
        MasterVolumeSLD.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        MusicVolumeSLD.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SFXVolumeSLD.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        qualityDropdown.value = PlayerPrefs.GetInt("VisualQuality", 1);
        Screen.SetResolution(PlayerPrefs.GetInt("Rwidth"), PlayerPrefs.GetInt("Rheight"), Screen.fullScreen);
        mouseSensSLD.value = PlayerPrefs.GetFloat("MSensitivity", 0f);

        //FullScreenCheck
        fullScreenToggle.isOn = Screen.fullScreen;

        //VsyncCheck
        if (QualitySettings.vSyncCount == 0)
        {
            vSyncToggle.isOn = false;
        }
        else
        {
            vSyncToggle.isOn = true;
        }

        //Resolutions
        resolutions = Screen.resolutions;
        LoadSettings();
        CreateResolutionDropdown();
        fullScreenToggle.onValueChanged.AddListener(SetFullScreen);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    public void SetMasterVolume(float decimalVolume)
    {
        var dbVolume1 = Mathf.Log10(decimalVolume) * 20;
        if (decimalVolume == 0.0f)
        {
            dbVolume1 = -80.0f;
        }
        audioMixer.SetFloat("MasterVolume", dbVolume1);

        PlayerPrefs.SetFloat("MasterVolume", decimalVolume);
    }

    public void SetMusicVolume(float decimalVolume)
    {
        var dbVolume2 = Mathf.Log10(decimalVolume) * 20;
        if (decimalVolume == 0.0f)
        {
            dbVolume2 = -80.0f;
        }
        audioMixer.SetFloat("MusicVolume", dbVolume2);

        PlayerPrefs.SetFloat("MusicVolume", decimalVolume);
    }

    public void SetSFXVolume(float decimalVolume)
    {
        var dbVolume3 = Mathf.Log10(decimalVolume) * 20;
        if (decimalVolume == 0.0f)
        {
            dbVolume3 = -80.0f;
        }
        audioMixer.SetFloat("SFXVolume", dbVolume3);

        PlayerPrefs.SetFloat("SFXVolume", decimalVolume);
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, false);

        PlayerPrefs.SetInt("VisualQuality", index);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt(fullScreenPlayerPrefKey, isFullscreen ? 1 : 0);
    }

    public void SetVsync()
    {
        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    private void LoadSettings()
    {
        selectedResolution = new Resolution();
        selectedResolution.width = PlayerPrefs.GetInt(resolutionWidthPlayerPrefKey, Screen.currentResolution.width);
        selectedResolution.height = PlayerPrefs.GetInt(resolutionHeightPlayerPrefKey, Screen.currentResolution.height);
        selectedResolution.refreshRate = PlayerPrefs.GetInt(resolutionRefreshRatePlayerPrefKey, Screen.currentResolution.refreshRate);

        fullScreenToggle.isOn = PlayerPrefs.GetInt(fullScreenPlayerPrefKey, Screen.fullScreen ? 1 : 0) > 0;

        Screen.SetResolution(
            selectedResolution.width,
            selectedResolution.height,
            fullScreenToggle.isOn
        );
    }

    private void CreateResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            options.Add(option);
            if (Mathf.Approximately(resolutions[i].width, selectedResolution.width) && Mathf.Approximately(resolutions[i].height, selectedResolution.height))
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        selectedResolution = resolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt(resolutionWidthPlayerPrefKey, selectedResolution.width);
        PlayerPrefs.SetInt(resolutionHeightPlayerPrefKey, selectedResolution.height);
        PlayerPrefs.SetInt(resolutionRefreshRatePlayerPrefKey, selectedResolution.refreshRate);
    }

    public void SetMouseSensitivity(float exponentSensitivity)
    {
        var expSens = Mathf.Pow(10f, exponentSensitivity);
        CameraController2.sensX = expSens;
        CameraController2.sensY = expSens;

        PlayerPrefs.SetFloat("MSensitivity", exponentSensitivity);
    }
}
