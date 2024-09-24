using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGroup
{
    private int taskGroupID;
    public int TaskGroupID { get { return taskGroupID; } }
    private List<Task> tasks;
    public List<Task> Tasks { get { return tasks; } }

    public TaskGroup()
    {

    }

    public TaskGroup(string id)
    {
        tasks = new List<Task>();
        int.TryParse(id, out this.taskGroupID);
    }

    public void AddTask(Task task)
    {
        tasks.Add(task);
    }
}
