using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Settings : MonoBehaviour
{
    private static Settings instance;
    
    private static int[] DEFAULT_SCREEN_RESOLUTION_SMALL = new int[2]{ 960, 540 };
    private static int[] DEFAULT_SCREEN_RESOLUTION_DEFAULT = new int[2] { 1280, 720 };
    private static int[] DEFAULT_SCREEN_RESOLUTION_NORMAL = new int[2]{ 1600, 900 };
    private static int[] DEFAULT_SCREEN_RESOLUTION_BIG = new int[2]{ 1920, 1080 };
    private const float DEFAULT_SOUND_VOLUME = 0.33f;

    private const string DEFAULT_LANGUAGE_KOREAN = "한국어";
    private const string DEFAULT_LANGUAGE_ENGLISH = "English";

    private const string DEFAULT_SETTING_NAME_RESOLUTION = "resolution";
    private const string DEFAULT_SETTING_NAME_FULLSCREEN = "fullscreen";
    private const string DEFAULT_SETTING_NAME_VOLUME = "volume";
    private const string DEFAULT_SETTING_NAME_LANGUAGE = "language";

    public TMP_Text languageText;
    public TMP_Dropdown resolution;
    public Toggle fullScreenToggle;
    public Slider masterVolumeSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Toggle masterToggle;
    public Toggle bgmToggle;
    public Toggle sfxToggle;
    public bool isKorean;

    private SettingsData settingsData;
    private Dictionary<int, int> screenResolutions;
    
    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    public static Settings Instance()
    {
        if (instance != null) return instance;
        instance = FindObjectOfType<Settings>();
        if (instance == null) Debug.Log("There's no active Settings object");
        return instance;
    }

    private void init()
    {
        if (File.Exists(Application.dataPath + "/Data/" + JsonManager.DEFAULT_SETTING_DATA_NAME + ".json"))
        {
            settingsData = JsonManager.LoadJsonFile<SettingsData>(JsonManager.DEFAULT_SETTING_DATA_NAME);
        }
        else
        {
            settingsData = new SettingsData(DEFAULT_LANGUAGE_KOREAN, DEFAULT_SCREEN_RESOLUTION_DEFAULT, false, DEFAULT_SOUND_VOLUME, 1f, 1f, true, true, true);
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_SETTING_DATA_NAME, settingsData);
        }

        instance = this;

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

        languageText.text = settingsData.language;
        if (settingsData.language == DEFAULT_LANGUAGE_KOREAN) isKorean = true;
        else
        {
            isKorean = false;
        }

        ApplyLanguageSettings();

        fullScreenToggle.isOn = settingsData.fullScreen;

        masterVolumeSlider.value = settingsData.volumeMaster;
        bgmSlider.value = settingsData.volumeBgm;
        sfxSlider.value = settingsData.volumeSfx;

        masterToggle.isOn = settingsData.masterOn;
        bgmToggle.isOn = settingsData.bgmOn;
        sfxToggle.isOn = settingsData.sfxOn;

        Screen.SetResolution(settingsData.screenResolution[0], settingsData.screenResolution[1],
            settingsData.fullScreen);
        SoundManager.Instance().UpdateVolume(settingsData);
    }

    public void ChangeLanguage()
    {
        if (languageText.text == DEFAULT_LANGUAGE_KOREAN)
        {
            languageText.text = DEFAULT_LANGUAGE_ENGLISH;
            isKorean = false;
        }
        else
        {
            languageText.text = DEFAULT_LANGUAGE_KOREAN;
            isKorean = true;
        }

        ApplyLanguageSettings();
        SaveSettings(DEFAULT_SETTING_NAME_LANGUAGE);
    }

    public void ApplyLanguageSettings()
    {

        switch (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
        {
            case "Title":
                TitleManager.GetInstance().ChangeLanguageSettings(isKorean);
                break;
            
            case "InGame":
                UIManager.GetInstance().ChangeLanguageSettings(isKorean);
                break;
            
            default:
                break;
        }
    }

    public void SaveSettings(string name)
    {
        switch (name)
        {
            case DEFAULT_SETTING_NAME_RESOLUTION:
                string resolutionText = resolution.captionText.text;
                resolutionText = resolutionText.Substring(0, resolutionText.IndexOf(" * "));
                int resolutionX = int.Parse(resolutionText);
                if (resolutionX == settingsData.screenResolution[0]) return;

                settingsData.screenResolution[0] = resolutionX;
                settingsData.screenResolution[1] = screenResolutions[resolutionX];

                Screen.SetResolution(settingsData.screenResolution[0], settingsData.screenResolution[1], settingsData.fullScreen);
                break;
            
            
            case DEFAULT_SETTING_NAME_FULLSCREEN:
                bool fullScreen = fullScreenToggle.isOn;
                if (fullScreen == settingsData.fullScreen) return;

                settingsData.fullScreen = fullScreen;
                Screen.fullScreen = settingsData.fullScreen;
                break;
            
            
            case DEFAULT_SETTING_NAME_VOLUME:
                settingsData.volumeMaster = masterVolumeSlider.value;
                settingsData.volumeBgm = bgmSlider.value;
                settingsData.volumeSfx = sfxSlider.value;
                settingsData.masterOn = masterToggle.isOn;
                settingsData.bgmOn = bgmToggle.isOn;
                settingsData.sfxOn = sfxToggle.isOn;
                
                SoundManager.Instance().UpdateVolume(settingsData);
                break;
            
            case DEFAULT_SETTING_NAME_LANGUAGE:
                settingsData.language = languageText.text;
                break;
            
            default:
                break;
        }

        JsonManager.CreateJsonFile(JsonManager.DEFAULT_SETTING_DATA_NAME, settingsData);
    }
}
