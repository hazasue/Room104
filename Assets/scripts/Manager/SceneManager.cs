using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance;

    private const string SCENE_NAME_TITLE = "title";
    private const string SCENE_NAME_INGAME = "ingame";
    private const string SCENE_NAME_FIRSTRUN = "firstrun";

    void Awake()
    {
        instance = this;
    }

    public static SceneManager Instance()
    {
        if (instance != null) return instance;
        instance = FindObjectOfType<SceneManager>();
        if (instance == null) Debug.Log("There's no active SceneManager object");
        return instance;
    }
    
    public void LoadScene(string name)
    {
        switch (name)
        {
            case SCENE_NAME_TITLE:
                UnityEngine.SceneManagement.SceneManager.LoadScene(name);
                break;
            
            case SCENE_NAME_INGAME:
                UnityEngine.SceneManagement.SceneManager.LoadScene(name);
                break;
            
            case SCENE_NAME_FIRSTRUN:
                UnityEngine.SceneManagement.SceneManager.LoadScene(name);
                break;
            
            default:
                Debug.Log($"No scene matched named /'{name}/'");
                break;
        }
    }
}
