using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStat stat;
    public PlayerStat Stat { get { return stat; } }

    private PlayerState state;
    private PlayerState Stete { get { return state; } }

    public Vector2 direction;

    private const float DEFAULT_MOVE_SPEED_WALK = 1f;
    private const float DEFAULT_MOVE_SPEED_RUN = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(0.0f, 0.0f);
        state = new IdleState();
        stat = new PlayerStat();
        Debug.Log(InputManager.Instance.GetKeyAction());
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

