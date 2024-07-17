using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEventManager : Singleton<MainEventManager>
{
    private Dictionary<int, MainEvent> eventDic;
    public Dictionary<int, MainEvent> EventDic;

    private MainEvent currMainEvent;

    private void Update()
    {
        currMainEvent.Update();
    }
}
