using TMPro;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] fields;

    public void ClearAll()
    {
        foreach (TMP_InputField field in fields)
        {
            field.text = ""; // Clears the text
        }
    }
}
