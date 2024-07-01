using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    private const string DEFAULT_SCREEN_NAME_SMARTPHONE = "smartphone";
    private const string DEFAULT_SCREEN_NAME_SHOPPING = "shopping";
    private const string DEFAULT_SCREEN_NAME_NOTIFICATION = "notification";
    private const string DEFAULT_SCREEN_NAME_GALLERY = "gallery";
    private const string DEFAULT_SCREEN_NAME_SETTINGS = "settings";
    private const string DEFAULT_SCREEN_NAME_REPORT = "report";
    private const string DEFAULT_SCREEN_NAME_CAMERA = "camera";
    private const string DEFAULT_SCREEN_NAME_SAYTALK = "saytalk";

    public GameObject smartPhoneScreen;
    public GameObject shoppingScreen;
    public GameObject notificationScreen;
    public GameObject galleryScreen;
    public GameObject settingsScreen;
    public GameObject reportScreen;
    public GameObject cameraScreen;
    public GameObject sayTalkScreen;


    private Stack<GameObject> activatedScreens;
    private Dictionary<string, GameObject> screens;
    
    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        applyKeyInput();
    }

    private void init()
    {
        instance = this;

        activatedScreens = new Stack<GameObject>();
        screens = new Dictionary<string, GameObject>();

        screens.Add(DEFAULT_SCREEN_NAME_SMARTPHONE, smartPhoneScreen);
        screens.Add(DEFAULT_SCREEN_NAME_SHOPPING, shoppingScreen);
        screens.Add(DEFAULT_SCREEN_NAME_NOTIFICATION, notificationScreen);
        screens.Add(DEFAULT_SCREEN_NAME_GALLERY, galleryScreen);
        screens.Add(DEFAULT_SCREEN_NAME_SETTINGS, settingsScreen);
        screens.Add(DEFAULT_SCREEN_NAME_REPORT, reportScreen);
        screens.Add(DEFAULT_SCREEN_NAME_CAMERA, cameraScreen);
        screens.Add(DEFAULT_SCREEN_NAME_SAYTALK, sayTalkScreen);
    }

    public static UIManager GetInstance()
    {
        if (instance != null) return instance;
        instance = FindObjectOfType<UIManager>();
        if (instance == null) Debug.Log("There's no active UIManager object");
        return instance;
    }

    private void applyKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (activatedScreens.Count <= 0)
            {
                GameManager.PauseGame();
                ActivateScreen(DEFAULT_SCREEN_NAME_SMARTPHONE);
            }
            else
            {
                InactivateScreen();
                if (activatedScreens.Count <= 0) GameManager.ResumeGame();
            }
        }
    }

    public void ActivateScreen(string name)
    {
        GameObject screen;
        if (!screens.TryGetValue(name, out screen))
        {
            Debug.Log($"There's no any screen named \'{name}\'");
            return;
        }        
        else if (screen.activeSelf == true)
        {
            return;
        }

        if (activatedScreens.Count > 0) activatedScreens.Peek().SetActive(false);

        screen.SetActive(true);
        activatedScreens.Push(screen);
    }

    public void InactivateScreen()
    {
        activatedScreens.Pop().SetActive(false);

        if (activatedScreens.Count > 0) activatedScreens.Peek().SetActive(true);
    }
}
