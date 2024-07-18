using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleScreenCloser : MonoBehaviour
{
    public List<GameObject> screens;

    public void CloseScreen(string scene)
    {
        if (screens.Count > 1)
        {
            screens[screens.Count - 1].SetActive(false);
            screens.RemoveAt(screens.Count - 1);
        }
        else
        {
            SceneManager.Instance().LoadScene(scene);
        }
    }
}
