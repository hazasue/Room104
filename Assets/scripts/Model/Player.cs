using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
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
    
    // 플레이어 기본 스텟
    private int S_health = 500;
    private int S_stress = 700;
    private int S_stamina = 1000;
    private int S_int = 500;
    private int S_speed = 1000;
    private int S_money = 0;
    private int S_safe = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.IDLE;
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
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

    public int getHealth()
    {
        int health = this.S_health;
        return health;
    }

    public int getStress()
    {
        int stress = this.S_stress;
        return stress;
    }

    public int getstamina()
    {
        int stamina = this.S_stamina;
        return stamina;
    }

    public int getInt()
    {
        int Int= this.S_int;
        return Int;
    }

    public int getmoney()
    {
        int money = this.S_money;
        return money;
    }

    public int getsafe()
    {
        int safe = this.S_safe;
        return safe;
    }

    // 플레이어 스텟 증가 함수
    public void healthIncrease(int health = 50)
    {
        this.S_health += health;
    }

    public void stressIncrease(int stress = 50)
    {
        this.S_stress += stress;
    }

    public void staminaIncrease(int stamina = 50)
    {
        this.S_stamina += stamina;
    }

    public void intIncrease(int Int = 25)
    {
        this.S_int += Int;
    }

    public void speedIncrease(int speed = 25)
    {
        this.S_speed += speed;
    }

    public void moneyIncrease(int money = 1)
    {
        this.S_money += money;
    }

    public void safeIncrease(int safe = 1)
    {
        this.S_safe += safe;
    }

    // 스텟 감소 함수
    public void healthDecrease(int health = 200)
    {
        this.S_health -= health;
    }

    public void stressDecrease(int stress = 50)
    {
        this.S_stress -= stress;
    }

    public void moneyDecrease(int money = 1)
    {
        this.S_money -= money;
    }
}
