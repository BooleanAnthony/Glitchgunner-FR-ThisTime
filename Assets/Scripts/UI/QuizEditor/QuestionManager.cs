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

        //Storing each component of the placeholders using TMP_Text
        TMP_Text questionPlaceholder = question.placeholder.GetComponent<TMP_Text>();
        TMP_Text answerPlaceholder = answer.placeholder.GetComponent<TMP_Text>();
        TMP_Text filler1Placeholder = filler1.placeholder.GetComponent<TMP_Text>();
        questionPlaceholder.text = jsonReader.questions[current_question].questionInput;
        answerPlaceholder.text = jsonReader.questions[current_question].answerInput;
        filler1Placeholder.text = jsonReader.questions[current_question].filler1Input;

        if (filler2 != null)
        {
            TMP_Text filler2Placeholder = filler2.placeholder.GetComponent<TMP_Text>();
            filler2Placeholder.text = jsonReader.questions[current_question].filler2Input;
        }
    }
}
