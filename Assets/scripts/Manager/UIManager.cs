using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private const string DEFAULT_SCREEN_NAME_JOBHUNTER = "jobhunter";
    private const string DEFAULT_SCREEN_NAME_SAYTALK = "saytalk";
    private const string DEFAULT_SCREEN_NAME_SAYTALKHISTORY = "saytalkhistory";

    public GameObject smartPhoneScreen;
    public GameObject shoppingScreen;
    public GameObject notificationScreen;
    public GameObject galleryScreen;
    public GameObject settingsScreen;
    public GameObject reportScreen;
    public GameObject cameraScreen;
    public GameObject sayTalkScreen;
    public GameObject sayTalkHistoryScreen;
    public GameObject jobHunterScreen;

    public Transform playerText;
    public Transform npcText;
    public SayTalkRoom sayTalkRoom;
    public Transform sayTalkList;
    public Transform sayTalkHistory;

    public TMP_Text currentDateInfo;
    public LoadGameInfo[] saveFiles = new LoadGameInfo[2];

    public GameObject diaryWarningMessage;

    private Stack<GameObject> activatedScreens;
    private Dictionary<string, GameObject> screens;
    private int dataIdx;
    
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

        initDiary();

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
        screens.Add(DEFAULT_SCREEN_NAME_SAYTALKHISTORY, sayTalkHistoryScreen);
        screens.Add(DEFAULT_SCREEN_NAME_JOBHUNTER, jobHunterScreen);
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
                GameManager.Instance.PauseGame();
                ActivateScreen(DEFAULT_SCREEN_NAME_SMARTPHONE);
            }
            else
            {
                InactivateScreen();
                if (activatedScreens.Count <= 0) GameManager.Instance.ResumeGame();
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
            Debug.Log($"Screen already activated: {name}");
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

    private void initDiary()
    {
        //init current infos, saved datas
        GameData gameData = GameManager.Instance.Data;
        dataIdx = -1;

        currentDateInfo.text = $"{gameData.date} 일차 {gameData.gameTime}";
        
        GameData[] gameDatas = JsonManager.LoadJsonFile<GameData[]>(JsonManager.DEFAULT_GAME_DATA_NAME);

        for (int i = 1; i <= 2; i++)
        {
            if (gameDatas[i] == null) continue;

            saveFiles[i - 1].Init(gameDatas[i]);
        }
    }

    public void SaveData(int idx)
    {
        if (JsonManager.LoadJsonFile<GameData[]>(JsonManager.DEFAULT_GAME_DATA_NAME)[idx] != null)
        {
            diaryWarningMessage.SetActive(true);
            dataIdx = idx;
            return;
        }
        
        GameManager.Instance.SaveGame(idx);
        initDiary();
    }

    public void ClearData(bool state)
    {
        if (state)
        {
            GameData[] gameDatas = JsonManager.LoadJsonFile<GameData[]>(JsonManager.DEFAULT_GAME_DATA_NAME);
            gameDatas[dataIdx] = null;
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_GAME_DATA_NAME, gameDatas);
            SaveData(dataIdx);
        }

        diaryWarningMessage.SetActive(false);
    }

    public void InitSayTalkList()
    {
        Dictionary<int, SayTalkHistory> datas = SayTalkManager.Instance().SayTalkDatas;
        
        for (int i = sayTalkList.childCount - 1; i >= 0; i--)
        {
            Destroy(sayTalkList.GetChild(i).gameObject);
        }

        foreach (KeyValuePair<int, SayTalkHistory> data in datas)
        {
            SayTalkRoom tempTalk = Instantiate(sayTalkRoom, sayTalkList, true);
            if (Settings.Instance().isKorean)
                tempTalk.Init(data.Key, data.Value.datas[data.Value.datas.Count - 1].textKor);
            else
            {
                tempTalk.Init(data.Key, data.Value.datas[data.Value.datas.Count - 1].textEn);
            }
            
            tempTalk.GetComponent<Button>().onClick.AddListener(() => ActivateScreen(DEFAULT_SCREEN_NAME_SAYTALKHISTORY));
            tempTalk.GetComponent<Button>().onClick.AddListener(() => InitSayTalkHistory(data.Key));
            // Instantiate button
            // init button
        }
    }

    public void InitSayTalkHistory(int id)
    {
        List<SayTalkData> data = SayTalkManager.Instance().SayTalkDatas[id].datas;
        
        for (int i = sayTalkHistory.childCount - 1; i >= 0; i--)
        {
            Destroy(sayTalkHistory.GetChild(i).gameObject);
        }

        Transform tempTransform;
        for (int i = 0; i < data.Count; i++)
        {
            // Instantiate Texts according to 'isPlaying'
            if (data[i].isPlayer)
            {
                tempTransform = Instantiate(playerText, sayTalkHistory, true);
                if (Settings.Instance().isKorean)
                    tempTransform.GetChild(1).GetComponent<TMP_Text>().text = $"<align=right>{data[i].textKor}</align>";
                else
                {
                    tempTransform.GetChild(1).GetComponent<TMP_Text>().text = $"<align=right>{data[i].textEn}</align>";
                }

            }
            else
            {
                tempTransform = Instantiate(npcText, sayTalkHistory, true);
                if (Settings.Instance().isKorean)
                    tempTransform.GetChild(2).GetComponent<TMP_Text>().text = $"<align=left>{data[i].textKor}</align>";
                else
                {
                    tempTransform.GetChild(2).GetComponent<TMP_Text>().text = $"<align=left>{data[i].textEn}</align>";
                }

            }
        }
    }
    
    public void ChangeLanguageSettings(bool isKorean)
    {
        if (isKorean)
        {
            notificationScreen.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/InGame/img_phone_criminal_KOR");
            reportScreen.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/InGame/img_phone_{DEFAULT_SCREEN_NAME_REPORT}_KOR");
        }
        else
        {
            notificationScreen.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/InGame/img_phone_criminal_EN");
            reportScreen.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/InGame/img_phone_{DEFAULT_SCREEN_NAME_REPORT}_EN");
        }
    }
}
