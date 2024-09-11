using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class MiniGame : MonoBehaviour
{
    protected const float DEFAULT_TIME_LIMIT = 10f;
    protected const float MAX_PROGRESS = 1f;
    protected const float DEFAULT_PROGRESS_INCREASE_AMOUNT = 0.04f;
    protected const float DEFAULT_DECREASE_MULTIPLE = 0.2f;
    
    public TMP_Text timer;
    public Slider gauge;
    
    protected float timeLimit;
    protected float progress;
    protected float safety;

    protected bool activated;

    public abstract void Activate(float safety);

    protected abstract void init(float safety);

    protected abstract IEnumerator interact();

    protected virtual IEnumerator decreaseGauge()
    {
        float gap;
        while (activated)
        {
            gap = Time.deltaTime;
            yield return new WaitForSeconds(gap);

            progress -= gap * DEFAULT_DECREASE_MULTIPLE;
            if (progress < 0f) progress = 0f;
            timeLimit -= gap;
            gauge.value = progress;
            timer.text = timeLimit.ToString("F2");

            if (timeLimit <= 0f || progress <= 0f)
            {
                sendClearState(false);
                activated = false;
            }
        }
    }

    protected virtual void sendClearState(bool state)
    {
        MiniGameManager.Instance.GetCurrentMiniGameState(this, state);
    }
}
