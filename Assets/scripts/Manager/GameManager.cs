using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        PAUSED,
        PLAYING,
    }
    
    private static GameManager instance;
    public static GameState gameState;

    private const float DEFAULT_TIME_SCALE_PAUSED = 0f;
    private const float DEFAULT_TIME_SCALE_PLAYING = 1f;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void init()
    {
        instance = this;

        Time.timeScale = DEFAULT_TIME_SCALE_PLAYING;
        gameState = GameState.PLAYING;

    }
    
    public static GameManager GetInstance()
    {
        if (instance != null) return instance;
        instance = FindObjectOfType<GameManager>();
        if (instance == null) Debug.Log("There's no active GameManager object");
        return instance;
    }

    public static void PauseGame()
    {
        Time.timeScale = DEFAULT_TIME_SCALE_PAUSED;
        gameState = GameState.PAUSED;
    }

    public static void ResumeGame()
    {
        Time.timeScale = DEFAULT_TIME_SCALE_PLAYING;
        gameState = GameState.PLAYING;
    }
}
