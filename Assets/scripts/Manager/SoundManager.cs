using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;
    
    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    private void init()
    {
        instance = this;
        
        DontDestroyOnLoad(this);
    }

    public static SoundManager Instance()
    {
        if (instance != null) return instance;
        instance = FindObjectOfType<SoundManager>();
        if (instance == null) Debug.Log("There's no active SoundManager object");
        return instance;
    }

    public void UpdateVolume(SettingsData data)
    {
        if (!data.masterOn)
        {
            bgmSource.volume = 0f;
            sfxSource.volume = 0f;
            return;
        }

        if (data.bgmOn)
            bgmSource.volume = data.volumeMaster * data.volumeBgm;
        else
        {
            bgmSource.volume = 0f;
        }

        if (data.sfxOn)
            sfxSource.volume = data.volumeMaster * data.volumeSfx;
        else
        {
            sfxSource.volume = 0f;
        }
    }
}
