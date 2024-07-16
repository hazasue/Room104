using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string root;
    public int episode;
    public int date;
    public int gameTime;
    public float playTime;
    public List<NpcData> npcDatas;
    public StatsData stats;

    //side event lists

    public GameData(string root, int episode, int date, int gameTime, float playTime, List<NpcData> npcDatas, StatsData stats)
    {
        this.root = root;
        this.episode = episode;
        this.date = date;
        this.gameTime = gameTime;
        this.playTime = playTime;
        this.npcDatas = npcDatas;
        this.stats = stats;
    }
}
