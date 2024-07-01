using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{

    public GameObject settingScreen;
    
    private Stack<GameObject> screens;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void init()
    {
        screens = new Stack<GameObject>();
    }

    public void ActivateScreen(string name)
    {
        
    }

    public void InactivateScreen()
    {
        
    }
    
    public void NewGame() {}
    
    public void LoadGame() {}
}
