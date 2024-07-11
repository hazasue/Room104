using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorLock : MonoBehaviour
{
    private const int DEFAULT_NUMBER_COUNT_LITTLE = 4;
    private const int DEFAULT_NUMBER_COUNT_MANY = 8;

    private int passWordLength;
    private List<int> passWord;
    private List<int> answerSheet;
    private List<TMP_Text> answerSheetTexts;

    public Transform viewport;
    public TMP_Text answerSheetPrefab;
    public TMP_Text passWordText;

    // Start is called before the first frame update
    void Start()
    {
        InitNumber(DEFAULT_NUMBER_COUNT_MANY);
    }

    public void InitNumber(int length)
    {
        passWord = new List<int>();
        answerSheet = new List<int>();
        answerSheetTexts = new List<TMP_Text>();
        passWordLength = length;

        for (int i = viewport.childCount - 1; i >= 0; i--)
        {
            Destroy(viewport.GetChild(i).gameObject);
        }

        for (int i = 0; i < passWordLength; i++)
        {
            passWord.Add(Random.Range(0, 10));
            answerSheetTexts.Add(Instantiate(answerSheetPrefab, viewport, true));
            answerSheetTexts[i].text = "";
        }

        showPassWord();
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
            //success
            Debug.Log("Complete");
        }

        updateAnswerSheet();
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
            answerSheetTexts[i].text = $"{answerSheet[i]}";
        }

        for (; i < passWordLength; i++)
        {
            answerSheetTexts[i].text = "";
        }
    }
}
