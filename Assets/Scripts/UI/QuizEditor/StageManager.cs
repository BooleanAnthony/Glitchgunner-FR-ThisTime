using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int stageNumber = 1;
    [SerializeField] private int maxStages = 2;
    [SerializeField] private TextMeshProUGUI currentStage;
    [SerializeField] private TMP_InputField inputField;
    private bool buttonActive = true;
    public int GetStageNumber() => stageNumber;
    public void SetStageNumber(int number) => stageNumber = number;

    public void ChangeStage()
    {
        stageNumber = (stageNumber < maxStages) ? stageNumber + 1 : 1;
        buttonActive = !buttonActive;
    }

    void LateUpdate()
    {
        currentStage.text = stageNumber.ToString();
        inputField.gameObject.SetActive(buttonActive);
    }
}
