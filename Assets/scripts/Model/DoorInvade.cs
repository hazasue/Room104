using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInvade : MiniGame
{
    private const float DEFAULT_DOOR_ROTATION = 25f;
    
    public Transform structure;
    private Vector3 structurePos;
    
    private float additionalIncreasion;
    
    public override void Activate(float safety)
    {
        activated = true;
        init(safety);
        StartCoroutine(decreaseGauge());
        StartCoroutine(interact());
    }

    protected override void init(float safety)
    {
        timeLimit = DEFAULT_TIME_LIMIT;
        timer.text = timeLimit.ToString("F2");

        this.safety = safety;
        switch (safety)
        {
            case 0f:
                additionalIncreasion = 0f;
                break;
            
            case 1f:
                additionalIncreasion = 0.01f;
                break;
            
            case 2f:
                additionalIncreasion = 0.03f;
                break;
            
            case 3f:
                additionalIncreasion = 0.05f;
                break;
            
            default:
                break;
        }

        gauge.maxValue = MAX_PROGRESS;
        progress = 0.5f;
        gauge.value = progress;
        
        structurePos = structure.eulerAngles;
        structure.rotation = Quaternion.Euler(structurePos + new Vector3(0f, DEFAULT_DOOR_ROTATION * progress, 0f));
    }

    protected override IEnumerator interact()
    {
        while (activated)
        {
            yield return new WaitForSeconds(Time.deltaTime / 2f);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                progress += DEFAULT_PROGRESS_INCREASE_AMOUNT + additionalIncreasion;
                gauge.value = progress;
                structure.rotation = Quaternion.Euler(structurePos + new Vector3(0f, DEFAULT_DOOR_ROTATION * progress, 0f));
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
            structure.rotation = Quaternion.Euler(structurePos + new Vector3(0f, DEFAULT_DOOR_ROTATION * progress, 0f));
            
            if (timeLimit <= 0f || progress <= 0f)
            {
                sendClearState(false);
                activated = false;
            }
        }
    }
}
