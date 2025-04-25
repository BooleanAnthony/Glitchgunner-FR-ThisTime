using UnityEngine;
using System.IO;
using TMPro;
using BossFights;

public class QuizInputDemo : MonoBehaviour
{   
    public TMP_InputField question, answer, filler; //Easily assign input field objects to script

    [Header("Recorded Input Values")]
    [SerializeField] private string questionReflect, answerReflect, fillerReflect; //To check functionality
    public void ReflectDemo() //just to make sure it works. Check the SaveInputDemo object
    {
        questionReflect = question.text; //"TMP_InputField".text to read text inside object
        answerReflect = answer.text;
        fillerReflect = filler.text;
    }

    public class QuizItem //class to write in JSON
    {
        public string questionInput;
        public string answerInput;
        public string fillerInput;
    }

    public QuizItem quizItem1 = new QuizItem(); //comment

    public void writeJSON() //comment
    {
        quizItem1.questionInput = question.text;
        quizItem1.answerInput = answer.text;
        quizItem1.fillerInput = filler.text;
        string quizItemOutput = JsonUtility.ToJson(quizItem1);
        File.WriteAllText(Application.dataPath + "/Scripts/Quiz/sampleQuiz.json", quizItemOutput);
    }
}