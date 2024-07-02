using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum eGameState
    {
        PAUSED,
        PLAYING,
    }
    
    private eGameState gameState;
    public eGameState GameState { get { return gameState; } }

    private const float DEFAULT_TIME_SCALE_PAUSED = 0f;
    private const float DEFAULT_TIME_SCALE_PLAYING = 1f;


    //플레이 타임 변수
    private int date;
    public int Date { get { return date; } }
    private int hour;
    public int Hour { get { return hour; } }
    private int minute;
    public int Minute { get { return minute; } }

    private void Awake()
    {
        init();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameManagerStart");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void init()
    {
        Time.timeScale = DEFAULT_TIME_SCALE_PLAYING;
        gameState = eGameState.PLAYING;
    }

    public void PauseGame()
    {
        Time.timeScale = DEFAULT_TIME_SCALE_PAUSED;
        gameState = eGameState.PAUSED;
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = DEFAULT_TIME_SCALE_PLAYING;
        gameState = eGameState.PLAYING;
        Debug.Log("Game Playing");
    }
}
