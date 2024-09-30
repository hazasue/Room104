using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainEvent
{
    public enum eRoute
    {
        Man,
        FirstNomal,
        OnlyNomal,
        OnlyTrue,
        TrueAndGood,
        OnlyGood,
        AllExceptFist,
        All
    }

    protected int eventID;
    public int EventID { get { return eventID; } }

    protected eRoute route;

    protected int eventDay;

    protected string occurrenceCondition;

    protected string detailedCondition;

    protected int eventTime;

    protected List<TaskGroup> taskGroups;
    public List<TaskGroup> TaskGroups { get { return taskGroups; } }

    private bool isExecutable;

    private Action conditionCheckFunc;

    public MainEvent(string route, string eventDay, string occurrenceCondition, string detailedCondition, string eventTime, string eventID)
    {
        taskGroups = new List<TaskGroup>();
        this.route = (eRoute)int.Parse(route);
        int.TryParse(eventDay, out this.eventDay);
        this.occurrenceCondition = occurrenceCondition;
        this.detailedCondition = detailedCondition;
        int.TryParse(eventTime, out this.eventTime);
        int.TryParse(eventID, out this.eventID);
        this.isExecutable = false;
    }

    public void AddTaskGroup(TaskGroup group)
    {
        taskGroups.Add(group);
    }

    public void Execute()
    {

    }
}

