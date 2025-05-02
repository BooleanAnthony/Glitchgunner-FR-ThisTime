using UnityEngine;
using System.IO;
using TMPro;
using BossFights;

public class QuizInputDemo : MonoBehaviour
{   
    public TMP_InputField question, answer, filler1, filler2; //Easily assign input field objects to script

    [Header("Recorded Input Values")]
    [SerializeField] private string questionReflect, answerReflect, filler1Reflect, filler2Reflect; //To check functionality
    public void ReflectDemo() //just to make sure it works. Check the SaveInputDemo object
    {
        questionReflect = question.text; //"TMP_InputField".text to read text inside object
        answerReflect = answer.text;
        filler1Reflect = filler1.text;
        filler2Reflect = filler2.text;
    }

    public class QuizItem //class to write in JSON
    {
        public string questionInput;
        public string answerInput;
        public string filler1Input;
        public string filler2Input;
    }

    public QuizItem quizItem1 = new QuizItem(); //comment

    public void writeJSON() //comment
    {
        quizItem1.questionInput = question.text;
        quizItem1.answerInput = answer.text;
        quizItem1.filler1Input = filler1.text;
        quizItem1.filler2Input = filler2.text;
        string quizItemOutput = JsonUtility.ToJson(quizItem1);
        File.WriteAllText(Application.dataPath + "/Scripts/Quiz/sampleQuiz.json", quizItemOutput);
    }
}