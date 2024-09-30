using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public enum eGameState
    {
        PAUSED,
        PLAYING,
    }
    
    private eGameState gameState;
    public eGameState GameState { get { return gameState; } }

    private const float DEFAULT_TIME_SCALE_PAUSED = 0f;
    private const float DEFAULT_TIME_SCALE_PLAYING = 1f;
    private const int MAX_SAVE_SLOT_COUNT = 3;
    private const string CSV_FILENAME_PRODUCT = "product_init";

    private Player player;
    public Player Player { get { return player; } }

    private GameData[] gameDatas;
    private Dictionary<int, List<NpcData>> npcDatas;
    private Dictionary<int, ProductData>[] productLists;
    
    private Dictionary<int, ProductData> products;
    public Dictionary<int, ProductData> Products
    {
        get { return products; }
    }
    
    
    private int dataKey;
    private GameData gameData;
    public GameData Data { get { return gameData; } }

    [SerializeField]
    private TMP_Text dateText;

    //플레이 타임 변수
    private int date;
    public int Date { get { return date; } set { date = value; } }
    private int hour;
    public int Hour { get { return hour; } }
    private int minute;
    public int Minute { get { return minute; } }

    public override void Awake()
    {
        base.Awake();
        init();
        date = 1;
        hour = 6;
        minute = 0;
        player.TimeEventHandler += ModifyDateTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameManagerStart");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void init()
    {
        Time.timeScale = DEFAULT_TIME_SCALE_PLAYING;
        gameState = eGameState.PLAYING;
        player = GameObject.Find("Player").GetComponent<Player>();

        initDatas();

        if (JsonManager.LoadJsonFile<CurrentGameInfo>(JsonManager.DEFAULT_CURRENT_DATA_NAME).newGame)
            newGame();
        else
        {
            loadGame(JsonManager.LoadJsonFile<CurrentGameInfo>(JsonManager.DEFAULT_CURRENT_DATA_NAME).dataId);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = DEFAULT_TIME_SCALE_PAUSED;
        gameState = eGameState.PAUSED;
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = DEFAULT_TIME_SCALE_PLAYING;
        gameState = eGameState.PLAYING;
        Debug.Log("Game Playing");
    }

    private void newGame()
    {
        int currentKey = 0;      
        gameData = new GameData("root name", 0, 0, 0, 0f, initNpcTrait(), player.Stat.ConvertToData());
        List<Dictionary<string, object>> productDB = CSVReader.Read(CSV_FILENAME_PRODUCT);
        products = new Dictionary<int, ProductData>();
        foreach (Dictionary<string, object> data in productDB)
        {
            products.Add((int)data["id"],
                new ProductData(data["nameKR"].ToString(), data["nameEN"].ToString(), data["descriptionKR"].ToString(),
                    data["descriptionEN"].ToString(),
                    data["requiredStat"].ToString(), (int)data["requiredStatValue"], (int)data["price"],
                    (int)data["count"])
            );
        }
    }

    private void loadGame(int slotIdx)
    {
        if (slotIdx >= MAX_SAVE_SLOT_COUNT) return;
        
        gameData = gameDatas[slotIdx];
        products = productLists[slotIdx];
        player.Stat.InitSavedStats(gameData.stats);
    }

    public void SaveGame(int slotIdx)
    {
        if (slotIdx >= MAX_SAVE_SLOT_COUNT) return;

        gameDatas[slotIdx] = gameData;
        JsonManager.CreateJsonFile(JsonManager.DEFAULT_GAME_DATA_NAME, gameDatas);

        productLists[slotIdx] = products;
        JsonManager.CreateJsonFile(JsonManager.DEFAULT_PRODUCT_DATA_NAME, productLists);
        
        Dictionary<int, Dictionary<int, SayTalkHistory>> tempDatas = JsonManager.LoadJsonFile<Dictionary< int, Dictionary<int, SayTalkHistory>>>
            (JsonManager.DEFAULT_SAYTALK_DATA_NAME);
        tempDatas[slotIdx] = SayTalkManager.Instance().SayTalkDatas;
        JsonManager.CreateJsonFile(JsonManager.DEFAULT_SAYTALK_DATA_NAME, tempDatas);
    }

    // init npc trait when new game starts
    private List<NpcData> initNpcTrait()
    {
        List<NpcData> npcInitDatas = JsonManager.LoadJsonFile<List<NpcData>>(JsonManager.DEFAULT_NPCINIT_DATA_NAME);

        List<NpcData> tempNpcDatas = new List<NpcData>();
        
        bool trait;

        foreach (NpcData data in npcInitDatas)
        {
            trait = (Random.Range(0, 2) == 0 ? false : true);
            tempNpcDatas.Add(new NpcData(data.id, data.name, trait, data.traitName1, data.traitEvents1, !trait,
                data.traitName2, data.traitEvents2));
            Debug.Log(
                $"{data.id}, {data.name}, {trait}, {data.traitName1}, {data.traitEvents1[0]}, {data.traitEvents1[1]}, {!trait}, {data.traitName2}, {data.traitEvents2[0]}, {data.traitEvents2[1]}");
        }

        return tempNpcDatas;


        // Add tempNpcDatas into npcDatas and save json file
    }

    private void initDatas()
    {
        if (!File.Exists(Application.dataPath + "/Data/" + JsonManager.DEFAULT_NPC_DATA_NAME + ".json"))
        {
            npcDatas = new Dictionary<int, List<NpcData>>();
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_NPC_DATA_NAME, npcDatas);
        }
        else
        {
            npcDatas = JsonManager.LoadJsonFile<Dictionary<int, List<NpcData>>>(JsonManager.DEFAULT_NPC_DATA_NAME);
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
        
        if (!File.Exists(Application.dataPath + "/Data/" + JsonManager.DEFAULT_PRODUCT_DATA_NAME + ".json"))
        {
            productLists = new Dictionary<int, ProductData>[MAX_SAVE_SLOT_COUNT];
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_PRODUCT_DATA_NAME, productLists);
        }
        else
        {
            productLists = JsonManager.LoadJsonFile<Dictionary<int, ProductData>[]>(JsonManager.DEFAULT_PRODUCT_DATA_NAME);
        }
    }

    // setting side event when date changes
    private void resetSideEventList() {}
    
    // setting guest when date changes
    private void resetGuest() {}

    public void ModifyDateTime()
    {
        minute += 10;
        if(minute == 60)
        {
            minute = 0;
            hour += 1;
        }
        if(hour == 24)
        {
            hour = 0;
            date += 1;
        }
        dateText.text = $"Day {date} / {hour} : {minute:D2}";
    }
}
