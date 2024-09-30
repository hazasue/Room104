using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{

    private const string CSV_FILENAME_MAINEVENT = "MainEvent";
    private const string CSV_FILENAME_MAINEVENT_DIALOG = "MainEventTaskXL";

    void Start()
    {
        Debug.Log("DataManager생성됨");
        LoadMainEventData();
        LoadMainEventTaskData();
    }

    void Update()
    {

    }

    public void LoadMainEventData()
    {
        ///////
        ///NOTE
        ///속성 이름은 다음과 같습니다.
        ///Route,EventDay,Trigger,Details,EventTime,EventID
        //////

        List<Dictionary<string, object>> mainEventDB = CSVReader.Read(CSV_FILENAME_MAINEVENT);
        foreach (Dictionary<string, object> data in mainEventDB)
        {
            int eventID = (int)data["EventID"];
            MainEventManager.Instance.EventDic.Add
                (eventID, new MainEvent(data["Route"].ToString(), data["EventDay"].ToString(), data["Trigger"].ToString(),
                data["Details"].ToString(), data["EventTime"].ToString(), data["EventID"].ToString())
                );
            Debug.Log(MainEventManager.Instance.EventDic[eventID].EventID.ToString());
        }
    }

    public void LoadMainEventTaskData()
    {
        //////
        ///NOTE
        ///속성 이름은 다음과 같습니다.
        ///EventID,TaskGroupID,TaskID,TaskType,ObjectName,RescName,RescNum,RescEffect,ImageDir,NameKor,NameEng,TextKor,TextEng,System
        //////

        int currEventID = 0;
        int currGroupID = 0;
        Dictionary<int, TaskGroup> temp = TaskManager.Instance.TaskGroupDic;
        TaskGroup taskGroup = new TaskGroup();
        List<Dictionary<string, object>> mainEventTaskDB = CSVReader.Read(CSV_FILENAME_MAINEVENT_DIALOG);
        foreach (Dictionary<string, object> data in mainEventTaskDB)
        {
            int tempEventID = (int)data["EventID"];
            int tempGroupID = (int)data["TaskGroupID"];
/*            Debug.Log(data["EventID"].ToString() + data["TaskGroupID"].ToString() + data["TaskID"].ToString() + data["TaskType"].ToString() + data["ObjectName"].ToString()
                + data["RescName"].ToString() + data["RescNum"].ToString() +
                data["RescEffect"].ToString() + data["ImageDir"].ToString() + data["NameKor"].ToString() + data["NameEng"].ToString() + data["TextKor"].ToString() + data["TextEng"].ToString() +
                data["System"].ToString());*/
            if (tempEventID != currEventID)
                currEventID = tempEventID;

            if (tempGroupID != currGroupID)
            {
                currGroupID = tempGroupID;
                taskGroup = new TaskGroup(data["TaskGroupID"].ToString());
                TaskManager.Instance.TaskGroupDic.Add(currGroupID, taskGroup);
                MainEventManager.Instance.EventDic[currEventID].AddTaskGroup(taskGroup);
                taskGroup.AddTask(classifyTask(data["TaskType"].ToString(), data));
            }
            else
            {
                taskGroup.AddTask(classifyTask(data["TaskType"].ToString(), data));
            }
        }
    }

    private Task classifyTask(string type, Dictionary<string, object> data)
    {
        switch (type)
        {
            case "text":
                return new TextOutput(data["TaskID"].ToString(), data["RescEffect"].ToString(), data["TextKor"].ToString(), data["System"].ToString());
            case "time":
                return new Timer(data["TaskID"].ToString(), data["RescEffect"].ToString(), data["System"].ToString());
            case "image1":
            case "image2":
            case "image3":
                return new ImageOutput(data["TaskID"].ToString(), type, data["RescName"].ToString(), data["RescNum"].ToString(), data["ImageDir"].ToString(), data["RescEffect"].ToString(), data["System"].ToString());
            case "object":
                return new SpriteModifier(data["TaskID"].ToString(), data["ObjectName"].ToString(), data["RescName"].ToString(), data["RescNum"].ToString(), data["RescEffect"].ToString(), data["System"].ToString());
            case "narrative":
                return new Narrative(data["TaskID"].ToString(), data["RescEffect"].ToString(), data["TextKor"].ToString(), data["System"].ToString());
            case "portrait":
                return new Portrait(data["TaskID"].ToString(), data["RescName"].ToString(), data["RescNum"].ToString(), data["RescEffect"].ToString(), data["NameKor"].ToString(), data["TextKor"].ToString(), data["System"].ToString());
            case "spawn":
                return new ObjectSpawner(data["TaskID"].ToString(), data["ObjectName"].ToString(), data["RescName"].ToString(), data["RescEffect"].ToString(), data["System"].ToString());
            case "bgm":
                return new BGM(data["TaskID"].ToString(), data["RescName"].ToString(), data["RescEffect"].ToString(), data["System"].ToString());
            case "sfx":
                return new SFX(data["TaskID"].ToString(), data["RescName"].ToString(), data["RescNum"].ToString(), data["RescEffect"].ToString(), data["System"].ToString());
            case "walk":
            case "run":
                return new ObjectMove(data["TaskID"].ToString(), data["ObjectName"].ToString(), data["RescName"].ToString(), data["System"].ToString());
            case "animation":
                return new Animation(data["TaskID"].ToString(), data["ObjectName"].ToString(), data["RescName"].ToString(), data["System"].ToString());
            default:
                Debug.Log("파일에서 올바른 타입을 입력 받지 못하여 Task를 생성하지 못하였습니다." + data["TaskID"].ToString());
                return null;
        }
    }

}
