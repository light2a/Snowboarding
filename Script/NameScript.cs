using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NameScript : MonoBehaviour
{
    [SerializeField]
    GameObject NameScreen;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button saveButton;
    [SerializeField]
    GameObject gameWinScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NameScreen.SetActive(false);
        saveButton.onClick.AddListener(OnSaveClicked);
    }
    void OnSaveClicked()
    {
        string playerName = nameInput.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Tên không được để trống");
            return;
        }
    
        FinishLine crashDetectors = FindObjectOfType<FinishLine>();
        CrashDetector detector = FindObjectOfType<CrashDetector>();

        
        crashDetectors.AddNewScore(playerName, detector.score);
        crashDetectors.SaveHighScores();
        NameScreen.SetActive(false);
        crashDetectors.ShowHighScoresOnUI();
        gameWinScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
