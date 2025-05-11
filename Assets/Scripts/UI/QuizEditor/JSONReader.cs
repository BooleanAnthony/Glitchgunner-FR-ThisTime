using System;
using System.IO;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    [SerializeField] private TextAsset jsonData;
    private QuizList collection;
    public QuizItem[] questions;
    
    
    void Start()
    {
        RefreshJson();
    }

    public void RefreshJson()
    {
        // Deserialize JSON into a QuizList object.
        collection = JsonUtility.FromJson<QuizList>(jsonData.text);

        // Extract the quiz items.
        questions = collection.questions;

        // Post-process each quiz item: if choices aren't provided in the JSON,
        // initialize 'choices' and calculate 'correct_answer'.
        foreach (QuizItem quiz in questions)
        {
            quiz.ShuffleChoices(); //Because of Kaizen x2
            if (quiz.choices == null || quiz.choices.Length == 0)
            {
                // Here we assume that answerInput, filler1Input, and filler2Input form the choices.
                quiz.choices = new string[] { quiz.answerInput, quiz.filler1Input, quiz.filler2Input };
                quiz.correct_answer = Array.IndexOf(quiz.choices, quiz.answerInput);
            }

            // Output to Unity's Console for verification.
            Debug.Log("Question Number: " + quiz.questionNumber);
            Debug.Log("Question: " + quiz.questionInput);
            Debug.Log("Choices: " + string.Join(", ", quiz.choices));
            Debug.Log("Correct answer index: " + quiz.correct_answer);
            Debug.Log("Correct Answer: " + quiz.choices[quiz.correct_answer]);
        }
    }
}
