using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEventManager : Singleton<MainEventManager>
{
    public delegate void mainEventHandler();
    public mainEventHandler MainEventHandler;

    private Dictionary<int, MainEvent> eventDic;
    public Dictionary<int, MainEvent> EventDic { get { return eventDic; } }

    private MainEvent currEvent;
    private int currEventID = 0;
    public override void Awake()
    {
        base.Awake();
        eventDic = new Dictionary<int, MainEvent>();
        Debug.Log("MainEventManager»ý¼ºµÊ");
    }

    public void Start()
    {
    }

    private void Update()
    {
        //currEvent.Execute();
    }
}
