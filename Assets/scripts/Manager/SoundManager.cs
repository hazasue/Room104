using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    private void init()
    {

        DontDestroyOnLoad(this);
    }
}
