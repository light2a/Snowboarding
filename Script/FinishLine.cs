using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] 
    float loadDelay = 0.1f;
    [SerializeField]
    ParticleSystem finishEffect;
    [SerializeField]
    GameObject gameWinScreen;
    [SerializeField] [CanBeNull] GameObject NameScreen;
    [SerializeField][CanBeNull]  private TMPro.TextMeshProUGUI[] scoreLines;
    bool gameWin = false;
    string path;

    void Awake()
    {
        path = Application.persistentDataPath + "/highscores.txt";
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            GameWin();
        }
    }
    public void ShowHighScoresOnUI()
    {
        var list = GetHighScoreDisplayList(); // ví dụ: ["1. Duc - 1000", ...]

        for (int i = 0; i < scoreLines.Length; i++)
        {
            if (i < list.Count)
            {
                scoreLines[i].text = list[i];
            }
            else
            {
                scoreLines[i].text = ""; // xóa nếu không có dữ liệu
            }
        }
    }
    
    string[] names = new string[5];
    int[] scores = new int[5];

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/highscores.txt";

        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < Mathf.Min(5, lines.Length); i++)
            {
                string[] parts = lines[i].Split(',');
                names[i] = parts[0];
                scores[i] = int.Parse(parts[1]);
            }
        }
        else
        {
            Debug.Log("Không tìm thấy file highscore.");
        }
    }
    public void SaveHighScores()
    {
        string path = Application.persistentDataPath + "/highscores.txt";
        using (StreamWriter writer = new StreamWriter(path))
        {
            for (int i = 0; i < 5; i++)
            {
                writer.WriteLine($"{names[i]},{scores[i]}");
            }
        }
    }
    public void AddNewScore(string newName, int newScore)
    {
        // Load trước
        LoadHighScores();

        // Tạm thêm vào cuối
        string[] tempNames = new string[6];
        int[] tempScores = new int[6];

        for (int i = 0; i < 5; i++)
        {
            tempNames[i] = names[i];
            tempScores[i] = scores[i];
        }
        tempNames[5] = newName;
        tempScores[5] = newScore;

        // Sắp xếp giảm dần
        for (int i = 0; i < 6; i++)
        {
            for (int j = i + 1; j < 6; j++)
            {
                if (tempScores[j] > tempScores[i])
                {
                    // hoán đổi điểm
                    int tmpScore = tempScores[i];
                    tempScores[i] = tempScores[j];
                    tempScores[j] = tmpScore;

                    // hoán đổi tên
                    string tmpName = tempNames[i];
                    tempNames[i] = tempNames[j];
                    tempNames[j] = tmpName;
                }
            }
        }

        // Cắt lại 5 phần tử
        for (int i = 0; i < 5; i++)
        {
            names[i] = tempNames[i];
            scores[i] = tempScores[i];
        }

        SaveHighScores();
    }
    public bool IsInTop5( int newScore)
    {
        LoadHighScores(); // đảm bảo đã load mảng names & scores từ file

        for (int i = 0; i < 5; i++)
        {
            if (newScore > scores[i])
            {
                return true; // điểm cao hơn ai đó trong top 5
            }
        }

        return false; // không đủ điểm vào top 5
    }
    public bool NameAlreadyInTop5(string newName)
    {
        LoadHighScores();

        for (int i = 0; i < 5; i++)
        {
            if (names[i] == newName)
            {
                return true;
            }
        }
        return false;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Level1");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameWinScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GameWin()
    { 
        CrashDetector crashDetectsors = FindObjectOfType<CrashDetector>();

        var main = finishEffect.main;
        main.useUnscaledTime = true;
        finishEffect.Play();
        Time.timeScale = 0;
        gameWin = true;
        if (IsInTop5(crashDetectsors.score))
        {
            NameScreen.SetActive(true);
            ShowHighScoresOnUI(); 
        }
        else
        {
            ShowHighScoresOnUI(); 
            gameWinScreen.SetActive(true);
        }
        crashDetectsors.score=0;
            
    }
    public List<string> GetHighScoreDisplayList()
    {
        LoadHighScores();

        List<string> result = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            if (!string.IsNullOrEmpty(names[i]))
            {
                result.Add($"{i + 1}. {names[i]} - {scores[i]}");
            }
        }

        return result;
    }
}
