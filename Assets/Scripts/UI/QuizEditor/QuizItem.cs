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

        public void ShuffleChoices()
        {
            Random rand = new();
            for (int i = choices.Length - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                (choices[i], choices[j]) = (choices[j], choices[i]); // Swap elements
            }

            // Update correct_answer index after shuffling
            correct_answer = Array.IndexOf(choices, answerInput);
        }
    }