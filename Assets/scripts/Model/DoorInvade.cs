using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInvade : MiniGame
{
    public override void Activate(float safety)
    {
        activated = true;
        init(safety);
        StartCoroutine(decreaseGauge());
        StartCoroutine(interact());
    }

    protected override void init(float safety)
    {
        timeLimit = DEFAULT_TIME_LIMIT + safety;
        timer.text = timeLimit.ToString("F2");

        gauge.maxValue = MAX_PROGRESS;
        gauge.value = 0f;
        progress = 0f;
    }

    protected override IEnumerator interact()
    {
        while (activated)
        {
            yield return new WaitForSeconds(Time.deltaTime / 2f);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                progress += DEFAULT_PROGRESS_INCREASE_AMOUNT;
                gauge.value = progress;
                if (progress >= MAX_PROGRESS)
                {
                    sendClearState(true);
                    activated = false;
                }
            }
        }
    }
}
