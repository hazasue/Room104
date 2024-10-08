using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorLock : MiniGame
{
    private const int DEFAULT_NUMBER_COUNT_LITTLE = 4;
    private const int DEFAULT_NUMBER_COUNT_MANY = 8;
    private const float DEFAULT_TIME_LIMIT_DOOR_LOCK = 5f;

    private int passWordLength;
    private List<int> passWord;
    private List<int> answerSheet;
    private List<Transform> answerSheetTexts;

    public Transform viewport;
    public Transform answerSheetPrefab;
    public TMP_Text passWordText;

    private Dictionary<int, Image> images;
    public List<Image> imageLists;
    public List<Sprite> defaultImages;
    public List<Sprite> hoverImages;
    public List<Sprite> clickImages;

    // Start is called before the first frame update

    public override void Activate(float safety)
    {
        activated = true;
        init(safety);
        StartCoroutine(decreaseGauge());
    }

    protected override void init(float safety)
    {
        this.safety = safety;
        if (safety != 3) timeLimit = DEFAULT_TIME_LIMIT_DOOR_LOCK + safety;
        else
        {
            timeLimit = DEFAULT_TIME_LIMIT_DOOR_LOCK + 5f;
        }

        timer.text = timeLimit.ToString("F2");

        int length = DEFAULT_NUMBER_COUNT_MANY;

        passWord = new List<int>();
        answerSheet = new List<int>();
        answerSheetTexts = new List<Transform>();
        passWordLength = length;

        for (int i = viewport.childCount - 1; i >= 0; i--)
        {
            Destroy(viewport.GetChild(i).gameObject);
        }

        for (int i = 0; i < passWordLength; i++)
        {
            passWord.Add(Random.Range(0, 10));
            answerSheetTexts.Add(Instantiate(answerSheetPrefab, viewport, true));
            answerSheetTexts[i].GetChild(0).GetComponent<TMP_Text>().text = "";
        }

        images = new Dictionary<int, Image>();
        for (int i = 0; i < imageLists.Count; i++)
        {
            images.Add(i, imageLists[i]);
        }
        
        showPassWord();
    }

    protected override IEnumerator interact()
    {
        yield break;
    }

    protected override IEnumerator decreaseGauge()
    {
        float gap;
        while (activated)
        {
            gap = Time.deltaTime;
            yield return new WaitForSeconds(gap);

            timeLimit -= gap;
            timer.text = timeLimit.ToString("F2");

            if (timeLimit <= 0f)
            {
                sendClearState(false);
                activated = false;
            }
        }
    }

    private void showPassWord()
    {
        passWordText.text = "";
        for (int i = 0; i < passWordLength; i++)
        {
            passWordText.text += $"{passWord[i]}";
        }
    }

    private void updateAnswerSheet()
    {
        int i;
        for (i = 0; i < answerSheet.Count; i++)
        {
            answerSheetTexts[i].GetChild(0).GetComponent<TMP_Text>().text = $"{answerSheet[i]}";
        }

        for (; i < passWordLength; i++)
        {
            answerSheetTexts[i].GetChild(0).GetComponent<TMP_Text>().text = "";
        }
    }
    
    public void AddNumber(int number)
    {
        int idx = answerSheet.Count;

        answerSheet.Add(number);

        if (passWord[idx] != answerSheet[idx])
        {
            answerSheet.Clear();
        }

        if (passWord.Count == answerSheet.Count)
        {
            sendClearState(true);
            activated = false;
        }

        updateAnswerSheet();
    }

    public void HoverNumber(int number)
    {
        images[number].sprite = hoverImages[number];
    }

    public void ClickNumber(int number)
    {
        images[number].sprite = clickImages[number];
        StartCoroutine(resetNumberPad(number));
    }

    public void MakeNumberPadDefault(int number)
    {
        images[number].sprite = defaultImages[number];
    }

    private IEnumerator resetNumberPad(int number)
    {
        yield return new WaitForSeconds(0.05f);

        images[number].sprite = defaultImages[number];
    }
}
