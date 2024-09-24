using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : Singleton<TaskManager>
{
    public TMP_Text narrativeText;
    public TMP_Text portraitText;
    public GameObject narrativeObject;
    public GameObject portraitObject;

    private Dictionary<int, TaskGroup> taskGroupDic;
    public Dictionary<int, TaskGroup> TaskGroupDic { get { return taskGroupDic; } }

    private int i = 0;

    public override void Awake()
    {
        base.Awake();
        taskGroupDic = new Dictionary<int, TaskGroup>();
        Debug.Log("TaskManager»ý¼ºµÊ");
    }

    public void Update()
    {
        if (i < 6)
        {
            if (taskGroupDic[2000].Tasks[i].State != Task.eState.End)
                taskGroupDic[2000].Tasks[i].Excute();
            else
                i += 1;
        }
    }

    public TaskGroup GetTaskList(int groupId)
    {
        if (!taskGroupDic.ContainsKey(groupId)) return new TaskGroup();

        return taskGroupDic[groupId];
    }
}
