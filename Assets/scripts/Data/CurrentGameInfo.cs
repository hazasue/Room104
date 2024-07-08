using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentGameInfo
{
    public bool newGame;
    public int dataId;

    public CurrentGameInfo(bool newGame, int dataId)
    {
        this.newGame = newGame;
        this.dataId = dataId;
    }
}
