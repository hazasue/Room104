using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : Singleton<TaskManager>
{
    private Dictionary<int, TaskGroup> taskGroupDic;
    public Dictionary<int, TaskGroup> TaskGroupDic { get { return taskGroupDic; } }

    private int taskCnt = 0;
    private int groupCnt = 2000;
    private int taskMax = 0;
    public override void Awake()
    {
        base.Awake();
        taskGroupDic = new Dictionary<int, TaskGroup>();
        Debug.Log("TaskManager»ý¼ºµÊ");
    }


    public void Start()
    {
        taskMax = taskGroupDic[2000].Tasks.Count;
    }

    public void Update()
    {
        if (taskCnt < taskMax)
        {
            if (taskGroupDic[groupCnt].Tasks[taskCnt].State != Task.eState.End)
                taskGroupDic[groupCnt].Tasks[taskCnt].Execute();
            else
                taskCnt += 1;
        }
        else
        {
            taskCnt = 0;
            groupCnt += 1;
            taskMax = taskGroupDic[groupCnt].Tasks.Count;
        }
    }

    public TaskGroup GetTaskList(int groupId)
    {
        if (!taskGroupDic.ContainsKey(groupId)) return new TaskGroup();

        return taskGroupDic[groupId];
    }
}
