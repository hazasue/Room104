using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainEvent
{ 
    protected int eventID;
    public int EventID { get { return eventID; } }

    protected int dialogGroupID;
    public int DialogGroupID { get { return dialogGroupID; } }

    protected EventGraph eventGraph;

    public MainEvent(string eventID, string dialogGroupID)
    {
        int.TryParse(eventID, out this.eventID);
        int.TryParse(dialogGroupID, out this.dialogGroupID);
    }

    public abstract void Update();
}
