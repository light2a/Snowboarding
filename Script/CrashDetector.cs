using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] 
    float loadDelay = 2.5f;
    [SerializeField]
    ParticleSystem CrashEffect;
    [SerializeField]
    GameObject gameOverScreen;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public int score = 0;
    bool gameOver = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag.ToString());
        if (other.CompareTag("Ground") && other is CircleCollider2D)
        {
            CrashEffect.Play();
            GameOver();
            Invoke("ReloadScene", loadDelay);
            Debug.Log("done");
        }
        if (other.CompareTag("Object") && other is BoxCollider2D)
        {
            CrashEffect.Play();
            GameOver();
            Invoke("ReloadScene", loadDelay);
            Debug.Log("done");
        }if (other.gameObject.tag.ToString().Equals("CoinNumber") )
        {
            Destroy(other.gameObject);
            AddScore();
            UpdateScore();
        }
    }
    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/Menu");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverScreen.SetActive(false);
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameOver()
    {
        Time.timeScale = 0;
        gameOver = true;
        gameOverScreen.SetActive(true);
    }

    public void AddScore()
    {
        Debug.Log("AddScore");
        score++;
    }

    public void UpdateScore()
    {
        scoreText.text ="Score: "+ score;
    }
}
