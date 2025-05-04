using System;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private int questionNumber = 1;
    [SerializeField] private int maxQuestions = 3;
    [SerializeField] private TextMeshProUGUI currentQuestion;

    public int GetQuestionNumber() => questionNumber;
    public void SetQuestionNumber(int number) => questionNumber = number;

    public void NextQuestion() => questionNumber = Mathf.Min(questionNumber + 1, maxQuestions);
    public void PreviousQuestion() => questionNumber = Mathf.Max(1, questionNumber - 1);

    void LateUpdate()
    {
        currentQuestion.text = questionNumber.ToString();
    }
}
