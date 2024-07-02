using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerStat stat;

    private enum ePlayerState
    {
        IDLE,
        WALK,
        RUN,
        INTERACT,
    }
    
    private const float DEFAULT_MOVE_SPEED_WALK = 1f;
    private const float DEFAULT_MOVE_SPEED_RUN = 1.5f;


    private ePlayerState state;
    private bool isRunning;
    private Vector2 moveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        state = ePlayerState.IDLE;
        isRunning = false;
        PlayerStat stat = GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {   
        /*if (GameManager.Instance.GameState == GameManager.eGameState.PAUSED)
            return;*/

        applyKeyInput();
        updatePlayerState();
        
        switch (state)
        {
                
            case ePlayerState.IDLE:
                break;
            
            case ePlayerState.WALK:
                walk();
                break;
            
            case ePlayerState.RUN:
                run();
                break;
            
            case ePlayerState.INTERACT:
                interact();
                break;
            
            default:
                Debug.Log($"Invalid Player State: {state}");
                break;
        }
    }
   
    public void Init() {}

    private void updatePlayerState()
    {
        switch (state)
        {
            case ePlayerState.IDLE:
                if (moveDirection != Vector2.zero && !isRunning) 
                    state = ePlayerState.WALK;
                else if(moveDirection != Vector2.zero && isRunning)
                {
                    state = ePlayerState.RUN;
                }
                    
                break;
            
            case ePlayerState.WALK:
                if (moveDirection == Vector2.zero) state = ePlayerState.IDLE;
                else if (isRunning)
                {
                    state = ePlayerState.RUN;
                }
                break;
            
            case ePlayerState.RUN:
                if (moveDirection == Vector2.zero) state = ePlayerState.IDLE;
                else if (!isRunning)
                {
                    state = ePlayerState.WALK;
                }
                break;
            
            case ePlayerState.INTERACT:
                break;
            
            default:
                Debug.Log($"Invalid Player State: {state}");
                break;
        }
    }

    private void applyKeyInput()
    {

        Vector2 direction = Vector2.zero;

        // move directions
        if (Input.GetKey(KeyCode.LeftArrow)) direction += new Vector2(-1f, 0f);
        
        if (Input.GetKey(KeyCode.RightArrow)) direction += new Vector2(1f, 0f);
        
        if (Input.GetKey(KeyCode.UpArrow)) direction += new Vector2(0f, 1f);
        
        if (Input.GetKey(KeyCode.DownArrow)) direction += new Vector2(0f, -1f);
        
        // run state
        if (Input.GetKeyDown(KeyCode.LeftShift)) isRunning = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) isRunning = false;


        //// 테스트용
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.GameState == GameManager.eGameState.PLAYING)
                GameManager.Instance.PauseGame();
            else
                GameManager.Instance.ResumeGame();
        }
        ////

        direction = direction.normalized;

        setDirections(direction);
    }

    private void setDirections(Vector2 direction)
    {
        this.moveDirection = direction;
        
        if (direction == Vector2.zero) return;

        if (direction.x >= 0f) this.transform.localScale = new Vector3(1f, 1f, 1f);
        else
        {
            this.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void walk()
    {
        if (moveDirection == Vector2.zero) return;
        
        this.transform.position += new Vector3(moveDirection.x, moveDirection.y, 0f) * Time.deltaTime * DEFAULT_MOVE_SPEED_WALK;
    }

    private void run()
    {
        if (moveDirection == Vector2.zero) return;
        
        this.transform.position += new Vector3(moveDirection.x, moveDirection.y, 0f) * Time.deltaTime * DEFAULT_MOVE_SPEED_RUN;
    }
    
    private void interact() {}
}
