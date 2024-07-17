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

    private static Vector3 DEFAULT_ARROW_POSITION = new Vector3(50f, -100f, 0f);
    private static Vector3 DEFAULT_ARROW_POSITION_GAP = new Vector3(0f, -260f, 0f);

    private static Vector3 DEFAULT_HOVER_POSITION = new Vector3(0f, 0f, 0f);
    private static Vector3 DEFAULT_HOVER_GAP = new Vector3(0f, -100f, 0f);
    
    private const int MAX_SAVE_SLOT_COUNT = 3;

    public Transform hover;
    
    public RawImage title;
    public GameObject newGameScreen;
    public GameObject loadGameScreen;
    public GameObject settingsScreen;
    public GameObject creditScreen;

    private Dictionary<string, GameObject> screens;
    private Stack<GameObject> activatedScreens;

    private GameData[] gameDatas;
    private int selectedSlot;
    public Transform arrowObject;
    public Transform loadViewPort;
    public LoadGameInfo loadGamePrefab;
    
    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    void Update()
    {
        applyKeyInput();
    }

    private void init()
    {
        instance = this;

        initDatas();

        activatedScreens = new Stack<GameObject>();
        screens = new Dictionary<string, GameObject>();

        screens.Add(DEFAULT_SCREEN_NAME_NEWGAME, newGameScreen);
        screens.Add(DEFAULT_SCREEN_NAME_LOADGAME, loadGameScreen);
        screens.Add(DEFAULT_SCREEN_NAME_SETTINGS, settingsScreen);
        screens.Add(DEFAULT_SCREEN_NAME_CREDIT, creditScreen);

        selectedSlot = 0;

        changeTitleImage();
    }

    private void initDatas()
    {
        if (!File.Exists(Application.dataPath + "/Data/" + JsonManager.DEFAULT_CURRENT_DATA_NAME + ".json"))
        {
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_CURRENT_DATA_NAME, new CurrentGameInfo(true, 0));
        }
        
        if(!File.Exists(Application.dataPath + "/Data/" + JsonManager.DEFAULT_ACCOUNT_DATA_NAME + ".json"))
        {
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_ACCOUNT_DATA_NAME, new AccountInfo(null));
        }

        if (!File.Exists(Application.dataPath + "/Data/" + JsonManager.DEFAULT_GAME_DATA_NAME + ".json"))
        {
            gameDatas = new GameData[MAX_SAVE_SLOT_COUNT];
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_GAME_DATA_NAME, gameDatas);
        }
        else
        {
            gameDatas = JsonManager.LoadJsonFile<GameData[]>(JsonManager.DEFAULT_GAME_DATA_NAME);
        }
        
        foreach (GameData data in gameDatas)
        {
            LoadGameInfo go = Instantiate(loadGamePrefab, loadViewPort, true);
            if(data != null) go.Init(data.root, data.episode, data.date, data.gameTime, data.playTime);
            else
            {
                Debug.Log("null data");
            }
        }
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

    public void LoadGame(int idx)
    {
        JsonManager.CreateJsonFile(JsonManager.DEFAULT_CURRENT_DATA_NAME, new CurrentGameInfo(false, idx));
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    private void applyKeyInput()
    {
        // if load game screen activated
        if (!loadGameScreen.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.UpArrow) && selectedSlot > 0)
            changeSelectArrowPosition(--selectedSlot);
        else if (Input.GetKeyDown(KeyCode.DownArrow) && selectedSlot < MAX_SAVE_SLOT_COUNT - 1)
        {
            changeSelectArrowPosition(++selectedSlot);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (gameDatas[selectedSlot] == null) return;
            LoadGame(selectedSlot);
            SceneManager.Instance().LoadScene("ingame");
        }
    }

    private void changeSelectArrowPosition(int idx)
    {
        if (idx >= MAX_SAVE_SLOT_COUNT) return;

        arrowObject.localPosition = DEFAULT_ARROW_POSITION + (DEFAULT_ARROW_POSITION_GAP * idx);
    }

    public void HoverButton(int idx)
    {
        hover.localPosition = DEFAULT_HOVER_POSITION + DEFAULT_HOVER_GAP * idx;
    }

    public void ActivateHover(bool state)
    {
        hover.gameObject.SetActive(state);
    }

    private void changeTitleImage()
    {
        string currentEnding =
            JsonManager.LoadJsonFile<AccountInfo>(JsonManager.DEFAULT_ACCOUNT_DATA_NAME).currentEnding;

        if (currentEnding == null)
        {
            // Activate special screen that has setting options
            SceneManager.Instance().LoadScene("firstrun");
            /*
            **Add to GameManager.newGame()
            string root;
            switch(JsonManager.LoadJsonFile<AccountInfo>(JsonManager.DEFAULT_ACCOUNT_DATA_NAME).currentEnding)
            {
                case null:
                    root = "fake";
                    break;
                
                case "bad":
                    root = "normal";
                    break;
                    
                case "normal":
                    root = "good";
                    break;
                
                case "good":
                    root = "true";
                    break;
                    
                case "true":
                    root = "true";
                    break;
                
                default:
                    Debug.Log($"Invalid current ending");
                    break;
            }
            
            gameData = new GameData(root, 0, 0, 0, 0f, initNpcTrait(), player.Stat.ConvertToData());
             */
            return;
        }

        title.texture = Resources.Load<Texture>($"Sprites/Title/{currentEnding}");
    }
}
