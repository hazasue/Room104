using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public int[] screenResolution;
    public bool fullScreen;
    public float volumeMaster;
    public float volumeBgm;
    public float volumeSfx;
    public bool masterOn;
    public bool bgmOn;
    public bool sfxOn;

    public SettingsData(int[] screenResolution, bool fullScreen, float volumeMaster, float volumeBgm, float volumeSfx, bool masterOn, bool bgmOn, bool sfxOn)
    {
        if (screenResolution.Length != 2)
        {
            Debug.Log($"Out of form: Screen Resolution(size: {screenResolution.Length})");
            return;
        }
        
        this.screenResolution = screenResolution;
        this.fullScreen = fullScreen;
        this.volumeMaster = volumeMaster;
        this.volumeBgm = volumeBgm;
        this.volumeSfx = volumeSfx;
        this.masterOn = masterOn;
        this.bgmOn = bgmOn;
        this.sfxOn = sfxOn;
    }
}
