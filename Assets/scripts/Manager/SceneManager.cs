using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private const string SCENE_NAME_TITLE = "title";
    private const string SCENE_NAME_INGAME = "ingame";

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
            
            default:
                Debug.Log($"No scene matched named /'{name}/'");
                break;
        }
    }
}
