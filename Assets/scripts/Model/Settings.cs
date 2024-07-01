using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Settings : MonoBehaviour
{
    private static int[] DEFAULT_SCREEN_RESOLUTION_SMALL = new int[2]{ 960, 540 };
    private static int[] DEFAULT_SCREEN_RESOLUTION_DEFAULT = new int[2] { 1280, 720 };
    private static int[] DEFAULT_SCREEN_RESOLUTION_NORMAL = new int[2]{ 1600, 900 };
    private static int[] DEFAULT_SCREEN_RESOLUTION_BIG = new int[2]{ 1920, 1080 };
    private const float DEFAULT_SOUND_VOLUME = 0.33f;

    private const string DEFAULT_SETTING_NAME_RESOLUTION = "resolution";
    private const string DEFAULT_SETTING_NAME_FULLSCREEN = "fullscreen";
    private const string DEFAULT_SETTING_NAME_VOLUME = "volume";

    public TMP_Dropdown resolution;
    public Toggle fullScreenToggle;
    public Slider masterVolumeSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Toggle masterToggle;
    public Toggle bgmToggle;
    public Toggle sfxToggle;

    private SettingsData settingsData;
    private Dictionary<int, int> screenResolutions;
    
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    private void init()
    {
        if (File.Exists(Application.dataPath + "/Data/" + JsonManager.DEFAULT_SETTING_DATA_NAME + ".json"))
        {
            settingsData = JsonManager.LoadJsonFile<SettingsData>(JsonManager.DEFAULT_SETTING_DATA_NAME);
        }
        else
        {
            settingsData = new SettingsData(DEFAULT_SCREEN_RESOLUTION_DEFAULT, false, DEFAULT_SOUND_VOLUME, 1f, 1f, true, true, true);
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_SETTING_DATA_NAME, settingsData);
        }

        screenResolutions = new Dictionary<int, int>();
        screenResolutions.Add(DEFAULT_SCREEN_RESOLUTION_SMALL[0], DEFAULT_SCREEN_RESOLUTION_SMALL[1]);
        screenResolutions.Add(DEFAULT_SCREEN_RESOLUTION_DEFAULT[0], DEFAULT_SCREEN_RESOLUTION_DEFAULT[1]);
        screenResolutions.Add(DEFAULT_SCREEN_RESOLUTION_NORMAL[0], DEFAULT_SCREEN_RESOLUTION_NORMAL[1]);
        screenResolutions.Add(DEFAULT_SCREEN_RESOLUTION_BIG[0], DEFAULT_SCREEN_RESOLUTION_BIG[1]);

        foreach (KeyValuePair<int, int> size in screenResolutions)
        {
            resolution.options.Add(new TMP_Dropdown.OptionData($"{size.Key} * {size.Value}"));
        }
        
        resolution.value = resolution.options.FindIndex(option => option.text == $"{settingsData.screenResolution[0]} * {settingsData.screenResolution[1]}");
        Debug.Log(resolution.options.FindIndex(option =>
            option.text == $"{settingsData.screenResolution[0]} * {settingsData.screenResolution[1]}"));

        fullScreenToggle.isOn = settingsData.fullScreen;
        Screen.fullScreen = settingsData.fullScreen;

        masterVolumeSlider.value = settingsData.volumeMaster;
        bgmSlider.value = settingsData.volumeBgm;
        sfxSlider.value = settingsData.volumeSfx;

        masterToggle.isOn = settingsData.masterOn;
        bgmToggle.isOn = settingsData.bgmOn;
        sfxToggle.isOn = settingsData.sfxOn;
    }

    public void ChangeSettings(string target)
    {
        switch (target)
        {
            case DEFAULT_SETTING_NAME_RESOLUTION:
                string resolutionText = resolution.captionText.text;
                resolutionText = resolutionText.Substring(0, resolutionText.IndexOf(" * "));
                int resolutionX = int.Parse(resolutionText);
                if (resolutionX == settingsData.screenResolution[0]) return;

                settingsData.screenResolution[0] = resolutionX;
                settingsData.screenResolution[1] = screenResolutions[resolutionX];

                SaveSettings(false);
                break;
            
            case DEFAULT_SETTING_NAME_FULLSCREEN:
                settingsData.fullScreen = fullScreenToggle.isOn;
                break;
            
            case DEFAULT_SETTING_NAME_VOLUME:
                settingsData.volumeMaster = masterVolumeSlider.value;
                settingsData.volumeBgm = bgmSlider.value;
                settingsData.volumeSfx = sfxSlider.value;
                settingsData.masterOn = masterToggle.isOn;
                settingsData.bgmOn = bgmToggle.isOn;
                settingsData.sfxOn = sfxToggle.isOn;
                break;
            
            default:
                Debug.Log($"Invalid setting name: {target}");
                break;
        }
    }

    public void SaveSettings(bool saveAll = true)
    {
        if (saveAll)
        {
            settingsData.fullScreen = fullScreenToggle.isOn;
            
            settingsData.volumeMaster = masterVolumeSlider.value;
            settingsData.volumeBgm = bgmSlider.value;
            settingsData.volumeSfx = sfxSlider.value;
            settingsData.masterOn = masterToggle.isOn;
            settingsData.bgmOn = bgmToggle.isOn;
            settingsData.sfxOn = sfxToggle.isOn;
        }

        JsonManager.CreateJsonFile(JsonManager.DEFAULT_SETTING_DATA_NAME, settingsData);
    }
}
