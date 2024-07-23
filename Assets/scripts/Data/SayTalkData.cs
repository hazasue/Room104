using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SayTalkHistory
{
    public List<SayTalkData> datas;

    public SayTalkHistory(List<SayTalkData> datas)
    {
        this.datas = datas;
    }
}

public class SayTalkData
{
    public bool isPlayer;
    public string text;

    public SayTalkData(bool isPlayer, string text)
    {
        this.isPlayer = isPlayer;
        this.text = text;
    }
}
