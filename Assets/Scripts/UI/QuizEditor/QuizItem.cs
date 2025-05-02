using System;

[Serializable]
public class QuizItem 
    {
        public string[] choices;
        public int correct_answer;
        public int questionNumber;
        public string questionInput;
        public string answerInput;
        public string filler1Input;
        public string filler2Input;
    }