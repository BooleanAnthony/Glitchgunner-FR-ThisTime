using UnityEngine;
using System.IO;
using TMPro;
using Unity.VisualScripting;

public class QuizInputDemo : MonoBehaviour
{   
    [SerializeField] private TMP_InputField question, answer, filler1, filler2; // Easily assign input field objects in Unity Inspector
    [SerializeField] private string fileName;
    [SerializeField] private QuestionManager questionManager;
    //[SerializeField] private StageManager stageManager;

    [Header("Recorded Input Values")]
    [SerializeField] private string questionReflect, answerReflect, filler1Reflect, filler2Reflect; // For debugging input values

    public QuizItem quizItem1 = new(); //Is this needed here? - Kaizen

    public void ReflectDemo() // Debugging method to reflect user input
    {
        questionReflect = question.text;
        answerReflect = answer.text;
        filler1Reflect = filler1.text;

        if (filler2 != null)
        {
            filler2Reflect = filler2.text;
        }
    }

    public void writeJSON() 
    {
        if (string.IsNullOrEmpty(question.text) || string.IsNullOrEmpty(answer.text))
        {
            Debug.Log("Text field is empty!");
            return;
        }

        int targetQuestionNumber = questionManager.GetQuestionNumber();
        Debug.Log($"Attempting to update question {targetQuestionNumber} in JSON...");

        // Create a new QuizItem from user input
        QuizItem newQuizItem = new();
        modifyQuizItem(newQuizItem, targetQuestionNumber);

        if (newQuizItem.correct_answer < 0) 
        {
            Debug.Log("Nothing was saved");
            return;
        }

        // Define file path
        string filePath = Application.dataPath + "/JSONData/" + fileName + ".json";

        // Check if JSON file exists
        QuizList quizCollection;
        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            quizCollection = JsonUtility.FromJson<QuizList>(existingJson);
        }
        else
        {
            Debug.LogWarning("JSON file does not exist. Cannot update.");
            return;
        }

        // Debugging: Show all current questions before updating
        Debug.Log($"Existing Questions Count: {quizCollection.questions.Length}");
        foreach (QuizItem item in quizCollection.questions)
        {
            Debug.Log($"Question {item.questionNumber}: {item.questionInput}");
        }

        // Flag to track if any question was updated
        bool updated = false;

        // Loop through questions and update the matching one
        for (int i = 0; i < quizCollection.questions.Length; i++)
        {
            if (quizCollection.questions[i].questionNumber == targetQuestionNumber)
            {
                quizCollection.questions[i] = newQuizItem;
                updated = true;
                Debug.Log($"Updated Question {targetQuestionNumber} Successfully.");
                break;
            }
        }

        // If no match found, show a warning
        if (!updated)
        {
            Debug.LogWarning($"No question found with questionNumber {targetQuestionNumber}. JSON remains unchanged.");
            return;
        }

        // Debugging: Show all updated questions AFTER modification
        foreach (QuizItem item in quizCollection.questions)
        {
            Debug.Log($"Updated Question {item.questionNumber}: {item.questionInput}");
        }

        // Convert updated data back to JSON
        string updatedJson = JsonUtility.ToJson(quizCollection, true);

        // Write updated JSON file
        File.WriteAllText(filePath, updatedJson);
        Debug.Log($"Successfully updated question {targetQuestionNumber} in JSON.");
    }

    private void modifyQuizItem(QuizItem newQuizItem, int targetQuestionNumber)
    {
        newQuizItem.questionNumber = targetQuestionNumber;
        newQuizItem.questionInput = question.text;
        newQuizItem.answerInput = answer.text;
        newQuizItem.filler1Input = filler1.text;
        if (filler2 != null)
        {
            newQuizItem.filler2Input = filler2.text;
        } 
        else 
        {
            newQuizItem.filler2Input = "0";
        }
        newQuizItem.choices = new string[] {newQuizItem.answerInput, newQuizItem.filler1Input, newQuizItem.filler2Input};
        newQuizItem.correct_answer = System.Array.IndexOf(newQuizItem.choices, newQuizItem.answerInput);
    }
}
