using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void statEventHandler();
    public statEventHandler StatEventHandler;

    public delegate void timeEventHandler();
    public timeEventHandler TimeEventHandler;

    private PlayerStat stat;
    public PlayerStat Stat { get { return stat; } }

    private PlayerState state;
    private PlayerState Stete { get { return state; } }

    public Vector2 direction;

    public Animator anime;

    private Camera camera;

    private const float DEFAULT_MOVE_SPEED_WALK = 1f;
    private const float DEFAULT_MOVE_SPEED_RUN = 1.5f;

    // Start is called before the first frame update
    void Awake()
    {
        anime = GetComponent<Animator>();

        direction = new Vector2(0.0f, 0.0f);
        state = new IdleState();
        stat = new PlayerStat();
        Debug.Log(InputManager.Instance.GetKeyAction());
    }
    private void Start()
    {
        camera = Camera.main;
        camera.gameObject.AddComponent<CameraFollow>().target = this.gameObject.transform;
        GameManager.Instance.SetPlayer(this.gameObject.name);
        UITest.Instance.PlayerUI.GetComponent<PlayerInfoUI>().SetPlayerHandler(this.GetComponent<Player>());
        StatEventHandler();
        TimeEventHandler();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameState == GameManager.eGameState.PAUSED)
            return;

        if (InputManager.Instance.BInputToglle == false)
        {
            PlayerState temp = state.TransitionState(InputManager.Instance.GetKeyAction());

            if (temp != null)
            {
                state.end(this);
                state = temp;
                state.start(this);
            }
        }

        state.Update(this);

    }

    private void FixedUpdate()
    {
        state.FixedUpdate(this);
    }
}

