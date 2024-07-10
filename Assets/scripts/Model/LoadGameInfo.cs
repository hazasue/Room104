using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadGameInfo : MonoBehaviour
{
    public TMP_Text rootTypeText;
    public TMP_Text episodeText;
    public TMP_Text infoText;

    public void Init(string rootType, int episode, int date, int gameTime, float playTime)
    {
        rootTypeText.text = rootType;
        episodeText.text = $"{episode}";
        infoText.text = $"{date} 일차 {gameTime}\n플레이 타임: {playTime.ToString("F2")}";
    }

    public void Init(GameData gameData)
    {
        rootTypeText.text = gameData.root;
        episodeText.text = $"{gameData.episode}";
        infoText.text = $"{gameData.date} 일차 {gameData.gameTime}\n플레이 타임: {gameData.playTime.ToString("F2")}";
    }
}
