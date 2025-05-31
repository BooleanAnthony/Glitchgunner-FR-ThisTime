using System;
using System.IO;
using UnityEngine;


public class JSONReader : MonoBehaviour
{
    enum BossType {Hornet, Centipede}
    [SerializeField] private BossType bossType;
    private string jsonQuizPath;
    private QuizList collection;
    public QuizItem[] questions;
    private string fileName = "";
   
   
    void Start()
    {
        if (bossType == BossType.Hornet)
        {
            fileName = "quiz1.json";
        }
        else if (bossType == BossType.Centipede)
        {
            fileName = "quiz2.json";
        }


        // Build full path to file in StreamingAssets
        jsonQuizPath = Path.Combine(Application.streamingAssetsPath, "JSONData", fileName);


        RefreshJson();
    }




    public void RefreshJson()
    {
        if (!File.Exists(jsonQuizPath))
        {
            Debug.LogError("File not found at path: " + jsonQuizPath);
            return;
        }


        string jsonText = File.ReadAllText(jsonQuizPath);
        collection = JsonUtility.FromJson<QuizList>(jsonText);
        questions = collection.questions;


        foreach (QuizItem quiz in questions)
        {
            if (quiz.filler2Input != "0")
            {
                quiz.ShuffleChoices(); // Because of Kaizen x2
            }


            if (quiz.choices == null || quiz.choices.Length == 0)
            {
                quiz.choices = new string[] { quiz.answerInput, quiz.filler1Input, quiz.filler2Input };
                quiz.correct_answer = Array.IndexOf(quiz.choices, quiz.answerInput);
            }


            Debug.Log("Question Number: " + quiz.questionNumber);
            Debug.Log("Question: " + quiz.questionInput);
            Debug.Log("Choices: " + string.Join(", ", quiz.choices));
            Debug.Log("Correct answer index: " + quiz.correct_answer);
            Debug.Log("Correct Answer: " + quiz.choices[quiz.correct_answer]);
        }
    }
}



//UPDATED SCRIPT