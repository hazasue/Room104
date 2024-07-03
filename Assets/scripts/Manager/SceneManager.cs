using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private const string SCENE_NAME_TITLE = "title";
    private const string SCENE_NAME_INGAME = "ingame";

    public static void LoadScene(string name)
    {
        switch (name)
        {
            case SCENE_NAME_TITLE:
                SceneManager.LoadScene(name);
                break;
            
            case SCENE_NAME_INGAME:
                SceneManager.LoadScene(name);
                break;
            
            default:
                Debug.Log($"No scene matched named /'{name}/'");
                break;
        }
    }
}
