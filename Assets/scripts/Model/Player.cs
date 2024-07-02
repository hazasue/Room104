using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private enum PlayerState
    {
        IDLE,
        WALK,
        RUN,
        INTERACT,
    }
    
    private const float DEFAULT_MOVE_SPEED_WALK = 1f;
    private const float DEFAULT_MOVE_SPEED_RUN = 1.5f;


    private PlayerState state;
    private bool isRunning;
    private Vector2 moveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.IDLE;
        isRunning = false;
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
                
            case PlayerState.IDLE:
                break;
            
            case PlayerState.WALK:
                walk();
                break;
            
            case PlayerState.RUN:
                run();
                break;
            
            case PlayerState.INTERACT:
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
            case PlayerState.IDLE:
                if (moveDirection != Vector2.zero && !isRunning) 
                    state = PlayerState.WALK;
                else if(moveDirection != Vector2.zero && isRunning)
                {
                    state = PlayerState.RUN;
                }
                    
                break;
            
            case PlayerState.WALK:
                if (moveDirection == Vector2.zero) state = PlayerState.IDLE;
                else if (isRunning)
                {
                    state = PlayerState.RUN;
                }
                break;
            
            case PlayerState.RUN:
                if (moveDirection == Vector2.zero) state = PlayerState.IDLE;
                else if (!isRunning)
                {
                    state = PlayerState.WALK;
                }
                break;
            
            case PlayerState.INTERACT:
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
