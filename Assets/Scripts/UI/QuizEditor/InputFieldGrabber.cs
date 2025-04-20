using UnityEngine;
using TMPro;

public class InputFieldGrabber : MonoBehaviour
{
    [Header("Recorded Input Value")] //If the text input is displayed here, it works.
    [SerializeField] private string inputText;

    [Header("Filler")] //Followed from a guide. Might use this to make a 
    [SerializeField] private GameObject reactionGroup;
    [SerializeField] private TMP_Text reactionTextBox;

    public void GrabFromInputField (string input)
    {
        inputText = input;
        DisplayReactionToInput();
    }

    public void DisplayReactionToInput()
    {
        reactionTextBox.text = "Question saved: " + inputText;
        reactionGroup.SetActive(true);
    }
}
