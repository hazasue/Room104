using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HiddenCatch : MiniGame
{
    private const int MAX_ANSWER_COUNT = 3;
    private static Vector3 DEFAULT_ANSWER_1 = new Vector3(0f, 0f, 0f);
    private static Vector3 DEFAULT_ANSWER_2 = new Vector3(-100f, -200f, 0f);
    private static Vector3 DEFAULT_ANSWER_3 = new Vector3(100f, 200f, 0f);
    private const float MAX_DETECT_RANGE = 40f;
    
    public Image beforeImage;
    public Image afterImage;
    public TMP_Text answerText;

    public Vector3[] answerPos;
    public GameObject answerImage;
    public Transform answerResources;
    
    private List<Vector3> leftAnswers;
    private List<Vector3> rightAnswers;
    private int currentAnswerCount;

    void Update()
    {
        if (!activated) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"{Input.mousePosition.x}, {Input.mousePosition.y}");
            Interact(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public override void Activate(float safety)
    {
        activated = true;
        init(safety);
    }

    protected override void init(float safety)
    {
        answerPos = new Vector3[MAX_ANSWER_COUNT];
        answerPos[0] = DEFAULT_ANSWER_1;
        answerPos[1] = DEFAULT_ANSWER_2;
        answerPos[2] = DEFAULT_ANSWER_3;
        
        leftAnswers = new List<Vector3>();
        rightAnswers = new List<Vector3>();

        for (int i = 0; i < MAX_ANSWER_COUNT; i++)
        {
            leftAnswers.Add(beforeImage.transform.position + answerPos[i]);
            rightAnswers.Add(afterImage.transform.position + answerPos[i]);
            Debug.Log(leftAnswers[i]);
        }

        for (int i = answerResources.childCount - 1; i >= 0; i--)
        {
            Destroy(answerResources.GetChild(i).gameObject);
        }

        currentAnswerCount = 0;
        answerText.text = $"{currentAnswerCount} / {MAX_ANSWER_COUNT}";
    }

    protected override IEnumerator interact()
    {
        yield break;
    }

    public void Interact(float xPos, float yPos)
    {
        for(int i = leftAnswers.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(leftAnswers[i], new Vector3(xPos, yPos, 0f)) <= MAX_DETECT_RANGE
                || Vector3.Distance(rightAnswers[i], new Vector3(xPos, yPos, 0f)) <= MAX_DETECT_RANGE)
            {
                currentAnswerCount++;
                answerText.text = $"{currentAnswerCount} / {MAX_ANSWER_COUNT}";

                if (currentAnswerCount >= MAX_ANSWER_COUNT)
                {
                    sendClearState(true);
                    activated = false;
                }

                GameObject tempObject = Instantiate(answerImage, answerResources, true);
                tempObject.transform.position = leftAnswers[i];

                tempObject = Instantiate(answerImage, answerResources, true);
                tempObject.transform.position = rightAnswers[i];

                leftAnswers.RemoveAt(i);
                rightAnswers.RemoveAt(i);
                return;
            }
        }
    }
}
