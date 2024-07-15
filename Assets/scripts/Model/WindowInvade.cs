using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowInvade : MiniGame
{
    public RectTransform structure;
    private Vector3 structurePos;

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

        structurePos = structure.position;
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
                structure.position = structurePos + new Vector3(structure.rect.width * progress, 0f, 0f);
                
                if (progress >= MAX_PROGRESS)
                {
                    sendClearState(true);
                    activated = false;
                }
            }
        }
    }
    
    protected override IEnumerator decreaseGauge()
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
            structure.position = structurePos + new Vector3(structure.rect.width * progress, 0f, 0f);
            
            if (timeLimit <= 0f)
            {
                sendClearState(false);
                activated = false;
            }
        }
    }
    
    protected override void sendClearState(bool state)
    {
        structure.position = structurePos;
        MiniGameManager.Instance.GetCurrentMiniGameState(this, state);
    }
}
