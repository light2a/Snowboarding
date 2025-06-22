using System;
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
    bool gameOver = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            CrashEffect.Play();
            GameOver();
            Invoke("ReloadScene", loadDelay);
            Debug.Log("done");
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
}
