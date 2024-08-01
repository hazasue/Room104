using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SayTalkManager : MonoBehaviour
{
    private static SayTalkManager instance;
    
    private const string CSV_FILENAME_SAYTALK = "say_talk";
    private const string SPLIT_STANDARD = ".";
    private const int MAX_SAVE_SLOT_COUNT = 3;

    private Dictionary<int, Dictionary<int, SayTalkHistory>> datas;

    private SayTalkHistory dummyData;
    private Dictionary<int, SayTalkHistory> sayTalkDatas;
    public Dictionary<int, SayTalkHistory> SayTalkDatas
    {
        get { return sayTalkDatas; }
    }
    
    private List<SayTalkData> sayTalkData;
    private Dictionary<int, List<SayTalk>> sayTalkLists;   
    private List<SayTalk> currentSayTalkList;
    private int currentSayTalkIdx;
    private int targetId;

    private GameObject currentObject;
    
    public TMP_Text narrativeText;
    public GameObject nullObject;
    public GameObject narrativeObject;
    public GameObject updateObject;

    public Dictionary<string, string> objects; 
    
    
    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    public static SayTalkManager Instance()
    {
        if (instance != null) return instance;
        instance = FindObjectOfType<SayTalkManager>();
        if (instance == null) Debug.Log("There's no active SayTalkManager object");
        return instance;
    }

    private void init()
    {
        initDatas();
        
        instance = this;

        sayTalkLists = new Dictionary<int, List<SayTalk>>();
        objects = new Dictionary<string, string>();
        objects.Add("img_phone_base", "saytalk");
        objects.Add("img_phone_sayTalk", "saytalkhistory");
        
        int eventId;
        List<SayTalk> tempTalks;
        SayTalk tempTalk;
        Color32 color;
        string[] splitColor;
        currentObject = nullObject;
        sayTalkData = null;
        targetId = -1;
        //event_id, dialog_num, type, isPlayer, autoSkip, text, duration, loop, color, direction
        
        List<Dictionary<string, object>> mainEventDB = CSVReader.Read(CSV_FILENAME_SAYTALK);
        foreach (Dictionary<string, object> data in mainEventDB)
        {
            eventId = (int)data["event_id"];

            if (!sayTalkLists.TryGetValue(eventId, out tempTalks))
            {
                sayTalkLists.Add(eventId, new List<SayTalk>());
            }

            splitColor = data["color"].ToString().Split(SPLIT_STANDARD);
            color = new Color32(Convert.ToByte(splitColor[0]), Convert.ToByte(splitColor[1]), Convert.ToByte(splitColor[2]), Convert.ToByte(splitColor[3]));
            tempTalk = new SayTalk((int)data["dialog_num"], data["type"].ToString(),
                Convert.ToBoolean(data["isPlayer"].ToString()), Convert.ToBoolean(data["autoSkip"].ToString()),
                data["text"].ToString(), float.Parse(data["duration"].ToString()),
                Convert.ToBoolean(data["loop"].ToString()), color, data["direction"].ToString(), (int)data["target"]);
            sayTalkLists[eventId].Add(tempTalk);
        }

        initSayTalk(10001);
    }

    private void initDatas()
    {
        if (!File.Exists(Application.dataPath + "/Data/" + JsonManager.DEFAULT_SAYTALK_DATA_NAME + ".json"))
        {
            datas = new Dictionary<int, Dictionary<int, SayTalkHistory>>();
            for (int i = 0; i < MAX_SAVE_SLOT_COUNT; i++)
            {
                datas.Add(i, null);
            }
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_SAYTALK_DATA_NAME, datas);
        }
        else
        {
            datas = JsonManager.LoadJsonFile<Dictionary<int, Dictionary<int, SayTalkHistory>>>(JsonManager.DEFAULT_SAYTALK_DATA_NAME);
        }

        if (JsonManager.LoadJsonFile<CurrentGameInfo>(JsonManager.DEFAULT_CURRENT_DATA_NAME).newGame)
        {
            sayTalkDatas = new Dictionary<int, SayTalkHistory>();
        }
        else
        {
            sayTalkDatas = datas[JsonManager.LoadJsonFile<CurrentGameInfo>(JsonManager.DEFAULT_CURRENT_DATA_NAME).dataId];
        }
    }
    
    private void initSayTalk(int eventId)
    {
        currentSayTalkList = sayTalkLists[eventId];
        currentSayTalkIdx = 0;
        

        handleSayTalk(currentSayTalkList[currentSayTalkIdx++]);
    }
    

    public List<SayTalk> GetSayTalkList(int eventId)
    {
        if (!sayTalkLists.ContainsKey(eventId)) return new List<SayTalk>();
        
        return sayTalkLists[eventId];
    }

    private void handleSayTalk(SayTalk sayTalk)
    {
        if (narrativeObject.activeSelf == false) narrativeObject.SetActive(false);
        
        switch (sayTalk.Type)
        {
            case eSayTalkType.NARRATIVE:
                // activate narrative screen
                toggleCurrentObject(narrativeObject);
                narrativeText.text = sayTalk.Text;
                // update text
                break;
            
            case eSayTalkType.SFX:
                // loop, duration, clip
                toggleCurrentObject(nullObject);
                SoundManager.Instance().ChangeSFX(sayTalk.Text, sayTalk.Loop);
                UpdateSayTalk(sayTalk.Duration);
                break;
            
            case eSayTalkType.OBJECT:
                toggleCurrentObject(nullObject);
                UIManager.GetInstance().ActivateScreen(objects[sayTalk.Text]);
                UpdateSayTalk(sayTalk.Duration);
                break;
            
            case eSayTalkType.TEXT:
                // update say talk text list
                toggleCurrentObject(updateObject);
                if (sayTalkData == null)
                {
                    if (!sayTalkDatas.TryGetValue(sayTalk.Target, out dummyData))
                    {
                        sayTalkDatas.Add(sayTalk.Target, new SayTalkHistory(new List<SayTalkData>()));
                    }
                    sayTalkData = sayTalkDatas[sayTalk.Target].datas;
                    targetId = sayTalk.Target;
                }
                sayTalkData.Add(new SayTalkData(sayTalk.IsPlayer, sayTalk.Text));
                UIManager.GetInstance().InitSayTalkHistory(sayTalk.Target);
                break;
            
            case eSayTalkType.ANIMATION:
                toggleCurrentObject(nullObject);
                UpdateSayTalk(sayTalk.Duration);
                break;
            
            default:
                Debug.Log($"Invalid say talk type: {sayTalk.Type}");
                break;
        }
    }

    public void UpdateSayTalk(float delay = 0f)
    {
        StartCoroutine(updateSayTalk(delay));
    }

    private IEnumerator updateSayTalk(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (currentSayTalkList == null 
            || currentSayTalkList.Count <= currentSayTalkIdx)
        {
            // exit dialog
            currentObject.SetActive(false);
            currentSayTalkList = null;
            currentSayTalkIdx = 0;
            currentObject = nullObject;
            sayTalkData = null;
            targetId = -1;
            yield break;
        }

        handleSayTalk(currentSayTalkList[currentSayTalkIdx++]);
    }
    
    private void toggleCurrentObject(GameObject selectedObject)
    {
        if (currentObject != selectedObject)
        {
            if(currentObject != null) currentObject.SetActive(false);
            currentObject = selectedObject;
            currentObject.SetActive(true);
        }
    }
}
