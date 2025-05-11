using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private int questionNumber = 1;
    [SerializeField] private int maxQuestions = 3;
    [SerializeField] private TextMeshProUGUI currentQuestion;

    //Stores the input fields to modify the placeholders
    [SerializeField] private TMP_InputField question, answer, filler1, filler2;
    [SerializeField] private JSONReader jsonReader;
    
    public int current_question = 0;

    public int GetQuestionNumber() => questionNumber;
    public void SetQuestionNumber(int number) => questionNumber = number;

    public void NextQuestion()
    {
        if (current_question < 3)
        {
            current_question++;
        }
        questionNumber = Mathf.Min(questionNumber + 1, maxQuestions);
    }
    public void PreviousQuestion()
    {
        if (current_question > 0)
        {
            current_question--;
        }
        questionNumber = Mathf.Max(1, questionNumber - 1);
    }

    public void RefreshAllInputFields()
    {
        jsonReader.RefreshJson();
    }

    void LateUpdate()
    {
        currentQuestion.text = questionNumber.ToString();

        //Storing each component of the placeholders using TMP_Text
        TMP_Text questionPlaceholder = question.placeholder.GetComponent<TMP_Text>();
        TMP_Text answerPlaceholder = answer.placeholder.GetComponent<TMP_Text>();
        TMP_Text filler1Placeholder = filler1.placeholder.GetComponent<TMP_Text>();
        TMP_Text filler2Placeholder = filler2.placeholder.GetComponent<TMP_Text>();

        questionPlaceholder.text = jsonReader.questions[current_question].questionInput;
        answerPlaceholder.text = jsonReader.questions[current_question].answerInput;
        filler1Placeholder.text = jsonReader.questions[current_question].filler1Input;
        filler2Placeholder.text = jsonReader.questions[current_question].filler2Input;
    }
}
