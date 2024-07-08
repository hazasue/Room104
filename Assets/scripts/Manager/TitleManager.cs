using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    private static TitleManager instance;

    private const string DEFAULT_SCREEN_NAME_NEWGAME = "newgame";
    private const string DEFAULT_SCREEN_NAME_LOADGAME = "loadgame";
    private const string DEFAULT_SCREEN_NAME_SETTINGS = "settings";
    private const string DEFAULT_SCREEN_NAME_CREDIT = "credit";

    public GameObject newGameScreen;
    public GameObject loadGameScreen;
    public GameObject settingsScreen;
    public GameObject creditScreen;

    private Dictionary<string, GameObject> screens;
    private Stack<GameObject> activatedScreens;

    private Dictionary<int, GameData> gameDatas;
    public Transform loadViewPort;
    public LoadGameInfo loadGamePrefab;
    
    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    private void init()
    {
        instance = this;
        
        if (!File.Exists(Application.dataPath + "/Data/" + JsonManager.DEFAULT_CURRENT_DATA_NAME + ".json"))
        {
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_CURRENT_DATA_NAME, new CurrentGameInfo(true, 0));
        }

        if (!File.Exists(Application.dataPath + "/Data/" + JsonManager.DEFAULT_GAME_DATA_NAME + ".json"))
        {
            gameDatas = new Dictionary<int, GameData>();
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_GAME_DATA_NAME, gameDatas);
        }
        else
        {
            gameDatas = JsonManager.LoadJsonFile<Dictionary<int, GameData>>(JsonManager.DEFAULT_GAME_DATA_NAME);

            foreach (GameData data in gameDatas.Values)
            {
                LoadGameInfo go = Instantiate(loadGamePrefab, loadViewPort, true);
                go.Init(data.root, data.episode, data.date, data.gameTime, data.playTime);
            }
        }

        activatedScreens = new Stack<GameObject>();
        screens = new Dictionary<string, GameObject>();

        screens.Add(DEFAULT_SCREEN_NAME_NEWGAME, newGameScreen);
        screens.Add(DEFAULT_SCREEN_NAME_LOADGAME, loadGameScreen);
        screens.Add(DEFAULT_SCREEN_NAME_SETTINGS, settingsScreen);
        screens.Add(DEFAULT_SCREEN_NAME_CREDIT, creditScreen);
    }
    
    public static TitleManager GetInstance()
    {
        if (instance != null) return instance;
        instance = FindObjectOfType<TitleManager>();
        if (instance == null) Debug.Log("There's no active TitleManager object");
        return instance;
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

    public void NewGame()
    {
        JsonManager.CreateJsonFile(JsonManager.DEFAULT_CURRENT_DATA_NAME, new CurrentGameInfo(true, 0));
    }

    public void LoadGame()
    {
        JsonManager.CreateJsonFile(JsonManager.DEFAULT_CURRENT_DATA_NAME, new CurrentGameInfo(false, 0));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
