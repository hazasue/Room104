using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Dictionary<int, GameData> gameDatas;
    private Dictionary<int, List<NpcData>> npcDatas;

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

        if (!File.Exists(Application.dataPath + "/Data/" + JsonManager.DEFAULT_NPC_DATA_NAME + ".json"))
        {
            npcDatas = new Dictionary<int, List<NpcData>>();
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_NPC_DATA_NAME, npcDatas);
        }
        else
        {
            npcDatas = JsonManager.LoadJsonFile<Dictionary<int, List<NpcData>>>(JsonManager.DEFAULT_NPC_DATA_NAME);
        }

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
        if (!File.Exists(Application.dataPath + "/Data/" + JsonManager.DEFAULT_GAME_DATA_NAME + ".json"))
        {
            gameDatas = new Dictionary<int, GameData>();
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_GAME_DATA_NAME, gameDatas);
        }
        else
        {
            gameDatas = JsonManager.LoadJsonFile<Dictionary<int, GameData>>(JsonManager.DEFAULT_GAME_DATA_NAME);
        }

        int currentKey = 0;
        
        if(gameDatas.Count <= 0) 
            gameDatas.Add(0, new GameData("test", 0, 0, 0, 0f, initNpcTrait()));
        else
        {
            foreach (int data in gameDatas.Keys)
            {
                if (data > currentKey) currentKey = data;
            }
            
            gameDatas.Add(currentKey + 1, new GameData("test", 0, 0, 0, 0f, initNpcTrait()));
        }

        JsonManager.CreateJsonFile(JsonManager.DEFAULT_GAME_DATA_NAME, gameDatas);
    }

    private void loadGame(int dataId)
    {
        
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
    
    // setting side event when date changes
    private void resetSideEventList() {}
    
    // setting guest when date changes
    private void resetGuest() {}
}
