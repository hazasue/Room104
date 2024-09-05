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
    public string textKor;
    public string textEn;

    public SayTalkData(bool isPlayer, string textKor, string textEn)
    {
        this.isPlayer = isPlayer;
        this.textKor = textKor;
        this.textEn = textEn;
    }
}
