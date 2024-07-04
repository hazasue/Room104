using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerStat stat;
    PlayerState state;
    private const float DEFAULT_MOVE_SPEED_WALK = 1f;
    private const float DEFAULT_MOVE_SPEED_RUN = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        state = new IdleState();
        stat = new PlayerStat();
        Debug.Log(InputManager.Instance.GetKeyAction());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameState == GameManager.eGameState.PAUSED)
            return;

        PlayerState temp = state.TransitionState(InputManager.Instance.GetKeyAction());

        if (temp != null)
        {
            state.end(this);
            state = temp;
            state.start(this);
        }

        state.Update(this);

    }

    private void FixedUpdate()
    {
        state.FixedUpdate(this);
    }
}

